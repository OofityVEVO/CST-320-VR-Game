using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSleepingPowder : MonoBehaviour
{
    public DialogueScene enterConRoom;

    void OnTriggerEnter(Collider collide)
    {
        enterConRoom.PlayAudio();
    }
}
