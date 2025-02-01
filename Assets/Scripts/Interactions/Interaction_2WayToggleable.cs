using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction_2WayToggleable : Interaction_General
{

    bool toggled = false;
    [SerializeField] UnityEvent OnInteractPress_StateB;

    public override void OnInteractableHoverEnter()
    {
        ButtonPrompts.instance.SetInteractionPrompt(true);
        OnHoverEnter.Invoke();
    }

    public override void OnInteractableHoverExit()
    {
        ButtonPrompts.instance.SetInteractionPrompt(false);
        OnHoverExit.Invoke();
    }

    public override void OnInteractDown()
    {
        if (!toggled) OnInteractPress.Invoke();
        else { OnInteractPress_StateB.Invoke(); }
    }

    public override void OnInteractUp()
    {
        OnInteractRelease.Invoke();
    }

    public override void SetInteractable(bool value) { interactable = value; }



}
