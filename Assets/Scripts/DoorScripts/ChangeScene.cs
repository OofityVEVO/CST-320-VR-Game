using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene: MonoBehaviour
{
    public string sceneName = "AF_Armory";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void onCollisionEnter(Collision collision)
    {
      
        Debug.Log("Collision");
        SceneManager.LoadScene(sceneName);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
