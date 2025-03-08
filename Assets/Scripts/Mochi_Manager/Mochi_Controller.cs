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

        EnableMochiManager();
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

        if (mochiManager != null)
        {
            mochiManager.enabled = false;
            if (mochiManager._navMeshAgent != null)
            {
                mochiManager._navMeshAgent.enabled = false;
            }
        }

        // Check for two-handed grab and pass the interactor
        if (throwable != null && handCount >= 2)
        {
            throwable.StartTrackingHands(grabInteractable, args.interactorObject as XRBaseInteractor);
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("Object released!");
        handCount--; // Decrement hand count

        if (throwable != null)
        {
            if (handCount <= 0)
            {
                // No hands left, throw if it was two-handed
                throwable.ThrowObject();
                /*
                if (throwable.WasTwoHanded())
                {
                    throwable.ThrowObject();
                }
                */
                Invoke("EnableMochiManager", 0.2f); // Delay to allow throw physics
                handCount = 0; // Reset to avoid negative
            }
            else
            {
                // One hand still grabbing, stop two-handed tracking if active
                throwable.StopTrackingHands(grabInteractable);
            }
        }
    }

    private void EnableMochiManager()
    {
        Debug.Log("Enabling MochiManager");
        StartCoroutine(DelayedTime());
        
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
        }
    }
}