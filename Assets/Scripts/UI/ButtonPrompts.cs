using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompts : MonoBehaviour
{
    public static ButtonPrompts instance;

    public GameObject InteractionPrompt;
    public GameObject ExitPrompt;
    public GameObject Prompt3;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    public void SetInteractionPrompt(bool value)
    {
        InteractionPrompt.SetActive(value);
    }
}
