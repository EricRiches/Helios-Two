using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLogPlay : MonoBehaviour
{
    public Subtitle subtitle;

    public void PlayAuidoLog()
    {
        SubtitleManager.instance.SetSubtitle(subtitle);
        SubtitleManager.instance.PlayCurrentSubtitles();
    }
}
