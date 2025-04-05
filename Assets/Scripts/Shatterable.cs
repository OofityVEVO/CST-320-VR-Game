using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatterable : MonoBehaviour
{
    [SerializeField] GameObject sphereSound;
    public float forceThreshold = 10f;
    public float soundRadius = 30f;

    bool hasSpawned = false;
    private void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce > forceThreshold && !hasSpawned)
        {
            createSoundSphere(collision);
            hasSpawned = !hasSpawned;
            Destroy(gameObject);
        }
    }

    private void createSoundSphere(Collision collision)
    {
        Vector3 collisionPoint = collision.contacts[0].point;

        GameObject sphere = Instantiate(sphereSound, collisionPoint, Quaternion.identity);
        sphere.transform.localScale = new Vector3(soundRadius, soundRadius, soundRadius);
    }
}
