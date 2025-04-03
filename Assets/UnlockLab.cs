using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLab : MonoBehaviour
{
    Doors door;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<Doors>();
        if (door == null) return;
        door.SetCanOpen(CarryOvers.LabOpen);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
