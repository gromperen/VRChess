using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BoardManager : MonoBehaviour
{
    public int STATE = 0; // 0 = White, 1 = Black, 2 = Game Over
    public bool PlayerIsWhite = true;

    public Piece[,] board { set; get; }
    private Piece selectedPiece;
    public GameObject[,] snapZones {set; get;}
    private const float TILE_SIZE = 1.0f;

    public GameObject snapPrefab;
    public List<GameObject> chessPrefabs;
    public static List<GameObject> activePieces = new List<GameObject>();
    public XROffsetGrabInteractable XROffsetGrabInteractableScript;


    // Start is called before the first frame update
    void Start()
    {
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
        /*
        if (selectedPiece == null)
        {
            UpdateSelection();
        }
        else
        {
            if (!checkValidPieceSelection())
            {
                selectedPiece = null;
            }
        }
        */
    }

    bool checkValidPieceSelection()
    {
        if (STATE != 0 || (PlayerIsWhite != selectedPiece.isWhite))
        {
            return false;
        }
        return true;
    }

    void UpdateSelection()
    {
        foreach (GameObject i in activePieces)
        {
            if (Mathf.RoundToInt(i.transform.position.y) != 0)
            {
                selectedPiece = board[Mathf.RoundToInt(i.transform.position.x), Mathf.RoundToInt(i.transform.position.z)];
                break;
            }
        }
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

    public void InitTurn()
    {
        bool isPlayerTurn;
        if ((STATE == 0 && !PlayerIsWhite) || (STATE == 1 && PlayerIsWhite)) {
            isPlayerTurn = false;
        }
        else{
            isPlayerTurn = true;
        }
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
               if (board[i, j] != null)
               {
                   /*
                        Add function to get turns and disable Grab if turns are 0
                   */
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
    }



    void SpawnAllPieces()
    {
        board = new Piece[8, 8];

        // White

        SpawnPiece(0, 3, 0);     // King
        SpawnPiece(1, 4, 0);     // Queen
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

        SpawnPiece(0 + 6, 3, 7);     // King
        SpawnPiece(1 + 6, 4, 7);     // Queen
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
