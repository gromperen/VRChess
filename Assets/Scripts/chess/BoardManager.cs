using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor;
using UnityEditor.Presets;

public class BoardManager : MonoBehaviour
{
    public bool WhiteCastleRight = true;
    public bool WhiteCastleLong = true;
    public bool WhiteCastleShort = true;
    public bool BlackCastleRight = true;
    public bool BlackCastleLong = true;
    public bool BlackCastleShort = true;
    Dictionary<string, int> TypePrefabIndexMap = new Dictionary<string, int>{
        {"King", 0},
        {"Queen", 1},
        {"Rook", 2},
        {"Knight", 3},
        {"Bishop", 4},
        {"Pawn", 5}
    };
    private BoardHighlights _BoardHighlights;
    public int STATE = 0; // 0 = White, 1 = Black, 2 = Game Over
    public bool PlayerIsWhite = true;

    public Piece[,] board { set; get; }
    private Piece selectedPiece;
    public GameObject[,] snapZones { set; get; }
    private const float TILE_SIZE = 1.0f;

    public GameObject snapPrefab;
    public List<GameObject> chessPrefabs;
    public static List<GameObject> activePieces = new List<GameObject>();
    public int[] EnPassant;

    public bool[,] SquaresAttackedByWhite = new bool[8, 8];
    public bool[,] SquaresAttackedByBlack = new bool[8, 8];

    // Start is called before the first frame update
    void Start()
    {
        _BoardHighlights = gameObject.GetComponent<BoardHighlights>();
        SpawnAllPieces();
        SpawnAllSnapzones();
        InitTurn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
    }


    public void Test()
    {
        Debug.Log("TEST");
    }

