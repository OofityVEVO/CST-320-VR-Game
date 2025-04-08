using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class UIMainMenu : MonoBehaviour
{
   

   

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Loading");
    }

   
}
