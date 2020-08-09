using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override bool[,] PossibleMoves()
    {
        bool[,] ret = new bool[8,8];
        Piece p;
        int i, j;
        
        // UP
        if (CurrentY != 7)
        {
            j = CurrentY + 1;
            
            i = System.Math.Max(CurrentX - 1, 0);
            for (; i < 8 && i <= CurrentX + 1; ++i)
            {
                p = _BoardManager.board[i, j];
                if (p == null || p.isWhite != isWhite)
                {
                    ret[i, j] = true;
                }
            }
        }
        
        // Down
        if (CurrentY != 0)
        {
            j = CurrentY - 1;
            
            i = System.Math.Max(CurrentX - 1, 0);
            for (; i < 8 && i <= CurrentX + 1; ++i)
            {
                p = _BoardManager.board[i, j];
                if (p == null || p.isWhite != isWhite)
                {
                    ret[i, j] = true;
                }
            }
        }

        // Left
        if (CurrentX != 0)
        {
            p = _BoardManager.board[CurrentX - 1, CurrentY];
            if (p == null || p.isWhite != isWhite)
            {
                ret[CurrentX - 1, CurrentY] = true;
            }
        }

        // Right
        if (CurrentX != 7)
        {
            p = _BoardManager.board[CurrentX + 1, CurrentY];
            if (p == null || p.isWhite != isWhite)
            {
                ret[CurrentX + 1, CurrentY] = true;
            }
        }


        

        return ret;
        
    }

}
