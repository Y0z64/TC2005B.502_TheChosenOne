using UnityEngine;

public class LookArround : MonoBehaviour
{
    private float mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
    private int inverted = PlayerPrefs.GetInt("Inverted");

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Check if inverted is set to invert the mouse movement
        if (inverted == 1)
        {
            xRotation += mouseY; // Invert the Y axis movement
        }
        else
        {
            xRotation -= mouseY; // Normal Y axis movement
        }

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}