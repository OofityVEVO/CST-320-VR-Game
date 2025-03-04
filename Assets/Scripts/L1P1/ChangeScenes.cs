using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public string sceneName = "AF_Armory";
    void OnCollisionEnter()
    {
        
        
        SceneManager.LoadScene("AF_Armory");
        Debug.Log("Collision");


    }

}
