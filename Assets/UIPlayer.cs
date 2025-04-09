using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class UIPlayer : MonoBehaviour
{
    private GameObject interact;
    private GameObject grab;
    private GameObject throw_;
    private GameObject commands;
    private GameObject Done1;
    private GameObject Done2;
    private GameObject Done3;
    private GameObject Next1;
    private GameObject panel;

    public void Start()
    {
        interact = GameObject.Find("Interact");
        grab = GameObject.Find("Grab");
        throw_ = GameObject.Find("Throw");
        commands = GameObject.Find("MochiCommands");
        Done1 = GameObject.Find("Done1");
        Done2 = GameObject.Find("Done2");
        Done3 = GameObject.Find("Done3");
        Next1 = GameObject.Find("Next1");
        panel = GameObject.Find("PanelPlayer");
        if(panel != null)
        {
            Debug.Log("Panel found");
        }


    }

    public void Grab()
    {
        grab.SetActive(false);
        Done1.SetActive(false);
        panel.SetActive(false);
    }

 
    public void Throw()
    {
        throw_.SetActive(false);
        Done2.SetActive(false);
        panel.SetActive(false);
    }

    public void Interact()
    {
        interact.SetActive(false);
        Next1.SetActive(false);
        
    }

    public void Commands()
    {
        commands.SetActive(false);
        Done3.SetActive(false);
        panel.SetActive(false);
    }
}