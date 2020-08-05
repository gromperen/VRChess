using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override bool[,] PossibleMoves()
    {
        bool[,] ret = new bool[8,8];
        bool[,] bish = BishopMoves();
        bool[,] rok = RookMoves();
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (bish[i, j] || rok[i, j])
                {
                    ret[i, j] = true;
                }
            }
        }
        return ret;
    }
    bool[,] BishopMoves()
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
    bool[,] RookMoves()
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
