using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PrototypeNavigator : MonoBehaviour
{
    NavMeshAgent controller;
    [SerializeField] MonsterNavigationGrid grid;

    void Start()
    {
        controller = FindObjectOfType<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 roamPosition;

            if (grid.FindRoamPosition(out roamPosition))
            {
                controller.destination = roamPosition;
            }
        }
    }
}
