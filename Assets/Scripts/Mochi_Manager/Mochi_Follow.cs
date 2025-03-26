using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mochi_Follow : StateBase
{
    private bool canCheckForInteractables = false; // Flag to control when interactables can be checked

    public override void StartState(MochiManager manager)
    {
        canCheckForInteractables = false; // Prevent checking immediately
        manager.StartCoroutine(EnableInteractableCheckAfterDelay(manager, 1f)); // Start coroutine
    }

    public override void UpdateState(MochiManager manager)
    {
        if (canCheckForInteractables)
        {
            CheckForInteractables(manager);
        }
        SetDestination(manager);
    }

    private IEnumerator EnableInteractableCheckAfterDelay(MochiManager manager, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 6 seconds
        canCheckForInteractables = true; // Enable interactable detection
        Debug.Log("Mochi is now checking for interactables!");
    }

    private void SetDestination(MochiManager manager)
    {
        if (manager._playerDestination != null)
        {
            Vector3 targetVector = manager._playerDestination.transform.position;
            Vector3 range = new Vector3(manager.socialDistance, manager.socialDistance, manager.socialDistance);
            targetVector -= range;

            manager._navMeshAgent.SetDestination(targetVector);
        }
    }

    private void CheckForInteractables(MochiManager manager)
    {
        Collider[] hitColliders = Physics.OverlapSphere(manager.transform.position, manager.detectionRadius, manager.interactableLayer);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Chitter_Interactable"))
            {
                Debug.Log("Mochi found an interactable item! Name : " + col.gameObject.name);
                manager.ChangeInteractable(col.gameObject);
                manager.SwitchState(manager.mochiInteract);
                return; // Stop checking once we find one
            }
        }
    }
}
