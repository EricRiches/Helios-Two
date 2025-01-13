using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
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
}
