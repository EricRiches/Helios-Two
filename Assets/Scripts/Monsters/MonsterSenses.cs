using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSenses : MonoBehaviour
{
    [SerializeField] MonsterBehavior attackedMonster;

    [Header("Vision")]
    [SerializeField] float Vision_Distance;

    [Header("Hearing")]
    [SerializeField] Transform Hear_InputPosition;
    [SerializeField] float Hear_Distance;
    [Range(0f, 100f)]
    [SerializeField] float Hear_VolumeCutOff;

    public void Hear_SoundPlayed(Vector3 soundPosition, float soundVolume)
    {
        float distanceToSound = Vector3.Distance(Hear_InputPosition.position, soundPosition);

        if (distanceToSound <= Hear_Distance)
        {
            float HearTotalPercent = soundVolume * ((Hear_Distance - distanceToSound) / Hear_Distance);
            Debug.Log(HearTotalPercent.ToString() + "% of the sound was heard");
            if (HearTotalPercent >= Hear_VolumeCutOff)
            {
                attackedMonster.TriggerHeardSounds(soundPosition);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Hear_InputPosition.position, Hear_Distance);
    }
}
