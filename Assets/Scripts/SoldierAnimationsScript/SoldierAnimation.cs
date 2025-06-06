using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;
using static Unity.VisualScripting.Member;

public class soldierAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;
    private AudioSource audio;
    private Transform child;
    // Start is called before the first frame update

   
    public bool isRunning = false;
    public bool isPoint = false;
    public bool isBoogie = false;
    public bool isSleep = false;
    public bool isWalking = false;
    private float crossfadeTime = 0.1f;
    private string currAnimation = "idle";
 


    void Start()
    {
        //intializes animator
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        if(audio == null)
        {
            child = transform.GetChild(0);
            audio = child.GetComponent<AudioSource>();
        }

        //sets current position to determine if the soldier is moving
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //runs point animation if point is true, then sets point to false
        if (isPoint == true)
        {
            changeAnimation("point");
            isPoint = false;
            
        }

        //runs boogie animation if boogie is true, then sets boogie to false
        if (isBoogie == true)
        {
            changeAnimation("boogie");
            isBoogie = false;
        }

        if (isSleep == true)
        {
            changeAnimation("sleep");
        }

        if (isRunning == true)
        {
            playAudio(1.7f);
        }
        else if (isWalking == true)
        {
            playAudio(1.4f);
        }

        else
        {
            StopAudio();
        }


        checkAnimation();

        // Update the last position
        lastPosition = transform.position;
    }
    //changes the animation from one to the other
    public void changeAnimation(string animation, float time = 0)
    {
        // checks if the time is greater than 0, if so it will wait for the time to pass before running the animation
        if (time > 0)
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

       // double checks current animation then changes animation
        void Validate()
        {
            if (currAnimation != animation)
            {
                //checks if current animation is null
                if (currAnimation == "")
                {
                    checkAnimation();
                }

                //changes animation with crossfade so that it transitions smoothly
                else
                {
                    currAnimation = animation;
                    animator.CrossFade(animation, crossfadeTime);
                }
            }
        }

       
    }

    //checks the animation to see what is currently playing, will run correct animation based on the current state of the soldier
    public void checkAnimation()
    {
        if (currAnimation == "point")
        {
            return;
        }

        if (currAnimation == "boogie")
        {
            return;
        }

        if (currAnimation == "sleep")
        {
            return;
        }
        // Check if the guard is moving

        if (isRunning == true)
        {
            changeAnimation("running");
            

        }
        else if (isWalking == true)
        {
            
            changeAnimation("walking");
           
            
        }
    }   

    void playAudio(float pitch)
    {
        if (!audio.isPlaying)
        {
            audio.Play();
        }
        audio.pitch = pitch;
    }

    void StopAudio()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
    }
}

