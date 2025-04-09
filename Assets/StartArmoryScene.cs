using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartArmoryScene : MonoBehaviour
{
    // Start is called before the first frame update

    public DialogueScene scene;
    void Start()
    {
        if(scene != null)
        {
            scene.PlayAudio();
        }
        else
        {
            Debug.Log("There is no dialogue lines in the script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
