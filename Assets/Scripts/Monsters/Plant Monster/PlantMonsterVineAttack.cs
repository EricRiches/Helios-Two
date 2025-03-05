using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlantMonsterVineAttack : MonoBehaviour
{
    PlantMonsterManager manager;
    [SerializeField] Animator vineAnimator;
    float Timer;

    private void Start()
    {
        manager = FindAnyObjectByType<PlantMonsterManager>();
        Timer = Random.Range(manager.VineSwingTimes.x, manager.VineSwingTimes.y);
    }

    private void Update()
    {
        if (!manager.isPlantDead)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0)
            {
                vineAnimator.SetTrigger("Attack");
                Timer = Random.Range(manager.VineSwingTimes.x, manager.VineSwingTimes.y);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            vineAnimator.SetTrigger("Player Hit");
        }
    }
}
