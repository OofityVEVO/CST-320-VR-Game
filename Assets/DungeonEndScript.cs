using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEndScript : MonoBehaviour
{
    public DialogueScene audios;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        audios.PlayAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
