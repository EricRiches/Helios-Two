using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockIfLockdownTerminal : MonoBehaviour
{
    [SerializeField]Doors door;
    // Start is called before the first frame update
    void Start()
    {
        if (CarryOvers.ReactorDoorOpen)
        {
            door.SetCanOpen(true);
        }
    }
}
