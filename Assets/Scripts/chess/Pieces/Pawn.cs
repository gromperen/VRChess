using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override bool[,] PossibleMoves()
    {
        Piece p;
        bool [,] ret = new bool[8,8];
        if (isWhite)
        {
            // Diagonal Left
            if (CurrentX != 0 && CurrentY != 7) 
            {
                p = _BoardManager.board[CurrentX - 1, CurrentY + 1];
                if (p != null && !p.isWhite)
                {
                    ret[CurrentX - 1, CurrentY + 1] = true;
                }
            }
            
            // Diagonal Right
            if (CurrentX != 7 && CurrentY != 7) 
            {
                p = _BoardManager.board[CurrentX + 1, CurrentY + 1];
                if (p != null && !p.isWhite)
                {
                    ret[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // Forward
            if (CurrentY != 7)
            {
                if (_BoardManager.board[CurrentX, CurrentY + 1] == null)
                {
                    ret[CurrentX, CurrentY + 1] = true;
                }
            }

            // First move
            if (CurrentY == 1)
            {
                if (_BoardManager.board[CurrentX, CurrentY + 1] == null && _BoardManager.board[CurrentX, CurrentY + 2] == null)
                {
                    ret[CurrentX, CurrentY + 2] = true;
                }   
            }
        }
        else
        {
            // Diagonal Right
            if (CurrentX != 0 && CurrentY != 0) 
            {
                p = _BoardManager.board[CurrentX - 1, CurrentY - 1];
                if (p != null && p.isWhite)
                {
                    ret[CurrentX - 1, CurrentY - 1] = true;
                }
            }
            
            // Diagonal Left
            if (CurrentX != 7 && CurrentY != 0) 
            {
                p = _BoardManager.board[CurrentX + 1, CurrentY - 1];
                if (p != null && p.isWhite)
                {
                    ret[CurrentX + 1, CurrentY - 1] = true;
                }
            }

            // Forward
            if (CurrentY != 0)
            {
                if (_BoardManager.board[CurrentX, CurrentY - 1] == null)
                {
                    ret[CurrentX, CurrentY - 1] = true;
                }
            }

            // First move
            if (CurrentY == 6)
            {
                if (_BoardManager.board[CurrentX, CurrentY - 1] == null && _BoardManager.board[CurrentX, CurrentY - 2] == null)
                {
                    ret[CurrentX, CurrentY - 2] = true;
                }   
            }
        }
        return ret;
    }
    

}
