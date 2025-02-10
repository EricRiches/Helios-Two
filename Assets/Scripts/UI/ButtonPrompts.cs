using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompts : MonoBehaviour
{
    public static ButtonPrompts instance;

    [SerializeField] GameObject InteractionPrompt;
    [SerializeField] GameObject HoldInteractPrompt;
    [SerializeField] GameObject HackToolPrompt;
    [SerializeField] GameObject ExitPrompt;
    [SerializeField] GameObject PanelNavigation;
    [SerializeField] GameObject UseTool;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    public void SetInteractionPrompt(bool value)
    {
        InteractionPrompt.SetActive(value);
        if (value  && MultiTool.instance.currentTool != null&& MultiTool.instance.currentTool.CheckToolType(Tool.Hack_Panel)) // only set active if hack panel is equipped.
        {
            HackToolPrompt.SetActive(value);
        }
        else if (!value) // in the event you put the hack panel away, always set to false.
        {
            HackToolPrompt.SetActive(false);
        }
    }

    public void SetExitPrompt(bool value)
    {
        ExitPrompt.SetActive(value);
    }
    public void SetHoldInteractPrompt(bool value) { HoldInteractPrompt.SetActive(value); }
    public void SetPanelNavPrompt(bool value)
    {
        PanelNavigation.SetActive(value);
    }
    public void SetUseTool(bool value)
    {
        UseTool.SetActive(value);
    }
}
