using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float CameraSensitivity;
    public float CameraFieldOfView;

    private Transform Player;
    private Transform Head;
    private Transform Body;

    private Vector3 HeadEulerRotation = new Vector3 (0, 0, 0);
    private Vector3 BodyEulerRotation = new Vector3(0, 0, 0);

    private Vector2 HeadRotationLimit = new Vector2(30f, 30f);

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Head = transform.parent.transform;
        Player = Head.parent.transform;
        Body = Player.Find("Body").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MouseVelocity = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        HeadEulerRotation += new Vector3(-MouseVelocity.y, MouseVelocity.x, 0) * CameraSensitivity;

        if (Mathf.Abs(HeadEulerRotation.x) > HeadRotationLimit.y) { HeadEulerRotation.x = HeadRotationLimit.y * Mathf.Sign(HeadEulerRotation.x); }

        if (Mathf.Abs(HeadEulerRotation.y - BodyEulerRotation.y) > HeadRotationLimit.x) 
        {
            BodyEulerRotation += new Vector3(0 , MouseVelocity.x, 0) * CameraSensitivity;
            Body.localEulerAngles = BodyEulerRotation;
        }

        Head.localEulerAngles = HeadEulerRotation;
    }
}
