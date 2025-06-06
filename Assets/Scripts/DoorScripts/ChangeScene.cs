using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    public bool hasKey;
    private Collider collider_;
    private Animator animator;

    public WizardChoice wizardChoice;
    public DialogueScene badEnding;

    void Start()
    {
        collider_ = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hasKey == true)
        {

            collider_.enabled = true;
            animator.enabled = true;

        }
    }
    
    private void OnTriggerStay(Collider key)
    {
        if(key.CompareTag("key"))
        {
            CheckEnding();

        }

        if (hasKey)
        {
            // If the player collides with the object, load the scene
            if (key.gameObject.tag == "Player")
                SceneManager.LoadScene(sceneName);
        }
    }

    private void CheckEnding()
    {
        hasKey = true;

        if(wizardChoice.GoodEnd == false)
        {
            badEnding.PlayAudio();
        }

    }

}
