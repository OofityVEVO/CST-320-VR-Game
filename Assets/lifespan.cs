using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifespan : MonoBehaviour
{
    public float span = 5f;

    void Start()
    {
        Destroy(gameObject, span);
    }
}
