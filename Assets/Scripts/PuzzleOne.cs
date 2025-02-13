using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class PuzzleOne : MonoBehaviour
{
    public UnityEvent MochiEvent1;

    [Header("Door Settings")]
    public GameObject door;
    public float openAngle = 90f; // Adjust to match your door's rotation
    public float closeAngle = 0f;
    public float speed = 2f; // Adjust speed for smoother opening
    private bool isOpening = false;
    public float duration = 2f; 

    void Start()
    {
        MochiEvent1 = new UnityEvent(); 
        MochiEvent1.AddListener(PuzzleSolve); 
    }


    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.CompareTag("Chitterkin")) 
        {
            MochiEvent1.Invoke();
            MochiEvent1.RemoveListener(PuzzleSolve); 
        }
    }

    void PuzzleSolve()
    {
        if (!isOpening)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        isOpening = true;  // Prevent multiple calls
        float elapsedTime = 0f;
        float duration = 2f; // Adjust duration for smoother transition

        Quaternion startRotation = door.transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(door.transform.localEulerAngles.x, openAngle, door.transform.localEulerAngles.z);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = Mathf.Clamp01(t);  // Ensure t is within 0 and 1

            door.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, t);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        door.transform.localRotation = endRotation;
        isOpening = false;  // Allow re-triggering
        Debug.Log("Door has been opened");
    }

}
