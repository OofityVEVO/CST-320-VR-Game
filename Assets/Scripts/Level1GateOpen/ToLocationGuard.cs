using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ToLocationGuard : MonoBehaviour
{
    #region Variables and Components
    [SerializeField] Transform[] Flags;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject AnimationManager;
    soldierAnimation animationScript;

    public bool LookAtPlayer;
    public bool willRun;
    public bool AutoEnable = true;
    public bool hasLooked = false;

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

        if (willRun)
        {
            animationScript.isRunning = true;
        }
        else
        {
            animationScript.isWalking = true;
        }
    }

    public void NewLocation(GameObject _object)
    {
        Flags[0] = _object.transform;

        ToFlag();

        if (willRun)
        {
            animationScript.isRunning = true;
        }
        else
        {
            animationScript.isWalking = true;
        }
    }

    // ### UNITY FUNCTIONS ####
    void Start()
    {
        animationScript = GetComponentInParent<soldierAnimation>();
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
        if (HasReachedFlag() && LookAtPlayer)
        {
            if (!hasLooked)
            {
                transform.LookAt(Player.transform.position);
                hasLooked = true;

                animationScript.isRunning = false;
                animationScript.isWalking = false;
                animationScript.changeAnimation("idle");
            }
        }
    }
}
