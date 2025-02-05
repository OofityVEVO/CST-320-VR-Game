using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    /*
     * Resource Used : https://sneakydaggergames.medium.com/vr-in-unity-how-to-create-a-continuous-movement-system-track-real-space-movement-2bd6fe31df0a
     * Implemented Movement based on direction of which the player is turned player will stay on the ground
     * thanks to layerMasks~
     * 
     * Summary:
     * 
     * 
     */
    public XRNode inputSource;
    private Vector2 inputAxis;

    private CharacterController character;
    public float speed = 3f;
    public float heightOffSet = 1f;
    private float fallingSpeed = 0;
    private float gravity = -9.81f;

    public XROrigin xrOrigin;

    public LayerMask groundLayer;


    void Start()
    {
        character = GetComponent<CharacterController>();
        xrOrigin = GetComponent<XROrigin>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    public void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, xrOrigin.Camera.transform.eulerAngles.y, 0);
        Vector3 headDirection = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(headDirection * speed * Time.deltaTime);


        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);

        CapsuleFollowHead();
    }

    private bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;

        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;

    }

    void CapsuleFollowHead()
    {
        float cameraHeight = xrOrigin.Camera.transform.localPosition.y + heightOffSet;
        character.height = cameraHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(xrOrigin.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, cameraHeight / 2 + character.skinWidth, capsuleCenter.z);
    }
}



