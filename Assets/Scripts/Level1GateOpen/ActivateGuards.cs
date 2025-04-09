using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ActivateGuards : MonoBehaviour
{
    [SerializeField] GameObject[] Guards;
    [SerializeField] GameObject MissPlane;

    public DialogueScene GuardsHalt;

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

            GuardsHalt.PlayAudio();


            MissPlane.SetActive(true);
            Destroy(gameObject);
        }
    }
}
