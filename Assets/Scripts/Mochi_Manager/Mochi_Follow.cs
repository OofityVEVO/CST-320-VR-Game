using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mochi_Follow : StateBase
{
    public override void StartState(MochiManager manager)
    {

    }
    public override void UpdateState(MochiManager manager)
    {
        SetDestination(manager);
        CheckForInteractables(manager);
    }

    private void SetDestination(MochiManager manager)
    {
        // sets the destination for the mochi to move to
        if (manager._playerDestination != null)
        {
            Vector3 targetVector = manager._playerDestination.transform.position;
            Vector3 range = new Vector3(manager.socialDistance, manager.socialDistance, manager.socialDistance);
            targetVector -= range;


            manager._navMeshAgent.SetDestination(targetVector);
        }
    }

    void CheckForInteractables(MochiManager manager)
    {
        Collider[] hitColliders = Physics.OverlapSphere(manager.transform.position, manager.detectionRadius, manager.interactableLayer);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Chitter_Interactable"))
            {
                Debug.Log("Mochi found an interactable item!");
                manager.ChangeInteractable(col.gameObject);
                manager.SwitchState(manager.mochiInteract);
                return; // Stop checking once we find one
            }
        }

    }

}
