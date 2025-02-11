using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRCagePull : MonoBehaviour
{
    public Transform anchorPoint;  // Set this to the anchor in Unity
    public float maxPullDistance = 3f;
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Ensure physics-based movement
        grabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, anchorPoint.position) > maxPullDistance)
        {
            // Calculate the closest allowed position
            Vector3 direction = (transform.position - anchorPoint.position).normalized;
            Vector3 clampedPosition = anchorPoint.position + direction * maxPullDistance;

            // Snap the cage back within range
            rb.MovePosition(clampedPosition);
        }
    }
}
