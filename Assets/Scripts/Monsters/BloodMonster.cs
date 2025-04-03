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

    [Header("Death")]
    [SerializeField] Animator BloodMonsterAnimator;
    [SerializeField] float DeathDistance;
    [SerializeField] Transform DeathCamera;

    Transform playerLocation;

    [HideInInspector] public bool canKillPlayer;

    void Start()
    {
        controller = FindObjectOfType<NavMeshAgent>();
        playerLocation = FindObjectOfType<SC_FPSController>().transform;
        RespawnPosition = transform.position;
        CanKillPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return; // allows you to disable the enemy movement - used in freeze spray.
        }

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

        if (Vector3.Distance(transform.position, playerLocation.position) <= DeathDistance && CanKillPlayer)
        {
            Debug.Log("Player Died");

            BloodMonsterAnimator.SetTrigger("Is Dead");
            DeathCamera.gameObject.SetActive(true);
            CanKillPlayer = false;
        }
    }

    public override void TriggerHeardSounds(Vector3 SoundPosition, float soundPercent)
    {
        currentBehavior = BloodMonsterMonsterBehvaior.HeardPlayer;
        controller.destination = SoundPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DeathDistance);
    }

    public override void PlayerReset()
    {
        Vector3 roamPosition;

        if (grid.FindRoamPosition(out roamPosition))
        {
            controller.destination = roamPosition;
        }

        base.PlayerReset();
    }
}


public enum BloodMonsterMonsterBehvaior
{
    Roam,
    HeardPlayer
}
