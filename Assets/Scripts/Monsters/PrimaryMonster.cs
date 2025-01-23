using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PrimaryMonster : MonsterBehavior
{
    NavMeshAgent controller;
    [SerializeField] MonsterNavigationGrid grid;

    [SerializeField] PrimaryMonsterBehvaior currentBehavior;

    [Header("Roam")]
    [SerializeField] float RoamSpeed;
    [SerializeField] float RoamWaitAtPointTime;

    [Header("Hear")]
    [SerializeField] float SearchSpeed;
    [SerializeField] float HeardWaitAtPointTime;
    [SerializeField, Range(0, 100)] float HearStateChangerPercent;
    bool ContinueWalkingAfterHearing = false;

    [Header("See")]
    [SerializeField] float ChaseSpeed;
    [SerializeField] float SawWaitAtPointTime;

    [Header("")]
    [SerializeField] float WaitTime;

    void Start()
    {
        controller = FindObjectOfType<NavMeshAgent>();

        Vector3 roamPosition;

        if (grid.FindRoamPosition(out roamPosition))
        {
            controller.destination = roamPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentBehavior)
        {
            case PrimaryMonsterBehvaior.Roam:

                controller.speed = RoamSpeed;
                if (WaitTime > 0)
                {
                    WaitTime -=Time.deltaTime;

                    if (WaitTime <= 0)
                    {
                        Vector3 roamPosition;

                        if (grid.FindRoamPosition(out roamPosition))
                        {
                            controller.destination = roamPosition;
                        }
                    }
                }
                else
                {
                    if (controller.remainingDistance <= 0.1f)
                    {
                        WaitTime = RoamWaitAtPointTime;
                    }
                }
                break;
            case PrimaryMonsterBehvaior.HeardPlayer:
                if (WaitTime <= 0)
                {
                    controller.speed = SearchSpeed;
                    if (controller.remainingDistance <= 0.1f)
                    {
                        WaitTime = HeardWaitAtPointTime;
                    }
                }
                else
                {
                    WaitTime -= Time.deltaTime;

                    if (WaitTime <= 0)
                    {
                        currentBehavior = PrimaryMonsterBehvaior.Roam;
                        if (!ContinueWalkingAfterHearing)
                        {
                            Vector3 roamPosition;

                            if (grid.FindRoamPosition(out roamPosition))
                            {
                                controller.destination = roamPosition;
                            }
                        }
                        ContinueWalkingAfterHearing = false;
                    }
                }
                break;
        }
    }

    public override void TriggerHeardSounds(Vector3 SoundPosition, float soundPercent)
    {
        currentBehavior = PrimaryMonsterBehvaior.HeardPlayer;
        if (soundPercent >= HearStateChangerPercent)
        {
            controller.destination = SoundPosition;
        }
        else
        {
            controller.speed = 0;
            WaitTime = HeardWaitAtPointTime;
            ContinueWalkingAfterHearing = true;
        }
    }

    public override Vector3 SetPlayerPosition
    {
        set
        {
            base.SetPlayerPosition = value;
            currentBehavior = PrimaryMonsterBehvaior.SawPlayer;
            controller.destination = value;
        }
    }
}

public enum PrimaryMonsterBehvaior
{
    Roam,
    HeardPlayer,
    SawPlayer
}