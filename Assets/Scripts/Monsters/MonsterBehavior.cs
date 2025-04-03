using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public bool canMove = true;
    protected Vector3 RespawnPosition;
    [HideInInspector] public bool CanKillPlayer = true;

    public virtual void TriggerHeardSounds(Vector3 SoundPosition, float soundPercent)
    {

    }

    protected Vector3 PlayerPosition;

    public virtual Vector3 SetPlayerPosition
    {
        set
        {
            PlayerPosition = value;
        }
    }

    public virtual void PlayerReset()
    {
        transform.position = RespawnPosition;
        CanKillPlayer = true;
    }
}
