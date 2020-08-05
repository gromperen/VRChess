using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{

    public override bool[,] PossibleMoves()
    {
        bool[,] ret = new bool [8,8];

        int i, j;
        Piece p;

        // UP - RIGHT
        
        i = CurrentX + 1;
        j = CurrentY + 1;

        while (i < 8 && j < 8)
        {
            p = _BoardManager.board[i, j];
            if (p != null)
            {
                if (p.isWhite != isWhite)
                {
                    ret[i, j] = true;
                }
                break;
            }
            ret[i, j] = true;
            i++;
            j++;
        }

        // UP - LEFT
        
        i = CurrentX - 1;
        j = CurrentY + 1;

        while (i >= 0 && j < 8)
        {
            p = _BoardManager.board[i, j];
            if (p != null)
            {
                if (p.isWhite != isWhite)
                {
                    ret[i, j] = true;
                }
                break;
            }
            ret[i, j] = true;
            i--;
            j++;
        }
        
        // DOWN - LEFT
        
        i = CurrentX - 1;
        j = CurrentY - 1;

        while (i >= 0 && j >= 0)
        {
            p = _BoardManager.board[i, j];
            if (p != null)
            {
                if (p.isWhite != isWhite)
                {
                    ret[i, j] = true;
                }
                break;
            }
            ret[i, j] = true;
            i--;
            j--;
        }
        
        // DOWN - RIGHT 
        
        i = CurrentX + 1;
        j = CurrentY - 1;

        while (i < 8 && j >= 0)
        {
            p = _BoardManager.board[i, j];
            if (p != null)
            {
                if (p.isWhite != isWhite)
                {
                    ret[i, j] = true;
                }
                break;
            }
            ret[i, j] = true;
            i++;
            j--;
        }

        return ret;
    }
}
