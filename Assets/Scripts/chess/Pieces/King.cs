using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override bool[,] PossibleMoves()
    {
        bool[,] ret = new bool[8, 8];
        Piece p;
        int i, j;
        bool[,] attacked = _BoardManager.AttackedSquares(!isWhite, false);
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


        // Castling
        if (isWhite)
        {
            if (_BoardManager.WhiteCastleRight)
            {
                // Short castle
                if (_BoardManager.WhiteCastleShort)
                {
                    bool cancastle = true;
                    for (i = 6; i < 7; ++i)
                    {
                        if (_BoardManager.board[i, 0] != null)
                        {
                            cancastle = false;
                        }
                    }
                    for (i = 5; i < 8; ++i)
                    {
                        if (attacked[i, 0])
                        {
                            cancastle = false;
                        }
                    }
                    if (cancastle)
                    {
                        ret[6, 0] = true;
                    }
                }


                // Long castle
                if (_BoardManager.WhiteCastleLong)
                {
                    bool cancastle = true;
                    for (i = 1; i < 4; ++i)
                    {
                        if (_BoardManager.board[i, 0] != null)
                        {
                            cancastle = false;
                        }
                    }

                    for (i = 0; i <=4; ++i)
                    {
                        if (attacked[i,0])
                        {
                            cancastle = false;
                        }
                    }
                    if (cancastle)
                    {
                        ret[2, 0] = true;
                    }
                }
            }
        }
        else
        {
            if (_BoardManager.BlackCastleRight)
            {
                // Short castle
                if (_BoardManager.BlackCastleShort)
                {
                    bool cancastle = true;
                    for (i = 6; i < 7; ++i)
                    {
                        if (_BoardManager.board[i, 7] != null)
                        {
                            cancastle = false;
                        }
                    }
                    for (i = 5; i < 8; ++i)
                    {
                        if (attacked[i, 7])
                        {
                            cancastle = false;
                        }
                    }
                    if (cancastle)
                    {
                        ret[6, 7] = true;
                    }
                }


                // Long castle
                if (_BoardManager.BlackCastleLong)
                {
                    bool cancastle = true;
                    for (i = 1; i < 4; ++i)
                    {
                        if (_BoardManager.board[i, 7] != null)
                        {
                            cancastle = false;
                        }
                    }

                    for (i = 0; i <=4; ++i)
                    {
                        if (attacked[i,7])
                        {
                            cancastle = false;
                        }
                    }
                    if (cancastle)
                    {
                        ret[2, 7] = true;
                    }
                }
            }
        }

        // remove attacked squares
        for (i = 0; i < 8; ++i)
        {
            for (j = 0; j < 8; ++j)
            {
                if (attacked[i, j])
                {
                    ret[i, j] = false;
                }
            }
        }
        ret[CurrentX, CurrentY] = false;

        return ret;

    }

}
