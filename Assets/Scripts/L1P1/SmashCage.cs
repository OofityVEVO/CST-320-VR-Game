using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SmashCage : MonoBehaviour
{
    #region Variables

    public int hitsToDestroy = 5;
    public float forceThreshold = 10f;



    #endregion

    public UnityEvent event1;
    public GameObject realMochi;

    void Start()
    {
        event1 = new UnityEvent();

        event1.AddListener(WakeGameObject);
        realMochi.SetActive(false); 
    }

    // ### Unity Functions ###
    void Update()
    {
        if (hitsToDestroy <= 0)
        {
            event1.Invoke();
            event1.RemoveListener(WakeGameObject);
            Destroy(gameObject);
        }
            

    }


    // ### Component Functions ###
    private void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce > forceThreshold)
        {
            hitsToDestroy--;
        }
    }


    private void WakeGameObject()
    {
        realMochi.SetActive(true);
    }

}
