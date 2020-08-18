using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChessEngine : MonoBehaviour
{
    private BoardManager _BoardManager;
    System.Random rand = new System.Random();

    private void Start()
    {
        _BoardManager = this.GetComponent<BoardManager>();
    }
    public int[] RandomMove()
    {
        int [] ret = new int[4];

        List <Piece> mine = new List<Piece>();
        
        for (int i = 0; i < 8; ++i) 
        {
            for (int j = 0; j < 8; ++j)
            {
                if (_BoardManager.board[i, j] != null && _BoardManager.board[i, j].isWhite != _BoardManager.PlayerIsWhite)
                {
                    if (_BoardManager.board[i,j].PossibleMoves() != new bool[8,8])
                        mine.Add(_BoardManager.board[i, j]);
                }
            }
        }

        int r = rand.Next(mine.Count);

        ret[0] = mine[r].CurrentX;
        ret[1] = mine[r].CurrentY;
        bool[,] tempmoves = mine[r].PossibleMoves();

        int [,] movecords = new int[64, 2];
        int cntmoves = 0;

        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (tempmoves[i, j])
                {
                    movecords[cntmoves, 0] = i;
                    movecords[cntmoves, 1] = j;

                    cntmoves++;

                }
            }
        }
        r = rand.Next(cntmoves);

        ret[2] = movecords[r, 0];
        ret[3] = movecords[r, 1];


        return ret;
    }
}
