using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Coin")
        {
            Destroy(other.gameObject);
        }
    }
}