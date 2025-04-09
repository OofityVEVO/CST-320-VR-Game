using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionShatterable : MonoBehaviour
{
    [SerializeField] GameObject sphereRadius;
    [SerializeField] GameObject Mochi;
    [SerializeField] XRGrabInteractable grabInteractable;

    HoldingObject holdingScript;

    public float forceThreshold = 10f;
    public float radius = 30f;
    public float invincibleTime = 3f;
    bool isInvincible = true;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        Mochi = GameObject.Find("Mochi_V1");
        holdingScript = Mochi.GetComponent<HoldingObject>();
    }

    void Update()
    {
        StartCoroutine(InvincibleDelay(invincibleTime));

        // Check if being grabbed
        if (grabInteractable.isSelected)
        {
            // It's being held by a player
            holdingScript.OnPlayerInteract();
            Debug.Log("Object is being held");
        }
    }

    private IEnumerator InvincibleDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isInvincible = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce > forceThreshold && !isInvincible)
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
