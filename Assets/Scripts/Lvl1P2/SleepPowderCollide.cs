using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepPowderCollide : MonoBehaviour
{
    public GameObject sleepingPower;

    void OnTriggerEnter(Collider collide)
    {
        if (collide.CompareTag("Chitterkin"))
        {
            HoldingObject holder = collide.gameObject.GetComponent<HoldingObject>();
            if (holder != null)
            {
                holder.InstantiateGameObject(sleepingPower);
            }
        }
    }
}
