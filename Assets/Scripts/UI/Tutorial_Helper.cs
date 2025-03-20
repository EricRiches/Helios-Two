using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial_Helper : MonoBehaviour
{
    [SerializeField] UnityEvent FlashlightEquipped;
    [SerializeField] UnityEvent ToolUsed;

    public void StartWaitUntilFlashlight()
    {
        StartCoroutine(WaitUntilFlashlightEquipped());
    }
    public void StartWaitFlashlightUsed()
    {
        StartCoroutine(WaitFlashlightUsed());
    }

    IEnumerator WaitUntilFlashlightEquipped()
    {
        yield return new WaitUntil(
           () => MultiTool.instance.currentTool != null &&
              MultiTool.instance.currentTool.CheckToolType(Tool.Flashlight)
        );
        FlashlightEquipped.Invoke();
    }

    IEnumerator WaitFlashlightUsed()
    {
        yield return new WaitUntil(
            () => MultiTool.flashlight.isOn
        );
        ToolUsed.Invoke();
    }
}
