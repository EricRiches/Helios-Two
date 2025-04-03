using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffDeath : MonoBehaviour
{
    RespawnManager respawn;

    // Start is called before the first frame update
    void Start()
    {
        respawn = FindObjectOfType<RespawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            respawn.OpenDeathUI();
        }
    }
}
