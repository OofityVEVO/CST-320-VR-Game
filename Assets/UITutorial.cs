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
    private GameObject turn;
    private GameObject Walk;
    private GameObject Next1;
    private GameObject Next2;

    private void Start()
    {
        click = GameObject.Find("Click");
        turn = GameObject.Find("Turn");
        Walk = GameObject.Find("Walk");
        Next1 = GameObject.Find("Next1");
        Next2 = GameObject.Find("Next2");



    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Level1Scene1");
    }

    public void Turn()
    {
        click.SetActive(false);
        Next1.SetActive(false);

    }

    public void walk()
    {
        turn.SetActive(false);
        Next2.SetActive(false);
    }
}