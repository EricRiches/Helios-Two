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
    bool isCurrentlyChasingPlayer;

    [Header("")]
    [SerializeField] float WaitTime;

    [HideInInspector]public float speedMultiplier = 1;

    void Start()
    {
        controller = GetComponent<NavMeshAgent>();

        Vector3 roamPosition;

        if (grid == null)
        {
            grid = FindObjectOfType<MonsterNavigationGrid>();
        }

        if (grid.FindRoamPosition(out roamPosition))
        {
            controller.destination = roamPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grid == null)
        {
            grid = FindObjectOfType<MonsterNavigationGrid>();
        }

        switch (currentBehavior)
        {
            case PrimaryMonsterBehvaior.Roam:

                controller.speed = RoamSpeed * speedMultiplier;
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
                    controller.speed = SearchSpeed * speedMultiplier;
                    if (controller.remainingDistance <= 0.1f)
                    {
                        WaitTime = SawWaitAtPointTime;
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
            case PrimaryMonsterBehvaior.SawPlayer:
                if (WaitTime <= 0)
                {
                    controller.speed = ChaseSpeed * speedMultiplier;
                    if (controller.remainingDistance <= 0.1f)
                    {
                        WaitTime = HeardWaitAtPointTime;
                        isCurrentlyChasingPlayer = false;
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
                    }
                }
                break;
        }
    }

    public override void TriggerHeardSounds(Vector3 SoundPosition, float soundPercent)
    {
        if (currentBehavior != PrimaryMonsterBehvaior.SawPlayer)
        {
            TriggerHeardSoundsCode(SoundPosition, soundPercent);
        }
        else
        {
            if (!isCurrentlyChasingPlayer)
            {
                controller.destination = SoundPosition;
            }
        }
    }

    void TriggerHeardSoundsCode(Vector3 SoundPosition, float soundPercent)
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
            isCurrentlyChasingPlayer = true;
        }
    }
}

public enum PrimaryMonsterBehvaior
{
    Roam,
    HeardPlayer,
    SawPlayer
}