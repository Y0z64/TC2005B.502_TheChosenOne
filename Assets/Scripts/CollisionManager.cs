using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    public UIControl uiControl;
    public Transform playerTransform;

    public float winDistance = 4f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Coin") || other.transform.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            if (other.transform.CompareTag("Enemy"))
            { 
                uiControl.hasWon = true;
            }
        }
    }
}