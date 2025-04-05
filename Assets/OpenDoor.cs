using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animation open = GetComponent<Animation>();
        open.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
