using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public abstract void OnInteractableHoverEnter();
    public abstract void OnInteractableHoverExit();
    public abstract void OnInteractDown();

    public abstract void OnInteractUp();
    public abstract bool Interactable { get; }

    public void SetObjectOutline(Renderer rend, bool value) //If you want this to actually work, make sure there is a material using the " Outline " Shader.
    {
        if (!Interactable) return;
        foreach (var mat in rend.materials)
        {
            mat.SetFloat("_Enabled", Convert.ToInt32(value));
        }
    }
}
