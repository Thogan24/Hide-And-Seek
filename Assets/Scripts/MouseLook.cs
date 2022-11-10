using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 250f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        //Locks the Cursor so that user cannot see it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Makes it so that the user cannot look up / down beyond a certain point

        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}