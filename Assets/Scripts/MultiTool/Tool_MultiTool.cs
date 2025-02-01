using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public abstract class Tool_MultiTool
{
    protected Tool toolType;
    protected bool isUnlocked = false;
    public virtual bool IsUnlocked => isUnlocked;
    /// <summary>
    /// Check if Tool_Interaction is using the correct tool type.
    /// </summary>
    public bool CheckToolType(Interaction_Tool interaction)
    {
        return interaction.toolType == toolType;
    }
    public bool CheckToolType(Tool toolType)
    {
        return this.toolType == toolType;
    }

    public abstract void UseTool();

    public void SetUnlocked(bool value)
    {
        isUnlocked = value;
    }
    public abstract void OnSwapOff();



}

public class Flashlight : Tool_MultiTool
{
    Light light;
    EventReference sound;
    public bool isOn
    {
        get
        {
            if (light == null) { return false; }
            return light.enabled;
        }
    }
    public Flashlight()
    {
        toolType = Tool.Flashlight;

    }
    public void SetLight(Light light)
    {
        this.light = light;
    }
    public override void UseTool()
    {
        light.enabled = !light.enabled;
        if (!sound.IsNull)
        {
            var eventInstance = RuntimeManager.CreateInstance(sound);
            eventInstance.start();

            eventInstance.release();
        }

    }
    public void SetIntensity(float intensity)
    {
        light.intensity = intensity;
    }
    public void SetEventReference(EventReference sound)
    {
        this.sound = sound;
    }

    public override void OnSwapOff() { light.enabled = false; }



}

public class Sonic_Burst : Tool_MultiTool
{
    public Sonic_Burst()
    {
        toolType = Tool.Sonic_Burst;
    }

    public override void UseTool()
    {
        Debug.Log("Sonic Burst was used");
    }
    public override void OnSwapOff() { }
}

public class Freeze_Spray : Tool_MultiTool
{
    public Freeze_Spray()
    {
        toolType = Tool.Freeze_Spray;
    }

    public override void UseTool()
    {
        Debug.Log("Freeze Spray was used");
    }
    public override void OnSwapOff() { }


}

public class Hack_Panel : Tool_MultiTool
{
    // current interactable to activate when tool used.
    IInteractable storedInteractable;

    public override bool IsUnlocked => true;
    // readonly field for the types that cannot be hacked.
    private static readonly HashSet<Type> excludedTypes = new HashSet<Type>
    {
        typeof(Interaction_GeneratorSwitch),
        typeof(Interaction_FocusObject)
    };

    public Hack_Panel()
    {
        toolType = Tool.Hack_Panel;
    }

    public override void UseTool()
    {
        if (storedInteractable == null || !storedInteractable.Interactable) return;
        storedInteractable.OnInteractDown();
        Debug.Log("tool used2");

    }

    public override void OnSwapOff() { }

    public void SetInteractable(IInteractable interactable)
    {
        if (excludedTypes.Contains(interactable.GetType())) return; // if the class the iinteractable is attached to is excluded, return.

        storedInteractable = interactable; // else set interactable.
    }

}


