using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Coin") || other.transform.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }   
    }
}