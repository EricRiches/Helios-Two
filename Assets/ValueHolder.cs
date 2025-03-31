using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHolder : MonoBehaviour
{
    public Subtitle subtitle;

    public void PlaySubtitles()
    {
        SubtitleManager.instance.SetSubtitle(subtitle);
        SubtitleManager.instance.PlayCurrentSubtitles();
    }
}
