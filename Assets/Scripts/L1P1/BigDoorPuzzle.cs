using System.Collections;
using UnityEngine;

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

    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.CompareTag("Player") && !isOpening)
        {
            isOpening = true;
            StartCoroutine(OpenDoorCoroutine());
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
