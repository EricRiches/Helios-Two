using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeEntered : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CarryOvers.SetBridgeOpen(true);
        }
    }
}
