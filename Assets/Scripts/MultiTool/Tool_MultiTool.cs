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
    Coroutine sprayCoroutine;
    FreezeSprayCollider fsc;
    public Freeze_Spray()
    {
        toolType = Tool.Freeze_Spray;
    }
    public override bool IsUnlocked => true;
    public override void UseTool()
    {
        if (fsc == null) // if fsc is null ( was not set previously, or was deleted somehow?)
        {
            fsc = GameObject.FindFirstObjectByType<FreezeSprayCollider>(); // try to find it in scene.
            if(fsc == null) // if it wasn't found, error out.
            {
                Debug.LogError("FreezeSprayCollider Not found in scene. double check there is an instance of it.");
                return;
            }
        }
        if(sprayCoroutine==null) sprayCoroutine = fsc.StartCoroutine(SprayCoroutine());// make sure only 1 freeze spray happens at a time.
    }

    IEnumerator SprayCoroutine()
    {
        Debug.Log("spray" + fsc == null);
        fsc.Toggle(); // toggle collider on, wait, toggle off.
        yield return new WaitForSeconds(2);
        fsc.Toggle();
        sprayCoroutine = null; // set coroutine reference back to null.
    }
    public override void OnSwapOff() { }

    /// <summary>
    /// set FreezeSprayCollider field of freeze spray ( in case it is not assigned over scene transitions).
    /// </summary>
    public void SetFSC(FreezeSprayCollider newFSC)
    {
        if(newFSC != null) fsc = newFSC;
    }
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
        IInteractable interactable = PlayerInteractions.instance.GetCurrentInteractable();
        if (storedInteractable == null && interactable != null)
        {
            storedInteractable = interactable;
        }
        else if (storedInteractable != null && storedInteractable.Interactable)
        {
            storedInteractable.OnInteractDown();
            storedInteractable = null;
        }
    }

    public override void OnSwapOff() { }

    public void SetInteractable(IInteractable interactable)
    {
        if (excludedTypes.Contains(interactable.GetType())) return; // if the class the iinteractable is attached to is excluded, return.

        storedInteractable = interactable; // else set interactable.
    }
}