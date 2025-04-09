using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class UIPlayer : MonoBehaviour
{
    public GameObject interact;
    public GameObject grab;
    public GameObject throw_;
    public GameObject Done1;
    public GameObject Done2;
    public GameObject Done3;
    public GameObject panel;

    public void Start()
    {
        //interact = GameObject.Find("Interact");
        //grab = GameObject.Find("Grab");
        //throw_ = GameObject.Find("Throw");
        //Done1 = GameObject.Find("Done1");
        //Done2 = GameObject.Find("Done2");
        //Done3 = GameObject.Find("Done3");
        //panel = GameObject.Find("PanelPlayer");
        //if(panel != null)
        //{
        //    Debug.Log("Panel found");
        //}


    }

    public void Grab()
    {
        Debug.Log("Button1 Clicked");
        grab.SetActive(false);
        Done1.SetActive(false);
        //panel.SetActive(false);
    }

 
    public void Throw()
    {
        throw_.SetActive(false);
        Done2.SetActive(false);
        //panel.SetActive(false);
    }

    public void Interact()
    {
        interact.SetActive(false);
        Done3.SetActive(false);
        panel.SetActive(false);
    }
}