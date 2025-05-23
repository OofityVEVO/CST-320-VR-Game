using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }

public class PuzzleOne : MonoBehaviour
{
    public GameObjectEvent MochiEvent1; // Add this missing declaration

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
    public float animationMovementSpeed = 2f;
    public float climbDuration = 2f;

    private Rigidbody mochiRigidbody;
    private Quaternion uprightRotation = Quaternion.Euler(0f, 0f, 0f);

    public AudioSource source;
    public AudioClip doorOpen;

    [Header("SceneAudio")]
    public DialogueScene beginDialogueScene;
    public DialogueScene cageBreakScene;
    public DialogueScene DungeonDoorOpenScene;


    void Start()
    {
        if (MochiEvent1 == null)
            MochiEvent1 = new GameObjectEvent();

        source = GetComponent<AudioSource>();


        MochiEvent1.AddListener(PuzzleSolve);
        if (beginDialogueScene != null) { beginDialogueScene.PlayAudio(); }
        else { Debug.Log("No Dialogue Scene"); }
    }

    void OnTriggerEnter(Collider collide)
    {
        beginDialogueScene.keepPlaying = false;
        if (collide.gameObject.CompareTag("Chitterkin"))
        {
            mochiRigidbody = collide.gameObject.GetComponent<Rigidbody>();
            MochiEvent1.Invoke(collide.gameObject);
        }
    }

    void PuzzleSolve(GameObject Mochi)
    {
        if (!isOpening)
        {
            if (mochiRigidbody != null)
            {
                mochiRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                mochiRigidbody.velocity = Vector3.zero;
            }
            StartCoroutine(ClimbDownAndOpenDoor(Mochi));
        }
    }

    IEnumerator ClimbDownAndOpenDoor(GameObject Mochi)
    {
        cageBreakScene.StopAudio();
        Mochi_Controller controller = Mochi.GetComponent<Mochi_Controller>();
        controller.EnterCutsceneMode();
        Debug.Log("Mochi is on top of the cell bars!");
        isOpening = true;
        float elapsedTime = 0f;

        Mochi.transform.position = startPosition.position;
        Mochi.transform.rotation = uprightRotation;

        source.PlayOneShot(doorOpen);
        while (elapsedTime < climbDuration)
        {
            float t = elapsedTime / climbDuration;
            Mochi.transform.position = Vector3.Lerp(startPosition.position, climbEndPosition.position, t);
            Mochi.transform.rotation = uprightRotation;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Mochi.transform.position = climbEndPosition.position;
        Mochi.transform.rotation = uprightRotation;

        Debug.Log("Mochi has climbed down!");

        yield return new WaitForSeconds(1);

        StartCoroutine(OpenDoor());
        controller.ExitCutsceneMode();
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

        door.transform.localRotation = endRotation;
        isOpening = false;

        if (mochiRigidbody != null)
        {
            mochiRigidbody.constraints = RigidbodyConstraints.None;
        }

        Debug.Log("Door has been opened!");

        beginDialogueScene.StopAudio();
        if (DungeonDoorOpenScene != null)
        {
            DungeonDoorOpenScene.PlayAudio();
        }
    }
}