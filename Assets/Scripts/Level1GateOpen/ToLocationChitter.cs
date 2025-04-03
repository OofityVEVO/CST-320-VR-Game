using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ToLocationChitter : MonoBehaviour
{
    #region Variables and Components
    [SerializeField] Transform[] Flags;
    [SerializeField] GameObject NextFlag;
    [SerializeField] GameObject LookAtObject;

    public bool AutoEnable = true;
    public bool LookAtPosition = true;

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

    public void Begin()
    {
        ToFlag();
    }

    public void NewLocation(GameObject _object)
    {
        Flags[0] = _object.transform;

        ToFlag();
    }

    // ### UNITY FUNCTIONS ####
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        if (!AutoEnable)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Begin();
        }
    }

    void Update()
    {
        if (HasReachedFlag() && LookAtPosition)
        {
            if (LookAtPosition)
            {
                transform.LookAt(LookAtObject.transform.position);
            }
        }
    }
}
