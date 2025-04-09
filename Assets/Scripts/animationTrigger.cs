using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationTrigger : MonoBehaviour
{
    [SerializeField] public GameObject guard1;
    [SerializeField] public GameObject guard2;
    [SerializeField] public GameObject timer;

    public DialogueScene guardSpotScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (guardSpotScene != null)
            {
                guardSpotScene.PlayAudio();
            }

            //runs animations for the guards once the player passes the door
            guard1.GetComponent<BalconyAnimation>().enabled = true;
            guard2.GetComponent<BalconyAnimation>().enabled = true;
            timer.SetActive(true);

            Debug.Log("Collision Detected");
            
        }
    }
}
