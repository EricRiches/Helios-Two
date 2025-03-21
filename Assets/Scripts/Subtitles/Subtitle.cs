using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "HeliosTao/Subtitle")]
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
    public bool PlayLine(int index)
    {
        if (index >= subtitles.Length || index >= fmodEvents.Length) return false;// check if index in bounds.
        if (string.IsNullOrEmpty(subtitles[index]) ) return false; // check if subtitle null/empty at index
        currentFmodEvent = RuntimeManager.CreateInstance(fmodEvents[index]); // make instance of the event.
        eventCallback = new EVENT_CALLBACK(EndOfLine); // create callback that will call EndOfLine
        currentFmodEvent.setCallback(eventCallback, EVENT_CALLBACK_TYPE.STOPPED); // set the callback to be called when the event stops.
        currentFmodEvent.start(); // start playing event.

        // UIMANAGER.DISPLAYSUBTITLE(subtitles[index]);


        index++; // increment index.
        return true;

    }


    // Callback function for when the event stops
    private FMOD.RESULT EndOfLine(EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr paramPtr)
    {
        if (type == EVENT_CALLBACK_TYPE.STOPPED) // if fmod event stopped, set playing to true.
        {
            isLinePlaying = true;
        }
        return FMOD.RESULT.OK;
    }

    public IEnumerator PlayAll()
    {
        while (true)
        {
            if (!PlayLine(index)) break; // break if no next line.
            yield return new WaitUntil(() => isLinePlaying); // wait for current line to stop
            isLinePlaying = false; // reset bool
        }

    }
}
