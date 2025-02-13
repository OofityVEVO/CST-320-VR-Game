using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followingBehavior : MochiBehavior {

    // initialize the player position
    [SerializeField]
    Transform _destination;



   
    NavMeshAgent _navMeshAgent;

    // distance from the object it will stand at to prevent mochi getting to close
    public float distance = 0.5f;
   
    void Start () {

        //initialize the nav mesh
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
       

    }

    private void SetDestination()
    {
        // sets the destination for the mochi to move to
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            Vector3 range = new Vector3(distance, distance, distance);
            targetVector -= range;


            _navMeshAgent.SetDestination(targetVector);
        }
    }
	
	
	void Update () {
        //updates the player position for mochi to follow
        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent is not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }

      
    }

    //sets the position of mochi
    public override void SetPosition(Vector3 newPosition)
    {
        _destination.position = newPosition;
    }
}
