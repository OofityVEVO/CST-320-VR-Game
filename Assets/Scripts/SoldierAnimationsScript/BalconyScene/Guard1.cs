using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GridBrushBase;

public class Guard1 : MonoBehaviour
{
    public soldierAnimation soldierAnimation;
    public Vector3 rotationDirection;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 zero = new Vector3(0, 0, 0);
        soldierAnimation.isPoint = true;

        
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(wait(2));
        





    }

    IEnumerator wait(float waitTime)
    {
        UnityEngine.Debug.Log("Waiting");
        yield return new WaitForSecondsRealtime(waitTime);
        PointAndGo();
        
       

        UnityEngine.Debug.Log("hello");

        
    }

    void PointAndGo()
    {
        soldierAnimation.checkAnimation();
        float smooth = Time.deltaTime * 2;

            if (transform.rotation.y <= .1 && transform.rotation.y >= -.1)
            {
                UnityEngine.Debug.Log("Hi");
            }
            else
            {

                transform.Rotate(rotationDirection * smooth);
            }
        

        soldierAnimation.isRunning = true;
    }
}
