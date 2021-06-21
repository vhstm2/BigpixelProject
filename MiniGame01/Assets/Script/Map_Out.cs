using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Out : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rid = other.GetComponent<Rigidbody>();
            rid.AddForce(-rid.transform.forward * 100, ForceMode.Impulse);
        }
    }
}