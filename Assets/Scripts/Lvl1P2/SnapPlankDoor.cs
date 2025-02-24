using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SnapPlankDoor : MonoBehaviour
{
    public Transform snapTarget; 
    public float snapThreshold = 0.2f; 


    public GameObject lockPlank;

    private AudioSource source;
    public AudioClip placePlank;


    void Start()
    {
        lockPlank.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Plank") 
        {
            lockPlank.SetActive(true);
            Destroy(other.gameObject);
            source.PlayOneShot(placePlank);
            

            
        }
    }


}
