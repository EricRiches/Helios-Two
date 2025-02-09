using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMonsterVineAttack : MonoBehaviour
{
    PlantMonsterManager manager;

    private void Start()
    {
        manager = FindAnyObjectByType<PlantMonsterManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            manager.HitPlayer(name);
        }
    }
}
