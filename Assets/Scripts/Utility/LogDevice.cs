using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogDevice : MonoBehaviour
{
    [SerializeField] AudioClip audioLog;
    //[SerializeField] List<AudioClip> audioLogs;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    bool CheckIfPlayed()
    {
        return CarryOvers.HasLogObjectPlayed(gameObject);
    }

    void PlayAudioClip()
    {
        audioLog.LoadAudioData();
        //audioLogs[CarryOvers.GetIndex()].LoadAudioData();
        CarryOvers.AppendLogObject(gameObject);
        CarryOvers.LogTickUp();
        audioSource.Play();

    }

    public void PauseAudio()
    {
        if (audioSource.isPlaying) audioSource.Pause();
        else audioSource.UnPause();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!CheckIfPlayed() & audioLog != null)
            {
                PlayAudioClip();
            }
        }
    }
}
