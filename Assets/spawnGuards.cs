using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnGuards : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject guards;
    public bool hasPotion;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPotion)
        {

            guards.SetActive(true);
        }

        else
        {
            guards.SetActive(false);
            //Debug.Log("Potion is not in the scene");
        }

        

    }
}
