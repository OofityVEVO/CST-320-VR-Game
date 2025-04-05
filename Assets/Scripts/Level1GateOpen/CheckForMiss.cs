using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForMiss : MonoBehaviour
{
    [SerializeField] GameObject GameOverCauser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Chitterkin"))
        {
            GameOverCauser.SetActive(true);
        }
    }
}
