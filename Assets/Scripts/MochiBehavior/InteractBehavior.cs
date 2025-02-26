using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class interactBehavior : MochiBehavior
{
    // serializeField to hold all interacatable objects
    [SerializeField]
    public List<Transform> interactables = new List<Transform>();



    NavMeshAgent _navMeshAgent;
    

    
    //distance from the object it will stand at
    public int Distance = 2;

    //distance from the object it will interact at
    public int interactAt = 2;

    //range in which the object will be detectable
    public int Range = 10;

    void Start()
    {
        //intializes nav mesh
        _navMeshAgent = this.GetComponent<NavMeshAgent>();


    }

    private void SetDestination(Vector3 target)
    {
        // sets the destination for the mochi to move to
        if (target != null)
        {
            
           
            Vector3 range = new Vector3(Distance, 0, Distance);
            target -= range;


            _navMeshAgent.SetDestination(target);
            
        }
        else
        {
            Debug.Log("No target found");
        }
    }

    
    void Update()
    {
        //gets positions for the interactable items

        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < interactables.Count; i++)
        {
            if (interactables[i] != null)
            {
                positions.Add(interactables[i].position);
                
                
            }
        }
        


        // Initialize variables to track the closest position and distance
        Vector3 closestInteract = positions[0];
        float distancef = Vector3.Distance(transform.position, closestInteract);
        int distance = (int)distancef;
        


        // Loop through the array to find the closest object
        for (int i = 0; i < positions.Count; i++)
        {
            float currentDistance = Vector3.Distance(transform.position, positions[i]);
            if (currentDistance < distance)
            {
                distance = (int)currentDistance;
                closestInteract = positions[i];
                
            }
        }

        


        // checks if the object is in range then sets its destination
        if (distance < Range) 
        {
            SetDestination(closestInteract);

            if (distance < interactAt)
            {
                Debug.Log("interacting");
            }
           
        }

        else
        {
            Debug.Log("Object is out of range");

           
        }
    }

    //sets position for mochi
    public override void SetPosition(Vector3 newPosition)
    {
        interactables[0].position = newPosition;
    }
}
