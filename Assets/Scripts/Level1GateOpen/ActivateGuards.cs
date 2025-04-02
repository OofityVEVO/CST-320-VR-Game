using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
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
                GameObject child = Guard.transform.Find("guard")?.gameObject;

                Guard.SetActive(true);
                child.SetActive(true);

                child.GetComponent<ToLocationGuard>().Begin();
            }
        }

        Destroy(gameObject);
    }
}
