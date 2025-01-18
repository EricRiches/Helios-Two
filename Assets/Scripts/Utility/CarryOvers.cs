using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CarryOvers
{
    // Add more variables for things as necessary
    public static int logNum = 0;
    public static bool multiTool = false;

    // Add functions to this region as necessary
    #region Toggles and Iterations
    public static void LogTickUp()
    {
        logNum++;
    }

    public static void ToggleMultiTool()
    {
        multiTool = !multiTool;
    }
    #endregion
}
