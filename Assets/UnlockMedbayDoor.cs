using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class UnlockMedbayDoor : MonoBehaviour
{
    Doors door;
    // Start is called before the first frame update
    void Start()
    {
        
        door = GetComponent<Doors>();
        if (door == null) { Debug.Log("door not found"); return; }
        Debug.Log(CarryOvers.TramFirstUse);
        door.SetCanOpen(CarryOvers.TramFirstUse);
    }

    
}
