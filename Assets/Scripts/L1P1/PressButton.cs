using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    public BigDoorPuzzle puzzle;
    public int id;
    
    public void PressButton1()
    {
        puzzle.pressButton(0);
    }
}
