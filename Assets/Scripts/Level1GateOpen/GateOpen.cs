using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
    [SerializeField] GameObject LeftDoor;
    [SerializeField] GameObject RightDoor;
    [SerializeField] GameObject PrisonDoor;

    public float RotationAmount = 120f;
    public float RotationSnap = 1f;
    private float currentRotation = 0f;
    private bool isOpening = false;

    // Update is called once per frame
    void Update()
    {
        if (isOpening && currentRotation < RotationAmount)
        {
            float rotationStep = Mathf.Min(RotationSnap, RotationAmount - currentRotation);

            LeftDoor.transform.Rotate(0, rotationStep, 0);
            RightDoor.transform.Rotate(0, -rotationStep, 0);
            PrisonDoor.transform.Rotate(0, (float)(rotationStep / 1.5), 0);

            currentRotation += rotationStep;
        }
    }

    public void RunGateOpen()
    {
        Debug.Log("Opening Gate");
        isOpening = true;
        currentRotation = 0f;
    }
}
