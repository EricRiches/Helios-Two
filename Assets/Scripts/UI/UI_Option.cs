using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_Option : MonoBehaviour
{
    public UnityEvent Activate;

    public void Test(string str)
    {
        Debug.Log(str);
    }
}
