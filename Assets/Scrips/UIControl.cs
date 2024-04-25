using UnityEngine;
using TMPro;

public class UIControl : MonoBehaviour
{   
    float speed;
    public TMP_Text speedDisplay;
    public TMP_Text winningDisplay;

    public bool hasWon = false;
    public CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {   
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        // The speed on the x-z plane ignoring any speed
        float horizontalSpeed = horizontalVelocity.magnitude;
        speed = horizontalSpeed;
        speedDisplay.text = "Speed:\n" + horizontalSpeed.ToString();

        winningDisplay.gameObject.SetActive(hasWon);
    }
}
