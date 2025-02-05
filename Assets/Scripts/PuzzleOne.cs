using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleOne : MonoBehaviour
{
    UnityEvent MochiEvent1;

    void Start()
    {

        MochiEvent1.AddListener(PuzzleSolve);
    }

    void OnTriggerEnter(Collider collide)
    {
        if (collide.tag == "ChitterKin")
        {
            MochiEvent1.Invoke();
            MochiEvent1.RemoveListener(PuzzleSolve);
        }
    }

    void PuzzleSolve()
    {

    }


}
