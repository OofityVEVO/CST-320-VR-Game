using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialogueScene : MonoBehaviour
{
    public DialogueScene dialogueScene;

   void OnTriggerEnter()
    {
        dialogueScene.PlayAudio();
    }
}
