using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTicker : MonoBehaviour
{
    Objectives obj;

    public void Tick()
    {
        obj = GameObject.FindWithTag("Objectives").GetComponent<Objectives>();
        obj.NextObjective();
    }
}
