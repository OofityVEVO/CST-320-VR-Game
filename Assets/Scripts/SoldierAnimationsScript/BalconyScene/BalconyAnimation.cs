using UnityEngine;
using System.Collections;

public class BalconyAnimation : MonoBehaviour
{
    public soldierAnimation soldierAnimation; // Assuming this is your animation controller script
    private Vector3 rotationDirection; // The direction to rotate toward and run in
    private float waitTime = 2f; // Time to wait after pointing (if needed)
    private float rotationSpeed = 5f; // Speed of rotation
    private float runSpeed = 7f; // Speed of running

    public bool willPoint = true; // Flag to determine if the guard will point

    public Animator animator; // Reference to the Animator component
     // Flag to control running state

    void Start()
    {
       
        

        // Start the sequence
        StartCoroutine(GuardSequence());
    }

    void Update()
    {
        // Handle continuous running after the sequence
        if (soldierAnimation.isRunning)
        {
            transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
        }

        if(transform.position.z >= 44)
        {
            Object.Destroy(this.gameObject);
            Debug.Log(transform.position.z);
            
        }
    }

    IEnumerator GuardSequence()
    {
        // Step 1: Trigger the pointing animation
        if (willPoint)
        {

            
            soldierAnimation.isPoint = true; // Assuming this triggers the animation
                                             // Or use animator.SetTrigger("Point") if you're using a trigger parameter
        }
        // Step 2: Wait for the pointing animation to finish
        float animationLength = 2.5f; 
        yield return new WaitForSecondsRealtime(animationLength);


        // Step 3: Rotate toward the designated direction
        

        // Flatten the rotation direction to only consider horizontal (X and Z) components
        Vector3 horizontalDirection = new Vector3(rotationDirection.x, 0, rotationDirection.z).normalized;

        // Calculate the target rotation based on the horizontal direction
        Quaternion targetRotation = Quaternion.LookRotation(horizontalDirection);

        // Smoothly rotate toward the target rotation
        while (Quaternion.Angle(transform.rotation, targetRotation) > 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Ensure final rotation is exact
        transform.rotation = targetRotation;
        // Step 4: Start running
        
        soldierAnimation.isRunning = true; // Enable running in Update()
    }
}