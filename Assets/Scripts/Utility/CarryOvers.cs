using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CarryOvers
{
    // Add more variables for things as necessary
    static int logNum = 0;
    static List<AudioClip> logFiles = new List<AudioClip>();
    static List<GameObject> logObjects = new List<GameObject>();

    static bool multiTool = false;
    static bool flashlight = false;

    // Add functions to this region as necessary
    #region Audio Logs
    public static void LogTickUp()
    {
        logNum++;
    }

    public static void AppendLogObject(GameObject log)
    {
        logObjects.Add(log);
    }

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

    public static void SetLogList(List<AudioClip> audioClips)
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            logFiles.Add(audioClips[i]);
        }
    }

    public static AudioClip GetLog(int index)
    {
        return logFiles[index];
    }
    #endregion Audio Logs

    #region Multi Tools
    public static void ToggleMultiTool()
    {
        multiTool = !multiTool;
    }

    public static bool GetMultiTool()
    {
        return multiTool;
    }

    public static void ToggleFlashlight()
    {
        flashlight = !flashlight;
    }

    public static bool GetFlashlight()
    {
        return flashlight;
    }
    #endregion
}
