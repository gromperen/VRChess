using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour
{
    private BoardManager _BoardManager;
    private void Start()
    {
        _BoardManager = gameObject.GetComponent<BoardManager>();
    }
    
    public void HighlightSquares(bool[,] moves)
    {
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (moves[i, j])
                {
                    _BoardManager.snapZones[i, j].GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }   
    }

    public void HideAllHighlights()
    {
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                _BoardManager.snapZones[i, j].GetComponent<MeshRenderer>().enabled = false;
            }
        } 
    }
    public void HideHighlights(bool [,] moves)
    {
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                if (moves[i,j])
                    _BoardManager.snapZones[i, j].GetComponent<MeshRenderer>().enabled = false;
            }
        } 
    }
}
