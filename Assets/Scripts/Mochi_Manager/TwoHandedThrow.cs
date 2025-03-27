using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandedThrow : MonoBehaviour
{
    public float Throwforce = 20f;
    public float verticalBoost = 5f; // Tweak this value to your liking
    public TrailRenderer tr;

    private XRBaseInteractor leftHand;
    private XRBaseInteractor rightHand;
    private Vector3 lastLeftHandPosition;
    private Vector3 lastRightHandPosition;
    private Vector3 leftHandVelocity;
    private Vector3 rightHandVelocity;
    private Rigidbody rb;
    private bool isTwoHanded = false;
    private int handCount = 0; // Manual tracking of hands

    private void Start()
    {
        tr = GetComponent<TrailRenderer>();
        if (tr != null) tr.emitting = false;
        rb = GetComponent<Rigidbody>();
    }

    // Updated to take an interactor as a second argument
    public void StartTrackingHands(XRGrabInteractable grabInteractable, XRBaseInteractor newInteractor)
    {
        handCount++; // Increment when a hand grabs

        if (handCount == 1)
        {
            leftHand = newInteractor; // First hand is left
            Debug.Log("First hand (left) grabbed: " + leftHand.name);
        }
        else if (handCount >= 2)
        {
            if (rightHand == null && newInteractor != leftHand)
            {
                rightHand = newInteractor; // Second hand is right
                Debug.Log("Second hand (right) grabbed: " + rightHand.name);
            }

            if (leftHand != null && rightHand != null)
            {
                isTwoHanded = true;
                lastLeftHandPosition = leftHand.transform.position;
                lastRightHandPosition = rightHand.transform.position;

                rb.useGravity = false;
                rb.isKinematic = true;
                if (tr != null) tr.emitting = true;

                Debug.Log("Two-handed grip started!");
            }
        }
    }

    // Called when one or both hands release
    public void StopTrackingHands(XRGrabInteractable grabInteractable)
    {
        handCount--; // Decrement when a hand releases
        if (handCount < 2)
        {
            isTwoHanded = false;
            if (handCount == 0)
            {
                leftHand = null;
                rightHand = null;
            }
            else if (handCount == 1)
            {
                rightHand = null; // Assume the second hand released
            }
            if (tr != null) tr.emitting = false;
            Debug.Log("Two-handed grip stopped.");
        }
        if (handCount < 0) handCount = 0; // Prevent negative count
    }

    public bool WasTwoHanded()
    {
        return isTwoHanded;
    }

    private void FixedUpdate()
    {
        if (isTwoHanded && leftHand != null && rightHand != null)
        {
            leftHandVelocity = (leftHand.transform.position - lastLeftHandPosition) / Time.fixedDeltaTime;
            lastLeftHandPosition = leftHand.transform.position;
            rightHandVelocity = (rightHand.transform.position - lastRightHandPosition) / Time.fixedDeltaTime;
            lastRightHandPosition = rightHand.transform.position;
        }
    }

    public void ThrowObject()
    {
        if (WasTwoHanded())
        {
            rb.useGravity = true;
            rb.isKinematic = false;

            Vector3 throwVelocity = (leftHandVelocity + rightHandVelocity);
            float handSpeedDifference = (leftHandVelocity - rightHandVelocity).magnitude;
            throwVelocity += throwVelocity.normalized * handSpeedDifference * 1f;


            
            throwVelocity.y += verticalBoost;

            rb.velocity = throwVelocity * 3f * Throwforce;
            Vector3 spin = Vector3.Cross(leftHandVelocity, rightHandVelocity) * 0.7f;
            rb.angularVelocity = spin;

            isTwoHanded = false;
            if (tr != null) tr.emitting = false;

            Debug.Log("Object thrown with velocity: " + rb.velocity);
        }
    }

}