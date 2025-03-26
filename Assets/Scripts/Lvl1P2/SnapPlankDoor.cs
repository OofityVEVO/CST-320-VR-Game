using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SnapPlankDoor : MonoBehaviour
{
    public Transform snapTarget; 
    public float snapThreshold = 0.2f;
    public float timer = 10f;
    public bool isSnapped = false;


    public GameObject lockPlank;

    private AudioSource source;
    public AudioClip placePlank;


    void Start()
    {
        lockPlank.SetActive(false);
    }

    void Update()
    {

        if (!isSnapped) { 
            if (timer <= 0)
            {
                //Debug.Log("Time is up");
            }

            else
            {
                timer -= Time.deltaTime;
               
            }
        }

        else
        {
            Debug.Log("Plank is snapped");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Plank") 
        {
            lockPlank.SetActive(true);
            Destroy(other.gameObject);
            source.PlayOneShot(placePlank);
            isSnapped = true;
            Debug.Log("Plank is snapped");



        }
    }

   


}
