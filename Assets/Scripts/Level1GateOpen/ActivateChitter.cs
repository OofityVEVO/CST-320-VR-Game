using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ActivateChitter : MonoBehaviour
{
    [SerializeField] GameObject[] Chitterkins;

    public void Begin()
    {
        foreach (GameObject Chitter in Chitterkins)
        {
            Chitter.SetActive(true);
            Chitter.GetComponent<ToLocationChitter>().Begin();
        }
    }
}
