using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioFollower : MonoBehaviour
{
    private Transform source;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        source = transform.GetChild(0);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audio.transform.position = new Vector3(source.position.x + 1, source.position.y +5, source.position.z + 1);

    }
}
