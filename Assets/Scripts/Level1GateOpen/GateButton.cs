using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateButton : MonoBehaviour
{
    [SerializeField] GameObject Gate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Chitterkin"))
        {
            Gate.GetComponent<GateOpen>().RunGateOpen();
        }
    }
}
