using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction_General : MonoBehaviour, IInteractable
{
    [SerializeField] protected bool interactable;
    public bool Interactable => interactable;

    [SerializeField] protected UnityEvent OnInteractPress;
    [SerializeField] protected UnityEvent OnInteractRelease;
    [SerializeField] protected UnityEvent OnHoverEnter;
    [SerializeField] protected UnityEvent OnHoverExit;
    public virtual void OnInteractableHoverEnter()
    {
        ButtonPrompts.instance.SetInteractionPrompt(true);
        OnHoverEnter.Invoke();
    }

    public virtual void OnInteractableHoverExit()
    {
        ButtonPrompts.instance.SetInteractionPrompt(false);
        OnHoverExit.Invoke();
    }

    public virtual void OnInteractDown()
    {
        OnInteractPress.Invoke();
    }

    public virtual void OnInteractUp()
    {
        OnInteractRelease.Invoke();
    }

    public virtual void SetInteractable(bool value) { interactable = value; }


}
