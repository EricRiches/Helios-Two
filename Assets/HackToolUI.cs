using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HackToolUI : MonoBehaviour
{
    [SerializeField] UnityEvent events1;
    [SerializeField] UnityEvent events2;


    public void NextStep()
    {
         events1.Invoke();
        StartCoroutine(WaitHackToolEquipped());
    }
    IEnumerator WaitHackToolEquipped()
    {
        yield return new WaitUntil(() => MultiTool.instance.currentTool != null && MultiTool.instance.currentTool.CheckToolType(Tool.Hack_Panel));
        events2.Invoke();
    }
}
