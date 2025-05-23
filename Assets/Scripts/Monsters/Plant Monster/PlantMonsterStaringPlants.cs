using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMonsterStaringPlants : MonoBehaviour
{
    [SerializeField] Transform PlayerPosition;
    [SerializeField] Transform FlowerTop;

    PlantMonsterManager manager;

    private void Start()
    {
        manager = FindAnyObjectByType<PlantMonsterManager>();
    }

    void Update()
    {
        FlowerTop.LookAt(PlayerPosition);
    }
}