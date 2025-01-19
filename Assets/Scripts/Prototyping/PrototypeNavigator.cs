using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PrototypeNavigator : MonsterBehavior
{
    NavMeshAgent controller;
    [SerializeField] MonsterNavigationGrid grid;

    [SerializeField] PrototypeMonsterBehvaior currentBehavior;

    void Start()
    {
        controller = FindObjectOfType<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentBehavior)
        {
            case PrototypeMonsterBehvaior.Roam:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Vector3 roamPosition;

                    if (grid.FindRoamPosition(out roamPosition))
                    {
                        controller.destination = roamPosition;
                    }
                }
                break;
        }
    }

    public override void TriggerHeardSounds(Vector3 SoundPosition, float soundPercent)
    {
        currentBehavior = PrototypeMonsterBehvaior.HeardPlayer;
        controller.destination = SoundPosition;
    }

    public override Vector3 SetPlayerPosition 
    {        
        set
        {
            base.SetPlayerPosition = value;
            currentBehavior = PrototypeMonsterBehvaior.SawPlayer;
            controller.destination = value;
        }
    }
}

public enum PrototypeMonsterBehvaior
{
    Roam,
    HeardPlayer,
    SawPlayer
}