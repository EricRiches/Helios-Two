using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Interaction_Tool : MonoBehaviour, IInteractable
{
    public Tool toolType;

    public void OnInteract()
    {
        if (MultiTool.instance.currentTool.CheckToolType(this))
        {
            Debug.Log("CorrectTool");
        }
        else Debug.Log("IncorrectTool");
    }

    public void OnInteractableHoverEnter()
    {
        ButtonPrompts.instance.SetInteractionPrompt(true);
    }

    public void OnInteractableHoverExit()
    {
        ButtonPrompts.instance.SetInteractionPrompt(false);
    }

}
