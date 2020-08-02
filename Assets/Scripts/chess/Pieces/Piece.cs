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
}
