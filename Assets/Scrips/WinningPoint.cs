using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningPoint : MonoBehaviour
{
    public UIControl uiControl;
    public Transform playerTransform;
    public float winDistance = 4f; // Adjust the distance threshold as needed

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the player and the winning point
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // If the player is within the win distance, update the condition
        if (distanceToPlayer <= winDistance)
        {
            uiControl.hasWon = true;
        }
        else
        {
            uiControl.hasWon = false;
        }
    }
}
