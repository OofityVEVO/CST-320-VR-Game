using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public abstract class StateBase
{
    abstract public void StartState(MochiManager manager);
    abstract public void UpdateState(MochiManager manager);
}

public class MochiManager : MonoBehaviour
{

    [Header("Mochi States")]
    public StateBase currState;
    public StateBase mochiStandby;
    public StateBase mochiFollow;
    public StateBase mochiInteract;

    [Header("Mochi Information")]
    public UnityEngine.AI.NavMeshAgent _navMeshAgent;
    public Transform _playerDestination;
    public Rigidbody rb;
    public float mochiYRotation = 90f;
    public Transform visualRoot; // Drag the VisualMochi here in the Inspector


    [Header("Interact Behavior Information")]
    public float interactDistance = 2.5f; //distance from the object it will stand at
    public GameObject interactable;
    public MochiInteractBase interactScript;
    public float objectDistance = 10f;
    public float distanceToInteractable; // the distance between Mochi and the interactableObject
    public float currDistanceToPlayer; //distance from the object it will interact at
    public float distanceToPlayer = 10f; //distance from the object it will interact at
    public XRController controller; // Controls which trigger the player needs to press to get Mochi to interact

    [Header("Follow Behavior Information")]
    public float socialDistance = 0.5f; // distance from the object it will stand at to prevent mochi getting to close
    public float detectionRadius = 10f; // how far Mochi will want to check for an interactable
    public LayerMask interactableLayer;

    [Header("Standby Behavior Information")]
    public Vector3 MochiPosition; // Standby Behavior

    void Start()
    {
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        _navMeshAgent.updateRotation = false;

        interactable = null;

        mochiStandby = new Mochi_Standby();
        mochiInteract = new Mochi_Interact();
        mochiFollow = new Mochi_Follow();

        currState = mochiFollow;
    }

    // Update is called once per frame
    public void Update()
    {
        currState.UpdateState(this);
        FacePlayer();
        // Debug.Log("Current State: " + currState.GetType().Name);


        /*
        if (_playerDestination != null)
        {
            Vector3 lookDirection = _playerDestination.position - transform.position;
            lookDirection.y = 0f; // Prevents Mochi from tilting up/down
            lookDirection.z = 90f;
            lookDirection.x = 90f;
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
        */


        Debug.DrawLine(visualRoot.position, _playerDestination.position, Color.green);


    }

    public void SwitchState(StateBase newState)
    {
        currState = newState;
        currState.StartState(this);
    }

    public void ChangeInteractable(GameObject newInteractable)
    {
        interactable = newInteractable;
        interactScript = interactable.GetComponent<MochiInteractBase>();
        currState = mochiInteract;
        currState.StartState(this);
    }

    public void ResetAgent()
    {
        _navMeshAgent.ResetPath(); // Clears the current path
        _navMeshAgent.isStopped = true; // Stops movement
        _navMeshAgent.velocity = Vector3.zero; // Resets velocity
        _navMeshAgent.enabled = false; // Disables and reenables to fully reset
        _navMeshAgent.enabled = true;
    }

    private void FacePlayer()
    {
        if (_playerDestination == null || visualRoot == null) return;

        Vector3 direction = _playerDestination.position - visualRoot.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion correctedRotation = lookRotation * Quaternion.Euler(0, mochiYRotation, 0); // <<< Add this

            visualRoot.rotation = Quaternion.Slerp(visualRoot.rotation, correctedRotation, Time.deltaTime * 5f);
        }
    }




}
