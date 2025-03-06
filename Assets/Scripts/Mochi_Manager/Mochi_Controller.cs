using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Mochi_Controller : MonoBehaviour
{
    public MochiManager mochiManager; // AI behavior
    public TwoHandedThrow throwable; // Throw behavior

    private XRGrabInteractable grabInteractable; // Reference to grab system

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        throwable = GetComponent<TwoHandedThrow>(); // Ensure reference is assigned

        if (grabInteractable != null)
        {
            Debug.Log("grabInteractable FOUND!");
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogError("grabInteractable is NULL! Make sure the XRGrabInteractable component is attached.");
        }

        EnableMochiManager();
    }



    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("OnGrab triggered! Switching to Throwable Mode.");
        EnableThrowable();
    }


    private void OnRelease(SelectExitEventArgs args)
    {
        EnableMochiManager();
    }

    private void EnableMochiManager()
    {
        Debug.Log("Enabling MochiManager, Disabling Throwable");

        if (mochiManager != null)
        {
            mochiManager.enabled = true;
        }
        if (throwable != null)
        {
            throwable.enabled = false;
        }
    }

    private void EnableThrowable()
    {
        Debug.Log("Enabling Throwable, Disabling MochiManager");

        if (mochiManager != null)
        {
            mochiManager.enabled = false;
        }
        if (throwable != null)
        {
            throwable.enabled = true;
        }
    }

}
