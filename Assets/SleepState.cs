using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : MonoBehaviour
{
    public soldierAnimation SoldierAnimation;
    //public DialogueScene guardsSleepingScene;

    public bool isAsleep = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SleepPotion"))
        {
            isAsleep = true;
            SoldierAnimation.isWalking = false;
            SoldierAnimation.isRunning = false;
            SoldierAnimation.isSleep = true;

            //guardsSleepingScene.PlayAudio();
        }
    }
}
