using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardChoice : MonoBehaviour
{
    private Animator animator; // animator for wizard
    public GameObject wizard;
    public Transform parent;
    public GameObject cage; // cage that holds the wizard
    public Transform star; // teleportation effect

    public DialogueScene goodEndingScene;
    public bool GoodEnd = false;


    // Start is called before the first frame update
    void Start()
    {
        // Find the wizard inside the cage and get its Animator


        animator = wizard.GetComponent<Animator>();



    }

    // Update is called once per frame
   


    IEnumerator wizardDance()
    {
        goodEndingScene.PlayAudio();

        float wait1 = .5f; // wait time before wizard starts dancing
        float wait2 = 3.7f; // wait time before wizard teleports
        float wait3 = 0.3f; // wait time for teleportation effect to play
        


        wizard.transform.SetParent(parent); // Set the parent to the new transform
        cage.SetActive(false);
        wizard.transform.position = new Vector3(wizard.transform.position.x, 3.3333f, wizard.transform.position.z); // puts wizard on ground

        yield return new WaitForSeconds(wait1);

        animator.Play("GMOD Default dance");
        yield return new WaitForSeconds(wait2);

        star.gameObject.SetActive(true); // activates the teleportation effect
        yield return new WaitForSeconds(wait3); // wait for the teleportation effect to play

        wizard.SetActive(false);

        GoodEnd = true;




    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("key")) // checks if the player has entered the trigger
        {
            StartCoroutine(wizardDance());
        }

    }
}
