using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateButton : MonoBehaviour
{
    [SerializeField] GameObject Gate;
    [SerializeField] GameObject NextLocation;
    [SerializeField] GameObject[] Guards;
    [SerializeField] GameObject[] Chitterkins;

    ActivateChitter activateChitter;

    void Start()
    {
        activateChitter = gameObject.GetComponent<ActivateChitter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Chitterkin"))
        {
            Gate.GetComponent<GateOpen>().RunGateOpen();
            activateChitter.Begin();

            StartCoroutine(ActivateGuardsWithDelay(17f));
            StartCoroutine(ActivateChitterWithDelay(17f));
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
}
