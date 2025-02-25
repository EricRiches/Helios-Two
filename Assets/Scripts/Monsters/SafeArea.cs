using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField] MonsterHearable FootstepSounds;
    [SerializeField] GameObject ObstacleCarve;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FootstepSounds.enabled = true;
            ObstacleCarve.SetActive(false);
        }
    }

    public void ResetAfterDeath()
    {
        FootstepSounds.enabled = false;
        ObstacleCarve.SetActive(true);
    }
}
