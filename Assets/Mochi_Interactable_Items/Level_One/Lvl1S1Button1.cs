using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1S1Button1 : MochiInteractBase
{
    public BigDoorPuzzle puzzle;

    public override void Interact()
    {
        puzzle.pressButton1();
        Debug.Log("Pressing the button");
    }
}
