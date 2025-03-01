using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ToLocation : MonoBehaviour
{
    #region Variables and Components
    [SerializeField] Transform[] Flags;
    [SerializeField] GameObject Player;
    public bool LookAtPlayer;

    int flagIndex = 0;
    NavMeshAgent _navMeshAgent;

    #endregion

    // ### SCRIPT FUNCTIONS ###
    public void ToFlag()
    {
        _navMeshAgent.SetDestination(Flags[flagIndex].position);
    }

    bool HasReachedFlag()
    {
        return !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;
    }


    // ### UNITY FUNCTIONS ####
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        if(HasReachedFlag() && LookAtPlayer)
        {
            transform.LookAt(Player.transform.position);
        }
    }
}
