using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialogueScene : MonoBehaviour
{
    public DialogueScene dialogueScene;
    public DialogueScene previousScene;
    private GameObject panel;

    void Start()
    {
        panel = GameObject.Find("PanelPlayer");
    }
    void OnTriggerEnter()
    {
        previousScene.StopAudio();
        dialogueScene.PlayAudio();
        //panel.SetActive(true);

    }
}
