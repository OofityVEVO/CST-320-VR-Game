using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mochiAnimation : MonoBehaviour
{
    private Animator mochiAnimator;
    private Vector3 lastPosition;
   
   

    // Start is called before the first frame update
    void Start()
    {
        mochiAnimator = GetComponent<Animator>();
        lastPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != lastPosition)
        {
            mochiAnimator.Play("Walk");
            lastPosition = transform.position;
        }
        else
        {
            mochiAnimator.Play("Idle");
        }
    }

}
