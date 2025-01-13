using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool_MultiTool
{
    protected Tool toolType;
    public abstract bool isUnlocked
    {
        get;
    }
    /// <summary>
    /// Check if Tool_Interaction is using the correct tool type.
    /// </summary>
    public bool CheckToolType(Interaction_Tool interaction)
    {
    
        return interaction.toolType == toolType;
    }

    public abstract void UseTool();




}

public class Flashlight : Tool_MultiTool
{
    public Flashlight()
    {
        toolType = Tool.Flashlight;
    }

    public override void UseTool()
    {
        Debug.Log("Flashlight was used");
    }

    public override bool isUnlocked
    {
        get=> true;
    }

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
    public override bool isUnlocked
    {
        get => true;
    }

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
    public override bool isUnlocked
    {
        get => true;
    }

}

public class Hack_Panel : Tool_MultiTool
{
    public Hack_Panel()
    {
        toolType = Tool.Hack_Panel;
    }

    public override void UseTool()
    {
        Debug.Log("Hacker Panel was used");
    }
    public override bool isUnlocked
    {
        get =>true;
    }

}


