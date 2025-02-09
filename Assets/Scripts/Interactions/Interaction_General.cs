using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction_General : MonoBehaviour, IInteractable
{
    [SerializeField] protected bool interactable = true;
    public bool Interactable => interactable;

    [SerializeField] protected UnityEvent OnInteractPress;
    [SerializeField] protected UnityEvent OnInteractRelease;
    [SerializeField] protected UnityEvent OnHoverEnter;
    [SerializeField] protected UnityEvent OnHoverExit;
    public virtual void OnInteractableHoverEnter()
    {
        if (!interactable) return;
        ButtonPrompts.instance.SetInteractionPrompt(true);
        OnHoverEnter.Invoke();
    }

    public virtual void OnInteractableHoverExit()
    {
        if (!interactable) return;
        ButtonPrompts.instance.SetInteractionPrompt(false);
        OnHoverExit.Invoke();
    }

    public virtual void OnInteractDown()
    {
        if (!interactable) return;
        OnInteractPress.Invoke();
    }

    public virtual void OnInteractUp()
    {
        if (!interactable) return;
        OnInteractRelease.Invoke();
    }

    public virtual void SetInteractable(bool value) { interactable = value; }


}
