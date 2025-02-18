using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    
    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(1);
        }

    }

}
