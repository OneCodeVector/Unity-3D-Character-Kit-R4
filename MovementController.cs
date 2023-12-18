using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpForce;
    public float Stamina;

    // Player Rig Parts.
    private Transform Body;
    private Transform Head;
    private Transform Camera;

    // Components
    private CharacterController Controller;

    public float GravitationalForce = 1f;
    private float VerticalVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch Player Rig Parts.
        Head = transform.Find("Head").transform;
        Body = transform.Find("Body").transform;
        Camera = Head.Find("Camera").transform;

        // Fetch Components.
        Controller = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 AxisVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // Linear algebra is fun!
        Vector3 MovementAxis = Camera.right * AxisVector.x + Camera.forward * AxisVector.z; // Relative to camera.

        if (MovementAxis.sqrMagnitude > 1 ) { MovementAxis.Normalize(); } // Prevent strafing

        // Gravity system.
        if (Controller.isGrounded) // If touching ground then...
        {
            VerticalVelocity = 0f; // Stop falling.

            if (Input.GetKey(KeyCode.Space)) // If jump key then...
            {
                VerticalVelocity = JumpForce; // Apply positive vertical velocity.
            }
        } else // If not on ground...
        {
            VerticalVelocity -= GravitationalForce * Time.deltaTime; // Add negative vertical velocity.
        }

        Vector3 MovementVector = new Vector3(MovementAxis.x, 0, MovementAxis.z) * MovementSpeed; // All movement combined into a single vector.
        MovementVector.y = VerticalVelocity; // Vertical velocity wont be effected by movement speed.

        Controller.Move(MovementVector * Time.deltaTime); // Move player.
    }
}
