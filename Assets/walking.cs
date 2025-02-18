using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class walking : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;
    // Start is called before the first frame update

    public int speed = 2;
    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
        int speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //tests to determine the animations of the guard
        if (transform.position.z >= 0)
        {
           Vector3 movement = new Vector3(0, 0, 0);
        }
        else
        {
            Vector3 movement = new Vector3(0, 0, speed);
            transform.position += movement * Time.deltaTime;
        }

        // Check if the guard is moving
        if (transform.position != lastPosition)
        {
            
            animator.SetBool("IsMoving", true);
            Debug.Log("swapped to walking");// Play walking animation
        }
        else
        {
           
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsRunning", false);
            Debug.Log("swapped to idle");
        }

       if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsRunning", true);
            Debug.Log("swapped to running");
            speed *= 2;
        }
       


        // Update the last position
        lastPosition = transform.position;
    }
}
