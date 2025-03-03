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
    public Rigidbody rb;

    [Header("Interact Behavior Information")]
    public List<Transform> interactables = new List<Transform>();
    public int interactDistance = 2; //distance from the object it will stand at
    public int interactAt = 2; //distance from the object it will interact at
    public int range = 10; //range in which the object will be detectable
    public GameObject interactable;
    public MochiInteractBase interactScript;
    public float objectDistance = 10f;
    public XRController controller; // Controls which trigger the player needs to press to get Mochi to interact

    [Header("Follow Behavior Information")]
    public Transform _playerDestination;
    public float socialDistance = 0.5f; // distance from the object it will stand at to prevent mochi getting to close
    public float detectionRadius = 10f; // how far Mochi will want to check
    public LayerMask interactableLayer;

    [Header("Standby Behavior Information")]
    public Vector3 MochiPosition; // Standby Behavior

    void Start()
    {
        _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
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
    }

}
