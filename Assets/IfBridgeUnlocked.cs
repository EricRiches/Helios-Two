using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IfBridgeUnlocked : MonoBehaviour
{

    [SerializeField] UnityEvent events;
    private void Start()
    {
        if(CarryOvers.BridgeEntered) events.Invoke();
    }
}
