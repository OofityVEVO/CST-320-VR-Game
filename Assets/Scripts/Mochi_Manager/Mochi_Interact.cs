using UnityEngine;
using System.Collections;

public class Mochi_Interact : StateBase
{
    private bool at_Interactable = false;

    public override void StartState(MochiManager manager)
    {
        Debug.Log("Mochi switched to interacting");

        manager.StartCoroutine(GoToInteractable(manager));
    }

    public override void UpdateState(MochiManager manager)
    {
        // Switch Input.KeyCode to a controller later
        if (at_Interactable && Input.GetKeyDown(KeyCode.Q))
        {
            manager.interactScript.Interact();
        }

    }

    private IEnumerator GoToInteractable(MochiManager manager)
    {
        manager._navMeshAgent.SetDestination(manager.interactable.transform.position);

        while (!at_Interactable)
        {
            float distance = Vector3.Distance(manager.transform.position, manager.interactable.transform.position);

            // Check if Mochi is close enough
            if (distance <= manager.objectDistance)
            {
                Debug.Log("Mochi reached the interactable");
                at_Interactable = true;
                manager._navMeshAgent.isStopped = true; // Stop the NavMeshAgent
            }
            yield return null;
        }
    }
}