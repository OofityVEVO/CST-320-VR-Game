using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public string sceneName;
    void OnCollisionEnter(Collision collision)
    {
        // If the player collides with the object, load the scene
        if (collision.gameObject.tag == "Player")
        SceneManager.LoadScene("AF_Armory");
        Debug.Log("Collision");


    }

}
