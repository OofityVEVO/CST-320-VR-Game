using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Grabbable : MonoBehaviour
{
    public float maxRadius = 2.0f; // Set max radius
    private Vector3 initialPosition;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        initialPosition = transform.position; // Store the starting position
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if (grabInteractable.isSelected) // Only apply restriction when grabbed
        {
            float distance = Vector3.Distance(initialPosition, transform.position);
            if (distance > maxRadius)
            {
                Vector3 direction = (transform.position - initialPosition).normalized;
                transform.position = initialPosition + direction * maxRadius; // Clamp position
            }
        }
    }
}
