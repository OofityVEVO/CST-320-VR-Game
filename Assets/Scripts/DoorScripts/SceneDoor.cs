using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoor : MonoBehaviour
{
    public bool hasKey = false;
    private GameObject nextScene;
    private Animator door;


    // Start is called before the first frame update

    void Start()
    {
        //initializes object and animator
        nextScene = transform.Find("nextScene").gameObject;
        door = GetComponentInChildren<Animator>();

        

    }

    // Update is called once per frame
    void Update()
    {
        //checks if player has key, if so, opens door
        if (hasKey == false)
        {
            nextScene.SetActive(false);
        }
        else
        {
            nextScene.SetActive(true);
            door.Play("DoorOpen");
        }
    }
}
