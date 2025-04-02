using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameOver : MonoBehaviour
{
    [SerializeField] GameObject XROrigin;
    PlayerGameOver gameOverScript;

    private void OnEnable()
    {
        gameOverScript = XROrigin.GetComponent<PlayerGameOver>();

        gameOverScript.TriggerGameOver();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        gameOverScript.TriggerGameOver();
    //    }
    //}
}
