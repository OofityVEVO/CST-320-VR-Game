using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celebrate : MonoBehaviour
{
    public float jumpForce = 5f;      // Strength of the jump
    public float jumpInterval = 2f;   // Time between jumps
    public float spinDuration = 1f;   // Time it takes to complete a full 360 spin

    private Rigidbody rb;
    private bool isGrounded = true;
    private float spinTimer = 0f;
    private bool isSpinning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(Jump), 0f, jumpInterval);
    }

    void Update()
    {
        if (isSpinning)
        {
            // Calculate rotation per frame
            float rotationAmount = (360f / spinDuration) * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationAmount);

            // Update the spin timer
            spinTimer += Time.deltaTime;

            // Stop spinning after completing a full 360-degree turn
            if (spinTimer >= spinDuration)
            {
                isSpinning = false;
            }
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            isSpinning = true;
            spinTimer = 0f; // Reset spin timer
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}
