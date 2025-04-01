using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalVoicelineHandler : MonoBehaviour
{
    private static bool lastTerminal;
    public StudioEventEmitter voiceLine1;
    public StudioEventEmitter voiceLIne2;
    public void PlayVoiceLine()
    {
        if (!lastTerminal)
        {
            voiceLine1.Play();
            lastTerminal = false;
        }
        else
        {
            voiceLIne2.Play();
        }
    }
}
