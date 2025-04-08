using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepGuardTimer : MonoBehaviour
{
    [SerializeField] GameObject GameOverCauser;
    [SerializeField] GameObject Guard1;
    [SerializeField] GameObject Guard2;

    private SleepState Guard1State;
    private SleepState Guard2State;

    public float timer = 10f;

    void Start()
    {
        Guard1State = Guard1.GetComponent<SleepState>();
        Guard2State = Guard2.GetComponent<SleepState>();

        StartCoroutine(SleepCheckTimer(timer));
    }

    private IEnumerator SleepCheckTimer(float delay)
    {
        yield return new WaitForSeconds(delay);

        if(Guard1State.isAsleep == true && Guard2State.isAsleep == true)
        {

        }
        else
        {
            GameOverCauser.SetActive(true);
        }
    }
}
