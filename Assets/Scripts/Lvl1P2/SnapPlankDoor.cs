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
    public GameObject gameOver;

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
            isSnapped = true;
            Debug.Log("Plank is snapped");

        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        /*
        if (isSnapped)
        {

        }
        else
        {
            // Put gameOver Script in this
            gameOver.enabled = true;
        }*/
    }




}
