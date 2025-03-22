using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "HeliosTao/Subtitle")]
[System.Serializable]
public class Subtitle : ScriptableObject
{

    [SerializeField] string[] subtitles;
    [SerializeField] EventReference[] fmodEvents;
    int index = 0;

    bool isLinePlaying;

    EventInstance currentFmodEvent;
    EVENT_CALLBACK eventCallback;

    /// <summary>
    /// Plays the next subtitle/fmodevent in the 
    /// </summary>
    /// <param name="index">index to play.</param>
    /// <returns> true if there is an element at that index.</returns>
    public bool PlayLine()
    {
        if (index >= subtitles.Length || index >= fmodEvents.Length) return false; // check if index in bounds.
        if (string.IsNullOrEmpty(subtitles[index]))  return false;  // check if subtitle null/empty at index
        
        currentFmodEvent = RuntimeManager.CreateInstance(fmodEvents[index]); // make instance of the event.
        currentFmodEvent.start(); // start playing event.

        SubtitleManager.instance.SetText(subtitles[index]);


        index++; // increment index.
        return true;

    }




    public IEnumerator PlayAll()
    {
        index = 0;
        while (true)
        {
            if (!PlayLine())  break;// break if no next line.
            yield return new WaitUntil(() => {
                currentFmodEvent.getPlaybackState(out PLAYBACK_STATE state);
                return state == PLAYBACK_STATE.STOPPED;
                }
            ); // wait for current line to stop
            yield return null;
            isLinePlaying = true; // reset bool
        }
        SubtitleManager.instance.SetText("");
    }
}
