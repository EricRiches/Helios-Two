using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSavePoint : MonoBehaviour
{
    RespawnManager RespawnManager;
    public string PointID;

    private void Start()
    {
        RespawnManager = FindObjectOfType<RespawnManager>();
        RespawnManager.AddRespawnPointToManager(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RespawnManager.SetRespawnPoint(PointID);
        }
    }
}
