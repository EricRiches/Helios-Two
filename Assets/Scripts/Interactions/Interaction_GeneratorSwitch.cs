using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Interaction_GeneratorSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] float holdDuration;
    //[SerializeField] Transform switchHandle;
    //[SerializeField] float switchRotationStart;
    //[SerializeField] float switchRotationEnd;
   // Quaternion startRotation;
   // Quaternion endRotation;
    bool IsOn = false;
    Coroutine holdSwitchCoroutine; 

    [SerializeField] UnityEvent OnSwitchFlippedOn;

    public bool Interactable => !IsOn;

    public void IncrementLockdownTerminalCount()
    {
        CarryOvers.LockdownTerminalsActivated++;
    }
    private void Start()
    {
        //var temp = switchHandle.localRotation.eulerAngles;
        // save quaternion rep of start and end rotations

        //startRotation = Quaternion.Euler(new Vector3(temp.x, temp.y, switchRotationStart));
        //endRotation = Quaternion.Euler(new Vector3(temp.x, temp.y, switchRotationEnd));
    }
    public void OnInteractDown()
    {
        if (IsOn) return;

        if (holdSwitchCoroutine != null) StopCoroutine(holdSwitchCoroutine);
        holdSwitchCoroutine = StartCoroutine(HoldSwitch());
        Throbber.instance.SetVisibility(true);

    }
    public void OnInteractUp()
    {
       // if (!IsOn) { switchHandle.localRotation = startRotation; }
        if (holdSwitchCoroutine != null) StopCoroutine(holdSwitchCoroutine);
        Throbber.instance.SetVisibility(false);
    }

    public void OnInteractableHoverEnter()
    {
        if (IsOn) return;
        ButtonPrompts.instance.SetHoldInteractPrompt(true);
    }

    public void OnInteractableHoverExit()
    {
        if (IsOn) return;
        ButtonPrompts.instance.SetHoldInteractPrompt(false);
    }



    IEnumerator HoldSwitch()
    {
        float elapsedDuration = 0;

 

        while (elapsedDuration <= holdDuration)
        {
            yield return null;
            elapsedDuration += Time.deltaTime;
            float percentComplete = elapsedDuration / holdDuration;

            // Update fill amount
            Throbber.instance.UpdateFillAmount(percentComplete);

            // Interpolate using Quaternion.Lerp to avoid backward rotation
            //switchHandle.localRotation = Quaternion.Lerp(startRotation, endRotation, percentComplete);
        }

        // Final adjustments after the loop completes
        IsOn = true;
        Throbber.instance.SetVisibility(false);
        ButtonPrompts.instance.SetHoldInteractPrompt(false);
        OnSwitchFlippedOn.Invoke();
    }
}
