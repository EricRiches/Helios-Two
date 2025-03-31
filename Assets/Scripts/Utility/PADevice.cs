using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PADevice : MonoBehaviour
{
    void Awake()
    {
        if (CheckIfPlayed())
        {
            gameObject.SetActive(false);
        }
    }

    bool CheckIfPlayed()
    {
        return CarryOvers.HasLogObjectPlayed(gameObject.name);
    }

    public void AppendObject()
    {
        CarryOvers.AppendLogObject(gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!CheckIfPlayed())
            {
                AppendObject();
            }
        }
    }
}
