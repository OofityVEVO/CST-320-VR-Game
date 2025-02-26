using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class walking : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;
    // Start is called before the first frame update

    public int speed = 2;
    public bool isRunning = false;
    public bool isPoint = false;
    public bool isBoogie = false;
    private float crossfadeTime = 0.1f;
    private string currAnimation = "idle";


    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
        
        
        

    }

    // Update is called once per frame
    void Update()
    {
       
        //tests to determine the animations of the guard
        if (transform.position.z >= 10)
        {
           Vector3 movement = new Vector3(0, 0, 0);
        }
        else
        {
            Vector3 movement = new Vector3(0, 0, speed);
            transform.position += movement * Time.deltaTime;
        }

        
        if (isPoint == true)
        {
            changeAnimation("point");
            isPoint = false;
            
        }

        if (isBoogie == true)
        {
            changeAnimation("boogie");
            isBoogie = false;
        }

        checkAnimation();
        // Update the last position
        lastPosition = transform.position;
    }

    public void changeAnimation(string animation, float time = 0)
    {
        
        if(time > 0)
        {
            StartCoroutine(Wait());
        }

        else
        {
            Validate();
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(time - crossfadeTime);
            Validate();
        }

        void Validate()
        {
            if (currAnimation != animation)
            {

                if (currAnimation == "")
                {
                    checkAnimation();
                }
                else
                {
                    currAnimation = animation;
                    animator.CrossFade(animation, crossfadeTime);
                }
            }
        }

       
    }

     void checkAnimation()
    {
        if (currAnimation == "point")
        {
            return;
        }

        if (currAnimation == "boogie")
        {
            return;
        }
        // Check if the guard is moving

        if (isRunning == true)
        {
            changeAnimation("running");
            Debug.Log("running");
        }

        else
        {
            if (transform.position != lastPosition)
            {

                changeAnimation("walking");


            }

            else
            {
                changeAnimation("idle");

            }

        }
       
    }

   
}

