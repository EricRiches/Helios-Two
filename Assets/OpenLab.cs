using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLab : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("lab unlocked- will open on next load");
        CarryOvers.SetLabOpen(true);
    }
}
