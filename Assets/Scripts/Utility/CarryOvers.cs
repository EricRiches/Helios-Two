using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CarryOvers
{
    // Add more variables for things as necessary
    static int logNum = 0;
    static List<AudioClip> logFiles = new List<AudioClip>();
    static List<GameObject> logObjects = new List<GameObject>();
    static bool isListEmpty = true;

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
    public static void AppendLogObject(GameObject log)
    {
        logObjects.Add(log);
    }

    // Checks if the log player has already played by seeing if it is in the list of played players
    public static bool HasLogObjectPlayed(GameObject log)
    {
        bool isIn = false;

        for (int i = 0; i < logObjects.Count; i++)
        {
            if (logObjects[i] == log)
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
}
