using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnlockDoorIf : MonoBehaviour
{
    Doors door;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<Doors>();
    }

    public void CheckConditions()
    {
        CarryOvers.SetHudEnabled(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
