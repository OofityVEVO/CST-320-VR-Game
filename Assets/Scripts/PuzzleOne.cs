using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }

public class PuzzleOne : MonoBehaviour
{
    public GameObjectEvent MochiEvent1;

    [Header("Door Settings")]
    public GameObject door;
    public float openAngle = 90f;
    public float closeAngle = 0f;
    public float speed = 2f;
    public float duration = 2f;

    private bool isOpening = false;

    [Header("Animation Settings")]
    public Transform startPosition;
    public Transform climbEndPosition;
    public float animationMovementSpeed = 2f; // Speed of the climbing animation
    public float climbDuration = 2f; // Duration of climbing animation

    void Start()
    {
        if (MochiEvent1 == null)
            MochiEvent1 = new GameObjectEvent();

        MochiEvent1.AddListener(PuzzleSolve);
    }

    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.CompareTag("Chitterkin"))
        {
            MochiEvent1.Invoke(collide.gameObject);
        }
    }

    void PuzzleSolve(GameObject Mochi)
    {
        if (!isOpening)
        {
            StartCoroutine(ClimbDownAndOpenDoor(Mochi));
        }
    }

    IEnumerator ClimbDownAndOpenDoor(GameObject Mochi)
    {
        Debug.Log("Mochi is on top of the cell bars!");
        isOpening = true;
        float elapsedTime = 0f;


        Mochi.transform.position = startPosition.position;

        while (elapsedTime < climbDuration)
        {
            float t = elapsedTime / climbDuration; // Normalized time (0 to 1)
            Mochi.transform.position = Vector3.Lerp(startPosition.position, climbEndPosition.position, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Mochi.transform.position = climbEndPosition.position;

        Debug.Log("Mochi has climbed down!");

        yield return new WaitForSeconds(1);

        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        Debug.Log("Opening the door...");
        float elapsedTime = 0f;

        Quaternion startRotation = door.transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(door.transform.localEulerAngles.x, openAngle, door.transform.localEulerAngles.z);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            door.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure door is fully opened
        door.transform.localRotation = endRotation;
        isOpening = false;

        Debug.Log("Door has been opened!");
    }
}
