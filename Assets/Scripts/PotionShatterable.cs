using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShatterable : MonoBehaviour
{
    [SerializeField] GameObject sphereRadius;
    public float forceThreshold = 10f;
    public float radius = 30f;

    private void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce > forceThreshold)
        {
            createSoundSphere(collision);
            Destroy(gameObject);
        }
    }

    private void createSoundSphere(Collision collision)
    {
        Vector3 collisionPoint = collision.contacts[0].point;

        GameObject sphere = Instantiate(sphereRadius, collisionPoint, Quaternion.identity);
        sphere.transform.localScale = new Vector3(radius / 2, radius, radius / 2);
        Destroy(sphere, 0.2f);
    }
}
