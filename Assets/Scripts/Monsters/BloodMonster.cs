using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BloodMonster : MonsterBehavior
{
    NavMeshAgent controller;
    [SerializeField] MonsterNavigationGrid grid;

    [SerializeField] BloodMonsterMonsterBehvaior currentBehavior;

    [Header("Movement")]
    [SerializeField] float SecondsItTakesForMovementCycle = 1;
    [SerializeField] float RoamSpeed;
    [SerializeField] float ChaseSpeed;
    float MovementTimer;

    void Start()
    {
        controller = FindObjectOfType<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 0;

        switch (currentBehavior)
        {
            case BloodMonsterMonsterBehvaior.Roam:
                moveSpeed = RoamSpeed;
                if (controller.remainingDistance <= 0.25f)
                {
                    Vector3 roamPosition;

                    if (grid.FindRoamPosition(out roamPosition))
                    {
                        controller.destination = roamPosition;
                    }
                }
                break;
            case BloodMonsterMonsterBehvaior.HeardPlayer:
                moveSpeed = ChaseSpeed;
                if (controller.remainingDistance <= 0.25f)
                {
                    currentBehavior = BloodMonsterMonsterBehvaior.Roam;
                }

                break;
        }

        MovementTimer += Time.deltaTime;
        if (MovementTimer >= SecondsItTakesForMovementCycle) 
        {
            MovementTimer -= SecondsItTakesForMovementCycle * 2;
        }
        controller.speed = (Mathf.Sin((MovementTimer / SecondsItTakesForMovementCycle) * Mathf.PI * 2) + 1) * (moveSpeed / 2);
    }

    public override void TriggerHeardSounds(Vector3 SoundPosition, float soundPercent)
    {
        currentBehavior = BloodMonsterMonsterBehvaior.HeardPlayer;
        controller.destination = SoundPosition;
    }
}


public enum BloodMonsterMonsterBehvaior
{
    Roam,
    HeardPlayer
}
