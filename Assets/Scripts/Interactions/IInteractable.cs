using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public abstract void OnInteractableHoverEnter();
    public abstract void OnInteractableHoverExit();

    public abstract void OnInteract();

    public void SetObjectOutline(Renderer rend, bool value) //If you want this to actually work, make sure there is a material using the " Outline " Shader.
    {
        
        foreach (var mat in rend.materials)
        {
            if (value) { mat.EnableKeyword("_ENABLE_OUTLINE"); }
            else mat.DisableKeyword("_ENABLE_OUTLINE");
        }
    }
}
