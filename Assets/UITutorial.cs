using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class UITutorial : MonoBehaviour
{
    private GameObject click;
    private GameObject Turn;
    private GameObject Walk;
    private GameObject Next1;
    private GameObject Next2;
    private GameObject Grab;

    private void Start()
    {
        Grab = GameObject.Find("Grab");
        click = GameObject.Find("Click");
        Turn = GameObject.Find("Turn");
        Walk = GameObject.Find("Walk");
        Next1 = GameObject.Find("Next1");
        Next2 = GameObject.Find("Next2");
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("M_Dungeon");
    }

    public void Click()
    {
        click.SetActive(false);
        Next1.SetActive(false);

    }

    public void walk()
    {
        Walk.SetActive(false);
        Next1.SetActive(false);
    }

    public void grab()
    {
        Grab.SetActive(false);
        Next1.SetActive(false);
    }

    public void turn()
    {
        Turn.SetActive(false);
        Next2.SetActive(false);
    }
}