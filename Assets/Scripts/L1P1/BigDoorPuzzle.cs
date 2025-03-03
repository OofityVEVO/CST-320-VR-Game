using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public enum buttons {button1, button2};

public class BigDoorPuzzle : MonoBehaviour
{
    [Header("Door Settings")]
    public GameObject leftDoor;
    public GameObject rightDoor;
    public float openAngle = 90f; // Adjust to match your door's rotation
    public float closeAngle = 0f;
    public float speed = 2f; // Adjust speed for smoother opening
    private bool isOpening = false;
    public float duration = 2f;

    [Header("General Button Settings")]
    public Transform player;
    public float activationDistance = 8f; // Activation distance must be bigger (at least 8 or smth)
    //public XRBaseInteractable interactable; // Assign the XR interactable component of the button

    [Header("Button 1 Settings")]
    public GameObject button1;
    public GameObject activatedButton1;
    public GameObject uiPrompt1;  
    public bool button1_Pressed;
    
    [Header("Button 2 Settings")]
    public GameObject button2;
    public GameObject activatedButton2;
    public GameObject uiPrompt2; 
    public bool button2_Pressed;
   
    void Update()
    {
        float distance1 = Vector3.Distance(player.position, button1.transform.position);
        float distance2 = Vector3.Distance(player.position, button2.transform.position);
    
        OpenUIPrompt(distance1, distance2);


        if (button1_Pressed && button2_Pressed)
        {
            StartCoroutine(OpenDoorCoroutine());
        }
        
    }

    void OpenUIPrompt(float distance1, float distance2)
    {
        if (distance1 <= activationDistance)
        {
            uiPrompt1.SetActive(true);
            //Debug.Log("UI Prompt 1 should be visible");
        }
        else
        {
            uiPrompt1.SetActive(false);
            //Debug.Log("UI Prompt 1 hidden");
        }

        if (distance2 <= activationDistance)
        {
            uiPrompt2.SetActive(true);
            //Debug.Log("UI Prompt 2 should be visible");
        }
        else
        {
            uiPrompt2.SetActive(false);
            //Debug.Log("UI Prompt 2 hidden");
        }
    }

    public void pressButton1()
    {
        ButtonIsPressed(buttons.button1);
    }

    public void pressButton2()
    {
        ButtonIsPressed(buttons.button2);
    }


    public IEnumerator ButtonIsPressed(buttons button)
    {
        switch (button)
        {
            case buttons.button1:
                button1.SetActive(false);
                activatedButton1.SetActive(true);
                button1_Pressed = true;
                Debug.Log("The Left button is active!");
                yield return new WaitForSeconds(1);
                button1.SetActive(true);
                activatedButton1.SetActive(false);
                button1_Pressed = false;
                Debug.Log("The Left button is unactive");
                break;
            case buttons.button2:
                button2.SetActive(false);
                activatedButton2.SetActive(true);
                button2_Pressed = true;
                Debug.Log("The Right button is active!");
                yield return new WaitForSeconds(1);
                button2.SetActive(true);
                activatedButton2.SetActive(false);
                button2_Pressed = false;
                Debug.Log("The Right button is un   active!");
                break;

        }
    }
    
    IEnumerator OpenDoorCoroutine()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = rightDoor.transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(rightDoor.transform.localEulerAngles.x, openAngle, rightDoor.transform.localEulerAngles.z);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = Mathf.Clamp01(t);  // Ensure t is within 0 and 1

            rightDoor.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, t);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        rightDoor.transform.localRotation = endRotation;
        isOpening = false;  // Allow re-triggering
        Debug.Log("Door has been opened");
    }
}
