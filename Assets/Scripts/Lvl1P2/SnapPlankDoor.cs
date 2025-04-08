using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [Header("Audio")]
    public AudioSource source;
    public AudioClip placePlank;
    public DialogueScene lockedPlankScene;

    void Start()
    {
        lockPlank.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
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

    private void OnEnable()
    {
        Debug.Log("Timer started");
        StartCoroutine(SnapPlankDoorTimer(timer));
    }

    private IEnumerator SnapPlankDoorTimer(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isSnapped)
        {
        }
        else
        {
            // Put gameOver Script in this
            gameOver.SetActive(true);
        }
    }
}
