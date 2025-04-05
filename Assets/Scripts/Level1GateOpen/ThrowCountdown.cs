using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowCountdown : MonoBehaviour
{
    [SerializeField] GameObject GameOverCauser;
    public float countdownTime = 20f; // Set the countdown duration

    public float currentTime;
    private bool isCountingDown;
    
    void OnEnable()
    {
        StartCountdown();
    }

    public void StartCountdown()
    {
        currentTime = countdownTime;
        isCountingDown = true;
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        isCountingDown = false;
        GameOverCauser.SetActive(true);
    }

    public void StopCountdown()
    {
        StopAllCoroutines();
        isCountingDown = false;
    }
}
