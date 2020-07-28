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
}
