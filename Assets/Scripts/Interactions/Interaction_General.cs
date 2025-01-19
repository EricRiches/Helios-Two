using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction_General : MonoBehaviour, IInteractable
{
    [SerializeField] bool interactable;
    public bool Interactable => interactable;

    [SerializeField] UnityEvent OnInteractPress;
    [SerializeField] UnityEvent OnInteractRelease;
    [SerializeField] UnityEvent OnHoverEnter;
    [SerializeField] UnityEvent OnHoverExit;
    public void OnInteractableHoverEnter()
    {
        ButtonPrompts.instance.SetInteractionPrompt(true);
        OnHoverEnter.Invoke();
    }

    public void OnInteractableHoverExit()
    {
        ButtonPrompts.instance.SetInteractionPrompt(false);
        OnHoverExit.Invoke();
    }

    public void OnInteractDown()
    {
        OnInteractPress.Invoke();
    }

    public void OnInteractUp()
    {
        OnInteractRelease.Invoke();
    }

    public void SetInteractable(bool value) { interactable = value; }


}
