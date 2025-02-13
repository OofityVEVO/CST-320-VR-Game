using UnityEngine;

public class MochiBehavior : MonoBehaviour
{
    //initializes the behaviors
    [SerializeField] private MochiBehavior standingBehavior;
    [SerializeField] private MochiBehavior followingBehavior;
    [SerializeField] private MochiBehavior interactingBehavior;

    private MochiBehavior currentBehavior;

    void Start()
    {
        // Start with standing behavior by default
        SwitchBehavior(followingBehavior);

        
        
    }

    void Update()
    {
        // Switch behavior based on input
        if (Input.GetKeyDown(KeyCode.Space)) // Space key, following behavior
        {
            Debug.Log("Switched to Following behavior.");
            SwitchBehavior(followingBehavior);
        }

        if (Input.GetMouseButtonDown(1)) // Right mouse click, stand behavior
        {
            Debug.Log("Switched to Standing behavior.");
            standingBehavior.SetPosition(transform.position);
            SwitchBehavior(standingBehavior);
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse click, Interact behavior
        {
            Debug.Log("Switched to Interacting behavior.");
            SwitchBehavior(interactingBehavior);
        }

        // enables the current behavior
        if (currentBehavior != null)
        {
            currentBehavior.enabled = true;
        }
        else
        {
            Debug.Log("currentBehavior is null! Assign behaviors in the Inspector.");
        }
    }

    void SwitchBehavior(MochiBehavior newBehavior)
    {
       
        if (newBehavior == null)
        {
            Debug.Log("Cannot switch behavior: newBehavior is null.");
            return;
        }

        // Disable the current behavior
        if (currentBehavior != null)
        {
            currentBehavior.enabled = false;
        }

        // Enable the new behavior
        currentBehavior = newBehavior;
        currentBehavior.enabled = true;
    }

    //function to set the position of the mochi, mainly used for standing behavior
    public virtual void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}