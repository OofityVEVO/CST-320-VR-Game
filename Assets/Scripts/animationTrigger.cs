using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationTrigger : MonoBehaviour
{

    [SerializeField] public GameObject guard1;
    [SerializeField] public GameObject guard2;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //runs animations for the guards once the player passes the door
            guard1.GetComponent<BalconyAnimation>().enabled = true;
            guard2.GetComponent<BalconyAnimation>().enabled = true;

            Debug.Log("Collision Detected");
        }
       
    }
}
