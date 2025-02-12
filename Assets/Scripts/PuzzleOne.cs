using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class PuzzleOne : MonoBehaviour
{
    public UnityEvent MochiEvent1;

    [Header("Door Settings")]
    public GameObject door;
    public float openBot;
    public float closeBot;
    public float speed;
    public bool opening;

    void Start()
    {
        MochiEvent1 = new UnityEvent(); 
        MochiEvent1.AddListener(PuzzleSolve); 
    }


    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.CompareTag("Chitterkin")) 
        {
            MochiEvent1.Invoke();
            MochiEvent1.RemoveListener(PuzzleSolve); 
        }
    }

    void PuzzleSolve()
    {
        Debug.Log("Mochi Escaped!! :DD");

        Vector3 currentBot = door.transform.localEulerAngles;

        while (currentBot.y < openBot)
        {
            door.transform.localEulerAngles = Vector3.Lerp(currentBot, new Vector3(currentBot.x, openBot, currentBot.z), speed * Time.deltaTime);
        }
    }

    void OpenDoor()
    {


    }

}
