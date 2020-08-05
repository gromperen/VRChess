using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override bool[,] PossibleMoves()
    {
        return new bool[8,8];
    }

}
