using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalVoicelineHandler : MonoBehaviour
{
    private static bool lastTerminal;
    public StudioEventEmitter voiceLine1;
    public StudioEventEmitter voiceLIne2;

    public Subtitle subtitle1;
    public Subtitle subtitle2;

    public void PlaySubtitles()
    {

    }
    public void PlayVoiceLine()
    {
        if (!lastTerminal)
        {
            voiceLine1.Play();
            SubtitleManager.instance.SetSubtitle(subtitle1);
            SubtitleManager.instance.PlayCurrentSubtitles();
            lastTerminal = true;
        }
        else
        {
            SubtitleManager.instance.SetSubtitle(subtitle2);
            SubtitleManager.instance.PlayCurrentSubtitles();
            voiceLIne2.Play();
        }
    }
}
