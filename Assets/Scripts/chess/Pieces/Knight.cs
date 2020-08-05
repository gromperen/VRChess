using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override bool[,] PossibleMoves()
    {
        int[] X = { 2, 1, -1, -2, -2, -1, 1, 2 }; 
        int[] Y = { 1, 2, 2, 1, -1, -2, -2, -1 }; 
        bool[,] ret = new bool [8,8];

        for (int i = 0; i < 8; ++i)
        {
            int x = CurrentX + X[i];
            int y = CurrentY + Y[i];

            if (x >= 0 
            && y >= 0 
            && x < 8 
            && y < 8)
            {
                Piece p = _BoardManager.board[x, y];
                if (p == null
                || p.isWhite != isWhite)
                {
                    ret[x, y] = true;
                }
            }

        }
        
        return ret;
    }

}
