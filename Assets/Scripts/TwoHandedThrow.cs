using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandedThrow : XRGrabInteractable
{
    public float Throwforce = 20f;
    public TrailRenderer tr;

    private XRBaseInteractor leftHand;
    private XRBaseInteractor rightHand;

    private Vector3 lastLeftHandPosition;
    private Vector3 lastRightHandPosition;

    private Vector3 leftHandVelocity;
    private Vector3 rightHandVelocity;

    private Rigidbody rb;

    [SerializeField] private Transform holdOffset;

    protected override void Awake()
    {
        base.Awake();
        if (tr != null)
            tr.emitting = false;

        rb = GetComponent<Rigidbody>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor; // Explicit cast

        if (interactor == null) return; // Safety check

        if (leftHand == null)
        {
            leftHand = interactor;
        }
        else if (rightHand == null && interactor != leftHand)
        {
            rightHand = interactor;
        }

        if (leftHand != null && rightHand != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor; // Explicit cast

        if (interactor == null) return; // Safety check

        if (interactor == leftHand)
        {
            leftHand = null;
        }
        else if (interactor == rightHand)
        {
            rightHand = null;
        }

        if (leftHand == null && rightHand == null)
        {
            ThrowObject();
        }

        base.OnSelectExited(args);
    }

    private void FixedUpdate()
    {
        if (leftHand)
        {
            leftHandVelocity = (leftHand.transform.position - lastLeftHandPosition) / Time.fixedDeltaTime;
            lastLeftHandPosition = leftHand.transform.position;
        }

        if (rightHand)
        {
            rightHandVelocity = (rightHand.transform.position - lastRightHandPosition) / Time.fixedDeltaTime;
            lastRightHandPosition = rightHand.transform.position;
        }
    }

    private void ThrowObject()
    {
        rb.useGravity = true;
        rb.isKinematic = false;

        // Calculate throw velocity using both hands
        Vector3 throwVelocity = (leftHandVelocity + rightHandVelocity) / 2;

        // Additional force boost when both hands are used
        float handSpeedDifference = (leftHandVelocity - rightHandVelocity).magnitude;
        throwVelocity += throwVelocity.normalized * handSpeedDifference * 1f; // Extra power from hand movement

        // Increase the velocity multiplier
        rb.velocity = throwVelocity * 1.5f * Throwforce;

        // Improve spin calculation for a more dynamic throw
        Vector3 spin = Vector3.Cross(leftHandVelocity, rightHandVelocity) * 0.7f; // Slightly more spin
        rb.angularVelocity = spin;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        // Ensure event listeners are removed to prevent memory leaks
        selectEntered.RemoveAllListeners();
        selectExited.RemoveAllListeners();
    }
}
