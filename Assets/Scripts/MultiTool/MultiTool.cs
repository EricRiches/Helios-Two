using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTool : MonoBehaviour
{
    public static MultiTool instance;

    public static Flashlight flashlight = new();
    public static Sonic_Burst sonic_Burst= new();
    public static Freeze_Spray freeze_Spray = new();
    public static Hack_Panel hack_Panel = new();


    public Tool_MultiTool currentTool;

    public bool canUseTool = true;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentTool = flashlight;
    }

    public void SwitchTool(Tool tool) => tool switch
    {
        Tool.Flashlight => () => { if (flashlight.IsUnlocked) { currentTool = flashlight; } }
        ,
        Tool.Sonic_Burst => () => { if (flashlight.IsUnlocked) { currentTool = flashlight; } }
        ,
        Tool.Freeze_Spray => () => { if (flashlight.IsUnlocked) { currentTool = flashlight; } }
        ,
        Tool.Hack_Panel => () => { if (flashlight.IsUnlocked) { currentTool = flashlight; } }
        ,
        _ => 0,


    };


    public void SetCanUseTool(bool value) { canUseTool = value; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canUseTool) return;
            currentTool.UseTool();
        }
    }
}

public enum Tool
{
    Flashlight,
    Sonic_Burst,
    Freeze_Spray,
    Hack_Panel,
}