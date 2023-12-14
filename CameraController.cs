using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Settings, FOV does not work yet.
    public float CameraSensitivity;
    public float CameraFieldOfView;

    // Player Rig Parts
    private Transform Player;
    private Transform Head;
    private Transform Body;

    // Rotation Vectors
    private Vector3 HeadEulerRotation = new Vector3 (0, 0, 0);
    private Vector3 BodyEulerRotation = new Vector3(0, 0, 0);

    // Limit of the heads rotation.
    private Vector2 HeadRotationLimit = new Vector2(30f, 30f);

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the game screen.

        // Initialize the Player's rig parts.
        Head = transform.parent.transform;
        Player = Head.parent.transform;
        Body = Player.Find("Body").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MouseVelocity = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // Measure velocity of the mouse.

        HeadEulerRotation += new Vector3(-MouseVelocity.y, MouseVelocity.x, 0) * CameraSensitivity; // Update the rotation vector of the head.

        // Stop the head from looking too far up or down.
        if (Mathf.Abs(HeadEulerRotation.x) > HeadRotationLimit.y) { HeadEulerRotation.x = HeadRotationLimit.y * Mathf.Sign(HeadEulerRotation.x); }

        // Move the body when the head reaches it's rotation limit.
        if (Mathf.Abs(HeadEulerRotation.y - BodyEulerRotation.y) > HeadRotationLimit.x) 
        {
            BodyEulerRotation += new Vector3(0 , MouseVelocity.x, 0) * CameraSensitivity; // Update the rotation vector of the player.
            Body.localEulerAngles = BodyEulerRotation; // Set the body's euler angle to it's rotation.
        }

        Head.localEulerAngles = HeadEulerRotation; // Set the head's euler angle to it's rotation.
    }
}
