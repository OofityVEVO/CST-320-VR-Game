using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGuards : MonoBehaviour
{
    [SerializeField] GameObject[] Guards;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player Collided");
            foreach (GameObject Guard in Guards)
            {
                Guard.SetActive(true);
                Guard.GetComponent<ToLocation>().ToFlag();
            }
        }

        Destroy(gameObject);
    }
}
