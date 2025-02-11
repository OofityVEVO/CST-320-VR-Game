using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    #region Variables

    public int hitsToDestroy = 5;
    public float forceThreshold = 10f;

    #endregion

    // ### Unity Functions ###
    void Update()
    {
        if (hitsToDestroy <= 0)
            Destroy(gameObject);
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
}
