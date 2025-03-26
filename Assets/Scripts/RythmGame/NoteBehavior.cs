using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    NoteManager manager;
    NoteClicker clicker;

    float scrollSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindAnyObjectByType<NoteManager>();
        scrollSpeed = manager.GetScrollSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        float newY = (float)(transform.position.y - scrollSpeed);
        float newZ = (float)(transform.position.z - scrollSpeed);

        Vector3 newPosition = new Vector3(transform.position.x, newY, newZ);
        transform.SetPositionAndRotation(newPosition, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NoteCleaner"))
        {
            manager.AddScore(-50);
            manager.AddMiss();
            Destroy(gameObject);
        }
        else if(other.CompareTag("NoteClick")) 
        {
            clicker = other.GetComponent<NoteClicker>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NoteClick"))
        {   
            if (clicker.GetIsClicked())
            {
                manager.AddScore(150);
                manager.AddHit();
                Destroy(gameObject);
            }
        }
    }
}
