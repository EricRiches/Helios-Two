using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager instance;
    [SerializeField]TextMeshProUGUI subtitleText;
    [SerializeField]Subtitle currentSubtitleSet;

    public void SetSubtitle(Subtitle newSubtitle, bool startPlaying = false)
    {

        currentSubtitleSet = newSubtitle;

        if (currentSubtitleSet == null) return;
        if (startPlaying)
        {
            StartCoroutine(currentSubtitleSet.PlayAll());
        }
    }

    public void PlayCurrentSubtitles()
    {
        if (subtitleText == null) { subtitleText = GetComponent<TextMeshProUGUI>(); }
        if (currentSubtitleSet == null) { Debug.Log("subtitleset null"); return; }
        StartCoroutine(currentSubtitleSet.PlayAll());
    }

    public void SetText(string newText)
    {
        subtitleText.text = newText;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        subtitleText = GetComponent<TextMeshProUGUI>();
    }

}
