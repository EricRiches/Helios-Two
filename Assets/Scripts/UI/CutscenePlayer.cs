using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutscenePlayer : MonoBehaviour
{
    [SerializeField] GameObject[] enable;
    [SerializeField] GameObject[] disable;
    [SerializeField] VideoPlayer player;


    public void StartCutscene()
    {
        foreach (var item in enable)
        {
            item.SetActive(true);
        }

        foreach (var item in disable)
        {
            item.SetActive(false);
        }
        player.Play();
        player.loopPointReached += OnCutsceneEnd;
    }

    private void OnCutsceneEnd(VideoPlayer vp)
    {
        vp.Stop();
        SceneTransition.instance.LoadScene("Main Menu");
        
        
    }
}
