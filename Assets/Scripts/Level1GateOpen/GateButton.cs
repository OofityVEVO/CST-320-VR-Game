using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateButton : MonoBehaviour
{
    [SerializeField] GameObject Gate;
    [SerializeField] GameObject NextLocation;
    [SerializeField] GameObject[] Guards;
    [SerializeField] GameObject[] Chitterkins;
    [SerializeField] GameObject MissPlane;
    [SerializeField] GameObject Countdown;
    [SerializeField] GameObject PlayerXROrigin;
    ActivateChitter activateChitter;
    PlayerMovement playerScript;

    public DialogueScene ButtonHit;

    void Start()
    {
        activateChitter = gameObject.GetComponent<ActivateChitter>();
        playerScript = PlayerXROrigin.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Chitterkin"))
        {
            Gate.GetComponent<GateOpen>().RunGateOpen();
            MissPlane.SetActive(false);
            Countdown.SetActive(false);

            activateChitter.Begin();
            ButtonHit.PlayAudio();

            StartCoroutine(ActivateGuardsWithDelay(17f));
            StartCoroutine(ActivateChitterWithDelay(17f));
            StartCoroutine(ActivatePlayerMovement(20f));
        }
    }

    private IEnumerator ActivateGuardsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject Guard in Guards)
        {
            GameObject child = Guard.transform.Find("guard")?.gameObject;
            child.GetComponent<ToLocationGuard>().NewLocation(NextLocation);
        }
    }

    private IEnumerator ActivateChitterWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject Chitter in Chitterkins)
        {
            Chitter.GetComponent<ToLocationChitter>().NewLocation(NextLocation);
        }
    }

    private IEnumerator ActivatePlayerMovement(float delay)
    {
        yield return new WaitForSeconds(delay);

        playerScript.enabled = true;
    }
}
