using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHearable : MonoBehaviour
{
    [SerializeField] bool PlayOnAwake;
    [Range(0f, 100f)]
    [SerializeField] float SoundVolume = 100;

    private void Start()
    {
        if (PlayOnAwake)
        {
            PlaySound();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        MonsterSenses[] monstersInScene = FindObjectsOfType<MonsterSenses>();

        foreach (MonsterSenses senser in monstersInScene)
        {
            senser.Hear_SoundPlayed(transform.position, SoundVolume);
        }
    }
}
