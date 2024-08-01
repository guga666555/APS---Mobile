using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private float swaySmooth = 8;
    [SerializeField] private float swaySmoothMultiplier = 2;
    [SerializeField] private float bobMultiplier = 0.002f;
    [SerializeField] private float bobFrequency = 10.0f;
    [SerializeField] private float bobSmooth = 8f;
    private Vector3 StartPos;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();

        StartPos = transform.localPosition;
    }

    private void Update()
    {
        this.CameraSway();
        this.CameraHeadBob();
    }

    private void CameraSway()
    {
        float swayX = playerController.cameraJoystick.Vertical * swaySmoothMultiplier;
        float swayY = playerController.cameraJoystick.Horizontal * swaySmoothMultiplier;
        Quaternion rotationX = Quaternion.AngleAxis(-swayY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(-swayX, Vector3.up);
        Quaternion targetRotation = rotationX * rotationY;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, swaySmooth * Time.deltaTime);
    }

    private void CameraHeadBob()
    {
        if (playerController.characterController.velocity.magnitude > 0)
        {
            Vector3 cameraBob = Vector3.zero;
            cameraBob.y += Mathf.Lerp(cameraBob.y, Mathf.Sin(Time.time * bobFrequency) * bobMultiplier * 1.4f, bobSmooth * Time.deltaTime);
            cameraBob.x += Mathf.Lerp(cameraBob.x, Mathf.Cos(Time.time * bobFrequency / 2f) * bobMultiplier * 1.6f, bobSmooth * Time.deltaTime);
            transform.localPosition += cameraBob;
        }

        if (transform.localPosition != StartPos)
            transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
    }
}
