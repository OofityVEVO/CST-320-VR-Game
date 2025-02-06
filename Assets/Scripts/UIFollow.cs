using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class UIFollow : MonoBehaviour
{
    [SerializeField] private Transform playerHead;  // The player's head (XR Camera)
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, -0.2f, 0.5f); // Offset for better UI positioning
    [SerializeField] private GameObject uiPanel; // Reference to the UI Panel

    public XRNode inputSource = XRNode.RightHand; // Change to LeftHand if needed
    private bool isVisible = false;

    private bool played = false;

    public AudioClip openUIAudio;
    public AudioClip closeUIAudio;


    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();

        if (uiPanel != null)
            uiPanel.SetActive(false); // Start hidden
    }

    private void Update()
    {
        // Update UI position and rotation to follow the player's head
        if (playerHead != null)
        {
            Vector3 targetPosition = playerHead.position + playerHead.forward * offset.z + playerHead.up * offset.y + playerHead.right * offset.x;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            transform.LookAt(playerHead);
            transform.Rotate(0f, 180f, 0f); // Flip to face the user
        }

        // Check if the user is pressing the button to toggle the UI
        if (InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), InputHelpers.Button.PrimaryButton, out bool isPressed) && isPressed)
        {
            ToggleUI(true);
        }
        else
        {
            ToggleUI(false);

        }

        if(played)
        {
            source.PlayOneShot(closeUIAudio);
            played = false;
        }
    }

    private void ToggleUI(bool state)
    {
        if (uiPanel != null && state != isVisible)
        {
            isVisible = state;
            uiPanel.SetActive(state);
        }

        if(!played)
        {
            source.PlayOneShot(openUIAudio);
            played = true;
        }
    }
}
