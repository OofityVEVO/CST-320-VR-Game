using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Mochi_Controller : MonoBehaviour
{
    public MochiManager mochiManager; // AI behavior
    public TwoHandedThrow throwable;  // Throw behavior
    private XRGrabInteractable grabInteractable; // Single grab system
    public float timeSpentInThrowable = 3f;
    private int handCount = 0; // Manual tracking of hands
    private bool hasLanded = false; // To check if object hit the ground

    private void Start()
    {
        // Get components
        grabInteractable = GetComponent<XRGrabInteractable>();
        throwable = GetComponent<TwoHandedThrow>();

        if (grabInteractable != null)
        {
            Debug.Log("grabInteractable FOUND!");
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
            grabInteractable.selectExited.AddListener(OnSelectExited);
        }
        else
        {
            Debug.LogError("grabInteractable is NULL! Make sure the XRGrabInteractable component is attached.");
        }

        if (throwable == null)
        {
            Debug.LogError("TwoHandedThrow component is missing!");
        }

        // Don't enable MochiManager at start
        if (mochiManager != null)
        {
            mochiManager.enabled = false;
            if (mochiManager._navMeshAgent != null)
            {
                mochiManager._navMeshAgent.enabled = false;
            }
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            grabInteractable.selectExited.RemoveListener(OnSelectExited);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Object grabbed! Disabling AI.");
        handCount++; // Increment hand count
        hasLanded = false; // Reset when picked up again

        if (mochiManager != null)
        {
            mochiManager.enabled = false;
            if (mochiManager._navMeshAgent != null)
            {
                mochiManager._navMeshAgent.enabled = false;
            }
        }

        if (throwable != null && handCount >= 2)
        {
            throwable.StartTrackingHands(grabInteractable, args.interactorObject as XRBaseInteractor);
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("Object released!");
        handCount--;

        /*
        if (throwable != null)
        {
            if (handCount <= 0)
            {
                
            }
            else
            {
                throwable.StopTrackingHands(grabInteractable);
            }
        }*/

        throwable.ThrowObject();
        handCount = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded && collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Mochi hit the ground!");
            hasLanded = true;
            StartCoroutine(DelayedTime());
        }
    }

    IEnumerator DelayedTime()
    {
        yield return new WaitForSeconds(timeSpentInThrowable);

        if (mochiManager != null)
        {
            mochiManager.enabled = true;
            if (mochiManager._navMeshAgent != null)
            {
                mochiManager._navMeshAgent.enabled = true;
            }
            Debug.Log("MochiManager enabled after delay!");
        }
    }

    public void EnterCutsceneMode()
    {
        Debug.Log("Cutscene started — disabling behaviors.");

        if (mochiManager != null)
        {
            mochiManager.enabled = false;
            if (mochiManager._navMeshAgent != null)
                mochiManager._navMeshAgent.enabled = false;
        }

        // Disable interaction and physics
        if (throwable != null)
            throwable.enabled = false;

        if (grabInteractable != null)
            grabInteractable.enabled = false;

        // Optional: Freeze physics
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    public void ExitCutsceneMode()
    {
        Debug.Log("Cutscene ended — enabling behaviors.");

        // Enable interaction
        if (throwable != null)
            throwable.enabled = true;

        if (grabInteractable != null)
            grabInteractable.enabled = true;

        // Unfreeze physics
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // AI will still wait until mochi hits the ground
    }

}
