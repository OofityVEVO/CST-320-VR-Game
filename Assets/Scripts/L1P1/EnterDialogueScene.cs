using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDialogueScene : MonoBehaviour
{
    public DialogueScene dialogueScene;
    private GameObject panel;

    void Start()
    {
        panel = GameObject.Find("PanelPlayer");
    }
    void OnTriggerEnter()
    {
        dialogueScene.PlayAudio();
        panel.SetActive(true);

    }
}
