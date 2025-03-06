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
        if (at_Interactable && Input.GetKeyDown(KeyCode.Q))
        {
            manager.interactScript.Interact();
            Debug.Log("Mochi is interacting with the object");
        }

        if(!MochiTooFar(manager))
        {
            manager.SwitchState(manager.mochiFollow);
        }

    }
    private bool MochiTooFar(MochiManager manager)
    {
        manager.currDistanceToPlayer = Vector3.Distance(manager.transform.position, manager._playerDestination.transform.position);

        if(manager.currDistanceToPlayer >= manager.distanceToPlayer )
        {
            Debug.Log("Mochi is too far from the player");
            return true;
        }

        return false;
    }

    private IEnumerator GoToInteractable(MochiManager manager)
    {
        manager.ResetAgent();

        if (manager.interactable == null)
        {
            Debug.LogError("No interactable assigned!");
            yield break;
        }
        Debug.Log("Moving Mochi to interactable: " + manager.interactable.name);

        Vector3 targetPosition = manager.interactable.transform.position;

        if (UnityEngine.AI.NavMesh.SamplePosition(targetPosition, out UnityEngine.AI.NavMeshHit hit, 5.0f, UnityEngine.AI.NavMesh.AllAreas))
        {
            targetPosition = hit.position; // Snap to nearest NavMesh point
        }
        else
        {
            Debug.LogError("Target position is NOT on the NavMesh!");
            yield break;
        }

        // ✅ Reset NavMeshAgent before setting a new destination
        manager._navMeshAgent.ResetPath();
        manager._navMeshAgent.SetDestination(targetPosition);
        manager._navMeshAgent.isStopped = false;

        yield return null;

        if (!manager._navMeshAgent.hasPath)
        {
            Debug.LogError("No path found to the interactable!");
            yield break;
        }

        float interactDistanceSqr = manager.interactDistance * manager.interactDistance;
        float maxAllowedDistanceSqr = manager.distanceToPlayer * manager.distanceToPlayer; // Square the max allowed distance

        while (manager._navMeshAgent.pathPending ||
              (manager.transform.position - manager.interactable.transform.position).sqrMagnitude > interactDistanceSqr)
        {
            // ✅ Check if Mochi is too far from the player
            float distanceToPlayerSqr = (manager.transform.position - manager._playerDestination.transform.position).sqrMagnitude;
            if (distanceToPlayerSqr > maxAllowedDistanceSqr)
            {
                Debug.Log("Mochi is too far from the player, stopping interaction!");
                manager.StopAllCoroutines(); // Stops all running coroutines
                // manager.SwitchState(manager.mochiFollow); // Switch to follow state
                yield break;
            }


            // ✅ Update position and distance to interactable
            targetPosition = manager.interactable.transform.position;
            manager._navMeshAgent.SetDestination(targetPosition);

            yield return null;
        }

        // ✅ Ensure Mochi stops moving when reaching the interactable
        manager._navMeshAgent.isStopped = true;
        manager._navMeshAgent.ResetPath();
        manager._navMeshAgent.velocity = Vector3.zero;
        manager._navMeshAgent.updatePosition = false;
        manager._navMeshAgent.updateRotation = false;
        manager._navMeshAgent.enabled = false;

        // ✅ Stop Rigidbody movement
        manager.rb.velocity = Vector3.zero;
        manager.rb.angularVelocity = Vector3.zero;
        manager.rb.isKinematic = true;

        Debug.Log("Mochi reached the interactable and stopped completely!");
        at_Interactable = true;
    }



}