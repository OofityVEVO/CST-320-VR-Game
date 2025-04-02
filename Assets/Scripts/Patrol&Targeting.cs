using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PatrolTargeting : MonoBehaviour
{
    #region Variables and Components
    [SerializeField] GameObject AnimationManager;
    soldierAnimation animationScript;

    public int maxPatience = 1000;
    public int maxKnowLocation = 1000;
    public int startingFlag = 0;
    public bool randomTargeting = false;
    public bool canIdle = false;
    [SerializeField] Transform[] Flags;

    int flagIndex = 0;
    int patience = 0;
    int knowLocation = 0;
    bool playerInSight = false;
    bool lookingForPlayer = false;
    bool distracted = false;
    public bool isActive = true;
    Vector3 lastSeenPosition;
    NavMeshAgent _navMeshAgent;
    Transform player = null;

    #endregion

    // ### SCRIPT FUNCTIONS ###
    private void ToPlayer()
    {
        _navMeshAgent.speed = 4;
        _navMeshAgent.acceleration = 4;

        if (playerInSight)
        {
            _navMeshAgent.SetDestination(player.position);
        }
        else if (!playerInSight && knowLocation > 0)
        {
            _navMeshAgent.SetDestination(player.position);
        }
        else
        {
            _navMeshAgent.SetDestination(lastSeenPosition);
        }
    }

    private void ToFlag()
    {
        _navMeshAgent.speed = 2;
        _navMeshAgent.acceleration = 2;

        if (HasReachedFlag() && !canIdle)
        {
            flagIndex = randomTargeting
            ? Random.Range(0, Flags.Length)
            : (flagIndex + 1) % Flags.Length;

            _navMeshAgent.SetDestination(Flags[flagIndex].position);
        }
        else if (!HasReachedFlag())
        {
            _navMeshAgent.SetDestination(Flags[flagIndex].position);
        }
        else if (HasReachedFlag() || !distracted) // Can Idle Case
        {
            _navMeshAgent.SetDestination(Flags[flagIndex].position);
        }
    }

    private void ToDistraction()
    {
        _navMeshAgent.speed = 4;
        _navMeshAgent.acceleration = 4;

        _navMeshAgent.SetDestination(lastSeenPosition);

        if (HasReachedFlag())
        {
            animationScript.isRunning = false;
            distracted = false;
        }
    }

    bool HasReachedFlag()
    {
        return !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;
    }


    // ### UNITY FUNCTIONS ####
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        _navMeshAgent = GetComponent<NavMeshAgent>();

        flagIndex = startingFlag;
        _navMeshAgent.SetDestination(Flags[flagIndex].position);

        animationScript = GetComponentInParent<soldierAnimation>();
    }

    void Update()
    {
        if (isActive)
        {
            // Patience Decay
            if (!playerInSight && lookingForPlayer && patience > 0)
            {
                patience--;
                knowLocation--;

                if (patience <= 0 && !distracted)
                {
                    lookingForPlayer = false;
                    player = null;
                    lastSeenPosition = Vector3.zero;
                    knowLocation = 0;

                    if (canIdle)
                    {
                        animationScript.isRunning = true;
                        animationScript.isWalking = false;
                        ToFlag();
                    }
                }
            }

            // Targeting Cases
            if(HasReachedFlag() && canIdle && !playerInSight)
            {
                animationScript.isRunning = false;
                animationScript.isWalking = false;
                animationScript.changeAnimation("idle");
            }
            else if (!lookingForPlayer && !distracted)
            {
                animationScript.isRunning = false;
                animationScript.isWalking = true;
                ToFlag();
            }
            else if (!lookingForPlayer && distracted)
            {
                animationScript.isRunning = true;
                ToDistraction();
            }
            else if (playerInSight || lookingForPlayer)
            {
                animationScript.isRunning = true;
                ToPlayer();
            }
        }
    }


    // ### COMPONENT FUNCTIONS ###
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Distraction"))
        {
            Collider objectCollider = other.GetComponent<SphereCollider>();
            if (objectCollider != null && objectCollider.bounds.Contains(transform.position))
            {
                lastSeenPosition = other.transform.position;
                distracted = true;
            }
        }
        else if(other.CompareTag("SleepPotion"))
        {
            Collider objectCollider = other.GetComponent<SphereCollider>();
            if (objectCollider != null && objectCollider.bounds.Contains(transform.position))
            {
                FallAsleep();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 directionToPlayer = (other.transform.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, other.transform.position);

            Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.red);

            int layerMask = ~LayerMask.GetMask("Enemies");
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer, layerMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    player = other.transform;
                    lastSeenPosition = other.transform.position;
                    playerInSight = true;
                    lookingForPlayer = true;
                    distracted = false;
                    patience = maxPatience;
                    knowLocation = maxKnowLocation;
                }
                else
                {
                    playerInSight = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInSight = false;
        }
    }

    void FallAsleep()
    {
        isActive = false;
        _navMeshAgent.isStopped = true;

        animationScript.isRunning = false;
        animationScript.isWalking = false;
        animationScript.isSleep = true;
    }
}