using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject PlayerXROrigin;
    PlayerMovement playerScript;

    void Start()
    {
        playerScript = PlayerXROrigin.GetComponent<PlayerMovement>();
    }

    public void DisableMovement()
    {
        playerScript.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DisableMovement();
    }
}
