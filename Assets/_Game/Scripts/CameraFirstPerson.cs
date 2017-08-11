using UnityEngine;
using System.Collections;

public class CameraFirstPerson : MonoBehaviour
{
    public float xMouseSensibility = 100.0f;
    public float yMouseSensibility = 100.0f;

    public Transform center = null;
    public Vector3 offset = new Vector3(0.0f, 0.0f, -2.0f);

    private float yRotation = 0.0f;
    private float xRotation = 0.0f;

    private void Update()
    {
        xRotation += Input.GetAxis("Mouse X") * xMouseSensibility * Time.deltaTime;
        xRotation = Mathf.Repeat(xRotation, 360.0f);
        yRotation += -Input.GetAxis("Mouse Y") * yMouseSensibility * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, -40.0f, 80.0f);  
    }

    private void FixedUpdate()
    {
        transform.position = center.position + offset;

        transform.RotateAround(center.position, Vector3.up, xRotation);
        transform.RotateAround(center.position, Vector3.right, yRotation);

        transform.LookAt(center, Vector3.up);
    }
}
