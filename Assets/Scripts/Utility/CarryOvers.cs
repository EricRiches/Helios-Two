using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CarryOvers
{
    // Add more variables for things as necessary
    static int logNum = 0;
    static List<AudioClip> logFiles = new List<AudioClip>();
    static List<string> logObjects = new List<string>();
    static bool isListEmpty = true;

    static byte totalLockdownTerminals = 2;
    static byte lockdownTerminalsActivated = 0;
    static bool reactorDoorOpen = false;
    static bool tramFirstUse = false;
    static bool hudEnabled = false;
    static bool labOpen;

    public static List<string> paObj = new List<string>();

    #region Objective Variables
    static bool openedShutters = false;
    static bool killedBlood = false;
    #endregion Objective Variables

    public static bool ReactorDoorOpen => reactorDoorOpen;
    public static bool TramFirstUse => tramFirstUse;
    public static bool HudEnabled => hudEnabled;
    public static bool LabOpen => labOpen;
    public static byte LockdownTerminalsActivated
    {
        get { return lockdownTerminalsActivated; }
        set { 
            lockdownTerminalsActivated = value;
            if (lockdownTerminalsActivated >= totalLockdownTerminals)
            {
                reactorDoorOpen = true;
            }
        }
    }

    static bool multiTool = false;
    static bool flashlight = false;

    // Add functions to this region as necessary
    #region Audio Logs
    // Adds 1 to the log number. This is for the next log player to know which is the one it'll play
    public static void LogTickUp()
    {
        logNum++;
    }

    // This function adds the log player to the list of objects that have played a log so it wont play another one
    public static void AppendLogObject(string log)
    {
        logObjects.Add(log);
    }

    // Checks if the log player has already played by seeing if it is in the list of played players
    public static bool HasLogObjectPlayed(string logName)
    {
        bool isIn = false;

        for (int i = 0; i < logObjects.Count; i++)
        {
            if (logObjects[i] == logName)
            {
                isIn = true;
                i = logObjects.Count;
            }
        }

        return isIn;
    }

    // When sent the list of all logs, this function adds them all to a list. Use if the log players dont already have a list of logs
    public static void SetLogList(List<AudioClip> audioClips)
    {
        if (isListEmpty)
        {
            for (int i = 0; i < audioClips.Count; i++)
            {
                logFiles.Add(audioClips[i]);
            }

            isListEmpty = false;
        }
    }

    // Return the log to play
    public static int GetIndex()
    {
        return logNum;
    }
    #endregion Audio Logs

    #region Multi Tools
    // Sets the boolean for if you have the multitool to the opposite
    public static void ToggleMultiTool()
    {
        multiTool = !multiTool;
    }

    // For when you need to check if the player has access to the multitool
    public static bool GetMultiTool()
    {
        return multiTool;
    }

    // Sets the boolean for if you have the flashlight to the opposite
    public static void ToggleFlashlight()
    {
        flashlight = !flashlight;
    }

    // For when you need to check if the player has access to the flashlight
    public static bool GetFlashlight()
    {
        return flashlight;
    }
    #endregion

    #region Objective Checks
    public static bool GetOpenedShutters()
    {
        return openedShutters;
    }

    public static void SetOpenedShutters()
    {
        openedShutters = true;
    }

    public static bool GetKilledBlood()
    {
        return killedBlood;
    }

    public static void SetKilledBlood()
    {
        killedBlood = true;
    }
    #endregion Objective Checks


    public static void OnTramFirstUse()
    {
        tramFirstUse = true;
    }

    public static void SetHudEnabled(bool value)
    {
        hudEnabled = value;
        PlayerInteractions.instance.HUD.SetActive(value);
    }

    public static void SetLabOpen(bool value)
    {
        labOpen = value;
    }

    public static void AppendObj(string obj)
    {
        paObj.Add(obj);
    }

    public static List<string> GetPAObj()
    {
        return paObj;
    }
}