using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override bool[,] PossibleMoves()
    {
        bool[,] ret = new bool[8,8];
        Piece p;
        int i;

        // Right

        for (i = CurrentX + 1; i < 8; ++i)
        {

            p = _BoardManager.board[i, CurrentY];
            if (p != null)
            {
                if (p.isWhite != isWhite)
                {
                    ret[i, CurrentY] = true;
                }
                break;
            }
            else
            {
                ret[i, CurrentY] = true;
            }
        }
        // Left

        for (i = CurrentX - 1; i >= 0; --i)
        {

            p = _BoardManager.board[i, CurrentY];
            if (p != null)
            {
                if (p.isWhite != isWhite)
                {
                    ret[i, CurrentY] = true;
                }
                break;
            }
            else
            {
                ret[i, CurrentY] = true;
            }
        }

        // Down
        for (i = CurrentY - 1; i >= 0; --i)
        {
            p = _BoardManager.board[CurrentX, i];
            if (p != null)
            {
                if (p.isWhite != isWhite)
                {
                    ret[CurrentX, i] = true;
                }
                break;
            }
            else
            {
                ret[CurrentX, i] = true;
            }
        }
        
        // Up
        for (i = CurrentY + 1; i < 8; ++i)
        {

            p = _BoardManager.board[CurrentX, i];
            if (p != null)
            {
                if (p.isWhite != isWhite)
                {
                    ret[CurrentX, i] = true;
                }
                break;
            }
            else
            {
                ret[CurrentX, i] = true;
            }
        }

        return ret;
    }

}
