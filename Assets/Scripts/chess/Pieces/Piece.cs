using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int CurrentX{set;get;}
    public int CurrentY{set;get;}
    public int Value;
    public bool isWhite;
    public GameObject Object;
    private BoardManager _BoardManager;
    private BoardHighlights _BoardHighlights;

    private void Start()
    {
        _BoardManager = this.transform.parent.GetComponent<BoardManager>();
        _BoardHighlights = this.transform.parent.GetComponent<BoardHighlights>();
    }
    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public bool[,] PossibleMoves()
    {
        bool[,] ret = new bool[8,8]; 
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j <8; ++j)
            {
                ret[i,j] = true;
            }
        }
        return ret;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Hands")
        {
            _BoardHighlights.HighlightSquares(_BoardManager.board[0,0].PossibleMoves());
        }
    }
}
