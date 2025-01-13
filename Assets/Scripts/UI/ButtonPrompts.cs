using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompts : MonoBehaviour
{
    public static ButtonPrompts instance;

    [SerializeField] GameObject InteractionPrompt;
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
    }

    public void SetExitPrompt(bool value)
    {
        ExitPrompt.SetActive(value);
    }

    public void SetPanelNavPrompt(bool value)
    {
        PanelNavigation.SetActive(value);
    }
    public void SetUseTool(bool value)
    {
        UseTool.SetActive(value);
    }
}
