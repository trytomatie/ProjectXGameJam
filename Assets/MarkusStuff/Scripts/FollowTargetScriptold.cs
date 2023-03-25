using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetScriptold : MonoBehaviour
{
    /*[SerializeField] private float horizontalSensitivity = 5.0f;
    [SerializeField] private float horizontalLowerClampAngle = -10.0f;
    [SerializeField] private float horizontalUpperClampAngle = 75.0f;

    private Vector2 smoothedMouseMovement;
    private Vector3 currentRotation;

    private void Start()
    {
        currentRotation = transform.localEulerAngles;
    }

    private void Update()
    {
        // Get smoothed mouse movement
        smoothedMouseMovement.x += Input.GetAxisRaw("Mouse X");

        // Clamp horizontal rotation
        smoothedMouseMovement.x = Mathf.Clamp(smoothedMouseMovement.x, horizontalLowerClampAngle, horizontalUpperClampAngle);

        // Calculate rotation angles
        float horizontalRotation = smoothedMouseMovement.x * horizontalSensitivity;

        // Apply rotation to object
        currentRotation += new Vector3(0.0f, horizontalRotation, 0.0f) * Time.deltaTime;

        // Set new rotation angles
        transform.localEulerAngles = currentRotation;
    }*/
}
