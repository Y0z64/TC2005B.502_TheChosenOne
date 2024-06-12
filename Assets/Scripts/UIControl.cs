using UnityEngine;
using TMPro;

public class UIControl : MonoBehaviour
{
    public TMP_Text speedDisplay;
    public TMP_Text winningDisplay;
    public TMP_Text teleportCountDisplay;

    public bool hasWon = false;
    private CharacterController controller;

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
        speedDisplay.text = "Speed:\n" + horizontalSpeed.ToString();

        winningDisplay.gameObject.SetActive(hasWon);
    }

    public void UpdateTeleportCountDisplay(int count)
    {
        teleportCountDisplay.text = "Teleports Available: " + count;
    }
}
