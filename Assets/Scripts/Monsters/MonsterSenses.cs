using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSenses : MonoBehaviour
{
    [SerializeField] MonsterBehavior attackedMonster;

    [Header("Vision")]
    [SerializeField] bool CanSee = true;
    [SerializeField] bool ShowSolidVisionSphere = false;
    [SerializeField] Transform Player;
    [SerializeField] Transform Eye_location;
    [SerializeField] Transform Eye_ConeHelper;
    [SerializeField] Transform Eye_GizmoHelper;
    [SerializeField] float Vision_Distance;
    [SerializeField] float Vision_Degrees;
    [SerializeField, Range(0, 1)] float Prototype_VisionSphere;
    [SerializeField] LayerMask SightMask;

    [Header("Hearing")]
    [SerializeField] bool CanHear = true;
    [SerializeField] Transform Hear_InputPosition;
    [SerializeField] float Hear_Distance;
    [Range(0f, 100f)]
    [SerializeField] float Hear_VolumeCutOff;

    private void Start()
    {
        isPlayerInPlayMode = true;
    }

    public void Hear_SoundPlayed(Vector3 soundPosition, float soundVolume)
    {
        if (CanHear)
        {
            float distanceToSound = Vector3.Distance(Hear_InputPosition.position, soundPosition);

            if (distanceToSound <= Hear_Distance)
            {
                float HearTotalPercent = soundVolume * ((Hear_Distance - distanceToSound) / Hear_Distance);
                Debug.Log(HearTotalPercent.ToString() + "% of the sound was heard");
                if (HearTotalPercent >= Hear_VolumeCutOff)
                {
                    attackedMonster.TriggerHeardSounds(soundPosition, HearTotalPercent / Hear_VolumeCutOff);
                }
            }
        }
    }

    int isPlayerInAngle = 0;
    bool isPlayerInPlayMode = false;

    private void Update()
    {
        if (CanSee)
        {
            float PlayerDistanceAwayFromEyes = Vector3.Distance(Eye_location.position, Player.position);

            if (PlayerDistanceAwayFromEyes <= Vision_Distance)
            {
                Eye_ConeHelper.LookAt(Player.position);
                float AngleBetween = Vector3.Angle(Eye_ConeHelper.forward, Eye_location.forward);

                if (AngleBetween <= Vision_Degrees)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(Eye_ConeHelper.position, Eye_ConeHelper.forward, out hit, Vision_Distance, SightMask))
                    {
                        if (hit.transform == Player)
                        {
                            isPlayerInAngle = 2;
                            attackedMonster.SetPlayerPosition = hit.point;
                        }
                        else
                        {
                            isPlayerInAngle = 1;
                        }
                    }
                    else
                    {
                        isPlayerInAngle = 1;
                    }
                }
                else
                {
                    isPlayerInAngle = 1;
                }
            }
            else
            {
                isPlayerInAngle = 0;
            }
        }
        else
        {
            isPlayerInAngle = 0;
        }
    }

    private void OnDrawGizmos/*Selected*/()
    {
        if (CanHear)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Hear_InputPosition.position, Hear_Distance);
        }

        if (CanSee)
        {
            Vector3 Point1 = Vector3.zero;
            Vector3 Point2 = Vector3.zero;

            Eye_GizmoHelper.rotation = Eye_location.rotation;
            Eye_GizmoHelper.Rotate(Vector3.up, Vision_Degrees);
            Point1 = Eye_GizmoHelper.position + (Eye_GizmoHelper.forward * Vision_Distance);

            Eye_GizmoHelper.Rotate(Vector3.up, -2 * Vision_Degrees);
            Point2 = Eye_GizmoHelper.position + (Eye_GizmoHelper.forward * Vision_Distance);

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(Eye_location.position, Eye_location.position + (Eye_location.forward * Vision_Distance));
            Gizmos.DrawLine(Eye_location.position, Point1);
            Gizmos.DrawLine(Eye_location.position, Point2);

            if (ShowSolidVisionSphere)
            {
                Gizmos.DrawSphere(Eye_location.position, Vision_Distance);
            }
            else
            {
                Gizmos.DrawWireSphere(Eye_location.position, Vision_Distance);
            }

            if (isPlayerInPlayMode)
            {
                if (isPlayerInAngle == 0)
                {
                    Gizmos.color = Color.red;
                }
                else if (isPlayerInAngle == 1)
                {
                    Gizmos.color = Color.yellow;
                }
                else if (isPlayerInAngle == 2)
                {
                    Gizmos.color = Color.green;
                }

                Gizmos.DrawLine(Eye_location.position, Player.position);
            }
        }
    }
}