    void SpawnAllSnapzones()
    {
        snapZones = new GameObject[8, 8];
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                SpawnSnapZone(i, j);
            }
        }
    }
    void SpawnSnapZone(int x, int y)
    {
        GameObject temp = Instantiate(snapPrefab, GetTileCentre(x, y), Quaternion.Euler(0, 0, 0)) as GameObject;
        temp.transform.SetParent(transform);
        snapZones[x, y] = temp;

    }

    public bool[,] AttackedSquares(bool byWhite, bool recalculate) // Yes I know this is the worst programming you have ever seen so please fix it.
    {
        if (recalculate)
        {
            Debug.Log("Recalculated Attacked Squares");
            bool[,] ret = new bool[8, 8];
            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if (board[i, j] != null && board[i, j].isWhite == byWhite)
                    {
                        bool[,] tmp = board[i, j].PossibleMoves();

                        for (int a = 0; a < 8; ++a)
                        {
                            for (int b = 0; b < 8; ++b)
                            {
                                if (tmp[a, b])
                                {
                                    ret[a, b] = true;
                                }
                            }
                        }
                    }
                }
            }
            if (byWhite)
            {
                SquaresAttackedByWhite = ret;
            }
            else
            {
                SquaresAttackedByBlack = ret;
            }
            return ret;
        }
        else
        {
            if (byWhite)
                return SquaresAttackedByWhite;
            else
                return SquaresAttackedByBlack;
        }
    }
    public void ResetSelectedPiece()
    {
        int prefabindex = 0;
        if (!selectedPiece.isWhite)
        {
            prefabindex += 6;
        }
        prefabindex += TypePrefabIndexMap[selectedPiece.type];


        _BoardHighlights.HideHighlights(selectedPiece.PossibleMoves());
        
        activePieces.Remove(selectedPiece.Object);
        selectedPiece.Object.transform.position = new Vector3(6969, -50, 6969);
        SpawnPiece(prefabindex,selectedPiece.CurrentX, selectedPiece.CurrentY);



        // selectedPiece.Object.transform.position = new Vector3(selectedPiece.CurrentX + 0.5f, 2f, selectedPiece.CurrentY + 0.5f); //  GetTileCentre(selectedPiece.CurrentX, selectedPiece.CurrentY);
        Debug.Log("reset selected piece to " + selectedPiece.CurrentX + ","+ selectedPiece.CurrentY);
        selectedPiece = null;
    }


    void ResetSnapZone(int x, int y)
    {
        snapZones[x, y].transform.position = new Vector3(420, -420, 6969);
        snapZones[x, y] = null;
        SpawnSnapZone(x, y);
    }

    public void ResetGame()
    {
        foreach (GameObject go in activePieces)
        {
            go.transform.position = new Vector3 (6969, 69, 6969);
            Destroy(go);
        }
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                snapZones[i,j].transform.position = new Vector3 (6969, 69, 6969);
                snapZones[i, j] = null;
            }
        }
        _BoardHighlights.HideAllHighlights();   
        STATE = 2;
    }
    void PlayerMovePiece(int x, int y)
    {
        // bool isEnpassantMove = false;
        if (selectedPiece.PossibleMoves()[x, y])
        {
            Piece p = board[x, y];
            if (p != null)
            {
                activePieces.Remove(p.Object);
                Destroy(p.Object);
                if (p.type == "King")
                {
                    ResetGame();
                }
                if (p.type == "Rook")
                {
                    if (STATE == 0)
                    {
                        if (p.CurrentX == 0 && p.CurrentY == 0)
                        {
                            WhiteCastleLong = false;
                        }
                        if (p.CurrentX == 7 && p.CurrentY == 0)
                        {
                            WhiteCastleShort = false;
                        }
                    }
                    else
                    {
                        if (p.CurrentX == 0 && p.CurrentY == 7)
                        {
                            BlackCastleLong = false;
                        }
                        if (p.CurrentX == 7 && p.CurrentY == 7)
                        {
                            BlackCastleShort = false;
                        }

                    }

                }
            }
            if (selectedPiece.type == "Rook")
            {
                if (STATE == 0)
                {
                    if (selectedPiece.CurrentX == 0 && selectedPiece.CurrentY == 0)
                    {
                        WhiteCastleLong = false;
                    }
                    if (selectedPiece.CurrentX == 7 && selectedPiece.CurrentY == 0)
                    {
                        WhiteCastleShort = false;
                    }
                }
                else
                {
                    if (selectedPiece.CurrentX == 0 && selectedPiece.CurrentY == 7)
                    {
                        BlackCastleLong = false;
                    }
                    if (selectedPiece.CurrentX == 7 && selectedPiece.CurrentY == 7)
                    {
                        BlackCastleShort = false;
                    }

                }
            }
            if (selectedPiece.type == "King")
            {
                if (STATE == 0)
                {
                    if (WhiteCastleRight)
                    {
                        if (y == 0)
                        {
                            // Short Castle
                            if (x == 6)
                            {
                                activePieces.Remove(board[7, 0].Object);
                                Destroy(board[7, 0].Object);
                                board[7, 0] = null;
                                ResetSnapZone(7,0);
                                SpawnPiece(2, 5, 0);
                            }
                            else if (x == 2)
                            {
                                activePieces.Remove(board[0, 0].Object);
                                Destroy(board[0, 0].Object);
                                board[0, 0] = null;
                                ResetSnapZone(0,0);
                                SpawnPiece(2, 3, 0);

                            }
                        }
                    }
                    WhiteCastleRight = false;
                }
                else
                {
                    if (BlackCastleRight)
                    {
                        if (y == 7)
                        {
                            // Short Castle
                            if (x == 6)
                            {
                                activePieces.Remove(board[7, 7].Object);
                                Destroy(board[7, 7].Object);
                                board[7, 7] = null;
                                ResetSnapZone(7,7);
                                SpawnPiece(2, 5, 7);
                            }
                            else if (x == 2)
                            {
                                activePieces.Remove(board[0, 7].Object);
                                Destroy(board[0, 7].Object);
                                board[0, 7] = null;
                                ResetSnapZone(0,7);
                                SpawnPiece(2, 3, 7);
                            }
                        }
                    }
                    BlackCastleRight = false;
                }
            }
            if (selectedPiece.type == "Pawn")
            {
                // Promotion
                if (STATE == 0)
                {
                    if (y == 7)
                    {
                        int oldx = selectedPiece.CurrentX, oldy = selectedPiece.CurrentY;
                        activePieces.Remove(selectedPiece.Object);
                        selectedPiece.Object.transform.position = new Vector3(6969, 6969, 6969);
                        ResetSnapZone(x, y);
                        ResetSnapZone(oldx, oldy);
                        SpawnPiece(1, x, y);

                        selectedPiece = board[x, y];
                        selectedPiece.SetPosition(oldx, oldy);

                    }
                }
                else
                {
                    if (y == 0)
                    {
                        int oldx = selectedPiece.CurrentX, oldy = selectedPiece.CurrentY;
                        activePieces.Remove(selectedPiece.Object);
                        selectedPiece.Object.transform.position = new Vector3(6969, 6969, 6969);
                        ResetSnapZone(x, y);
                        ResetSnapZone(oldx, oldy);
                        SpawnPiece(7, x, y);
                        selectedPiece = board[x, y];
                        selectedPiece.SetPosition(oldx, oldy);

                    }

                }
            }
            // En Passant
            if (selectedPiece.type == "Pawn" && x == EnPassant[0] && y == EnPassant[1])
            {
                // isEnpassantMove = true;
                if (STATE == 0)
                {
                    p = board[x, y + 1];
                }
                else 
                {
                    p = board[x, y - 1];
                }
                
            }

            EnPassant[0] = -1;
            EnPassant[1] = -1;
            if (selectedPiece.type == "Pawn")
            {
                if (System.Math.Abs(selectedPiece.CurrentY - y) == 2)
                {
                    EnPassant[0] = x;
                    EnPassant[1] = y;
                    if (STATE == 0)
                    {
                        EnPassant[1] -= 1;
                    }
                    else{
                        EnPassant[1] += 1;
                    }
                } 
            }

            ResetSnapZone(selectedPiece.CurrentX, selectedPiece.CurrentY);
            board[selectedPiece.CurrentX, selectedPiece.CurrentY] = null; 
            selectedPiece.SetPosition(x, y);
            board[x, y] = selectedPiece;
            _BoardHighlights.HideAllHighlights();

            

            

            // Fix for Bug where Piece wont enter the place of the destroyed piece.
            ResetSnapZone(x, y);
            ResetSelectedPiece();
        }
        else
        {
            ResetSnapZone(selectedPiece.CurrentX, selectedPiece.CurrentY);
            ResetSelectedPiece();
            ResetSnapZone(x, y);
        }
        AttackedSquares(!PlayerIsWhite, true);
    }
    

    void ResetAllSnapZones()
    {
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                ResetSnapZone(i, j);
            }
        }
    }
    public void DetectMovedPiece()
    {

        



        Piece foundpiece;
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (board[i, j] == null)
                {
                    continue;
                }
                else
                {
                    GameObject currentObject = board[i, j].Object;
                    if (Math.Round(currentObject.transform.position.x - 0.5) != board[i, j].CurrentX ||
                        Math.Round(currentObject.transform.position.z - 0.5) != board[i, j].CurrentY)
                    {
                        foundpiece = board[i, j];
                        // Debug.Log(Math.Round(currentObject.transform.position.x - 0.5));
                        Debug.Log("Moved Piece is from" + foundpiece.CurrentX + "," + foundpiece.CurrentY + "Value: " + foundpiece.Value);
                        selectedPiece = foundpiece;
                        PlayerMovePiece((int)Math.Round(currentObject.transform.position.x - 0.5), (int)Math.Round(currentObject.transform.position.z - 0.5));
                        return;
                    }
                }
            }
        }
    }

    public void InitTurn()
    {
        // REMOVE THIS LATER
        bool tmp;
        if (STATE == 0)
        {
            tmp = true;
        }
        else
        {
            tmp = false;
        }
        AttackedSquares(tmp, true);
        // REMOVE THIS LATER


        bool isPlayerTurn;
        if ((STATE == 0 && !PlayerIsWhite) || (STATE == 1 && PlayerIsWhite))
        {
            isPlayerTurn = false;
        }
        else
        {
            isPlayerTurn = true;
        }
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (board[i, j] != null)
                {
                    if (board[i, j].isWhite == PlayerIsWhite && isPlayerTurn)
                    {
                        SetGrab(board[i, j].Object, true);
                    }
                    else
                    {
                        SetGrab(board[i, j].Object, false);
                    }
                }
            }
        }
    }

    public void ToggleGrab(GameObject obj)
    {
        if (obj.GetComponent<XROffsetGrabInteractable>() != null)
        {
            GameObject.Destroy(obj.GetComponent<XROffsetGrabInteractable>());
        }
        else
        {
            obj.AddComponent<XROffsetGrabInteractable>();
        }
    }

    public void SetGrab(GameObject obj, bool enable)
    {
        if (enable)
        {
            if (obj.GetComponent<XROffsetGrabInteractable>() == null)
            {
                obj.AddComponent<XROffsetGrabInteractable>();
            }
        }
        else
        {
            if (obj.GetComponent<XROffsetGrabInteractable>() != null)
            {
                GameObject.Destroy(obj.GetComponent<XROffsetGrabInteractable>());
            }
        }
    }
    public void ChangePlayer()
    {
        Debug.Log("Changing Player");
        foreach (GameObject i in activePieces)
        {
            ToggleGrab(i);
        }
        // Debug.Log("Changed Script Enabledness");
        PlayerIsWhite = !PlayerIsWhite;
        if (STATE == 0)
        {
            STATE = 1;
        }
        else{
            STATE = 0;
        }
    }

    void SpawnPiece(int index, int x, int y, int rotation = 0)
    {
        Quaternion orientation = Quaternion.Euler(0, rotation, 0);
        GameObject temp = Instantiate(chessPrefabs[index], GetTileCentre(x, y), orientation) as GameObject;
        temp.transform.SetParent(transform);
        board[x, y] = temp.GetComponent<Piece>();
        board[x, y].SetPosition(x, y);
        activePieces.Add(temp);
        board[x, y].Object = temp;

        //if (XRScript.CanBeAppliedTo(temp.GetComponent<XROffsetGrabInteractable>()))
        //    XRScript.ApplyTo(temp.GetComponent<XROffsetGrabInteractable>());
    }



    void SpawnAllPieces()
    {
        board = new Piece[8, 8];
        EnPassant = new int[2] {-1, -1};


        // White

        SpawnPiece(0, 4, 0);     // King
        SpawnPiece(1, 3, 0);     // Queen
        SpawnPiece(2, 0, 0);     // Rook
        SpawnPiece(2, 7, 0);     // Rook
        SpawnPiece(3, 1, 0);     // Knight
        SpawnPiece(3, 6, 0);     // Knight
        SpawnPiece(4, 2, 0);     // Bishop
        SpawnPiece(4, 5, 0);     // Bishop
        for (int i = 0; i < 8; ++i)
        {
            SpawnPiece(5, i, 1); // Pawn
        }

        // Black

        SpawnPiece(0 + 6, 4, 7);     // King
        SpawnPiece(1 + 6, 3, 7);     // Queen
        SpawnPiece(2 + 6, 0, 7);     // Rook
        SpawnPiece(2 + 6, 7, 7);     // Rook
        SpawnPiece(3 + 6, 1, 7, rotation: 180);     // Knight
        SpawnPiece(3 + 6, 6, 7, rotation: 180);     // Knight
        SpawnPiece(4 + 6, 2, 7);     // Bishop
        SpawnPiece(4 + 6, 5, 7);     // Bishop
        for (int i = 0; i < 8; ++i)
        {
            SpawnPiece(5 + 6, i, 6); // Pawn
        }



    }

    Vector3 GetTileCentre(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += TILE_SIZE * x + TILE_SIZE / 2;
        origin.z += TILE_SIZE * y + TILE_SIZE / 2;

        return origin;
    }

}
