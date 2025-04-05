using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetForCutscene : MonoBehaviour
{
    [SerializeField] GameObject PlayerXROrigin;
    [SerializeField] GameObject Countdown;
    [SerializeField] GameObject TeleportLocation;
    PlayerMovement playerScript;

    void Start()
    {
        playerScript = PlayerXROrigin.GetComponent<PlayerMovement>();
    }

    public void StartCutscene()
    {
        playerScript.enabled = false;
        PlayerXROrigin.transform.SetPositionAndRotation(TeleportLocation.transform.position, TeleportLocation.transform.rotation);
        Countdown.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        StartCutscene();
    }
}
