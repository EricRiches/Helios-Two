using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "HeliosTao/Subtitle")]
[System.Serializable]
public class Subtitle : ScriptableObject
{

    [SerializeField] SubtitleTimestamp[] subtitles;
    //[SerializeField] EventReference fmodEvents;
    int index = 0;

    EventInstance currentFmodEvent;

    /// <summary>
    /// Plays the next subtitle/fmodevent in the 
    /// </summary>
    /// <param name="index">index to play.</param>
    /// <returns> true if there is an element at that index.</returns>
    public bool PlayLine()
    {
        if (index >= subtitles.Length) return false; // check if index in bounds.
        if (string.IsNullOrEmpty(subtitles[index].subtitleLine))  return false;  // check if subtitle null/empty at index
        
    

        SubtitleManager.instance.SetText(subtitles[index].subtitleLine);


        index++; // increment index.
        return true;

    }




    public IEnumerator PlayAll()
    {


        /*new WaitUntil(() => {
            currentFmodEvent.getPlaybackState(out PLAYBACK_STATE state);
            return state == PLAYBACK_STATE.STOPPED;
        }
            ); // wait for current line to stop*/


        index = 0;
        //currentFmodEvent = RuntimeManager.CreateInstance(fmodEvents); // make instance of the event.
        //currentFmodEvent.start(); // start playing event.
        while (true)
        {
            if (!PlayLine())
            {
                yield return new WaitForSecondsRealtime(subtitles[subtitles.Length-1].duration);
                break;// break if no next line.
            }
              
            yield return new WaitForSecondsRealtime(subtitles[index-1].duration);
            yield return null;
        }
        SubtitleManager.instance.SetText("");
    }
}


[System.Serializable]
public class SubtitleTimestamp
{
    public float duration;
    public string subtitleLine;

}