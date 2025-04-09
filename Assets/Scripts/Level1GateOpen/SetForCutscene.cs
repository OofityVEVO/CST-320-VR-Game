using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetForCutscene : MonoBehaviour
{
    [SerializeField] GameObject PlayerXROrigin;
    [SerializeField] GameObject Mochi;
    [SerializeField] GameObject Countdown;
    [SerializeField] GameObject PlayerTeleportLocation;
    [SerializeField] GameObject MochiTeleportLocation;
    PlayerMovement playerScript;

    void Start()
    {
        playerScript = PlayerXROrigin.GetComponent<PlayerMovement>();
    }

    public void StartCutscene()
    {
        playerScript.enabled = false;
        PlayerXROrigin.transform.SetPositionAndRotation(PlayerTeleportLocation.transform.position, PlayerTeleportLocation.transform.rotation);
        Mochi.transform.SetPositionAndRotation(MochiTeleportLocation.transform.position, MochiTeleportLocation.transform.rotation);
        Countdown.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCutscene();
        }
    }
}
