using UnityEngine;
using TMPro;

public class UIControl : MonoBehaviour
{   
    float speed;
    public TMP_Text speedDisplay;
    public TMP_Text winningDisplay;
    public TMP_Text teleportCountDisplay; // Add this line

    public bool hasWon = false;
    public CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {   
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = new Vector3(controller.velocity.x, 0, horizontalVelocity.z);

        // The speed on the x-z plane ignoring any speed
        float horizontalSpeed = horizontalVelocity.magnitude;
        speed = horizontalSpeed;
        speedDisplay.text = "Speed:\n" + horizontalSpeed.ToString();

        winningDisplay.gameObject.SetActive(hasWon);
    }

    // Add this method to update the teleport count display
    public void UpdateTeleportCountDisplay(int count)
    {
        teleportCountDisplay.text = "Teleports Available: " + count;
    }
}
