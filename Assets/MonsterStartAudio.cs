using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStartAudio : MonoBehaviour
{
    public StudioEventEmitter monsterFootstep;

    private void Start()
    {
        monsterFootstep.Play();
    }

}
