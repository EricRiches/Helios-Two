using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PrimaryMonster))]
public class JackInTheBoxMonster : MonoBehaviour
{
    PrimaryMonster m_PrimaryMonster;
    bool isCurrentlySeen;

    JackVisionChecker checkers;

    private void Start()
    {
        m_PrimaryMonster = GetComponent<PrimaryMonster>();
    }

    void Update()
    {
        if (isCurrentlySeen)
        {
            m_PrimaryMonster.speedMultiplier = 0;
        }
        else
        {
            m_PrimaryMonster.speedMultiplier = 1;
        }

        isCurrentlySeen = false;
    }

    public void setIsCurrentlySeen(Vector3 VisionPosition)
    {
        isCurrentlySeen = true;
        m_PrimaryMonster.SetPlayerPosition = VisionPosition;
    }

    public void RemoveFromPlay()
    {
        checkers.JackInTheBoxes.Remove(this);
        Destroy(gameObject);
    }
}
