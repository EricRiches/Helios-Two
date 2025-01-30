using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMonsterStaringPlants : MonoBehaviour
{
    [SerializeField] Transform PlayerPosition;
    [SerializeField] Transform FlowerTop;

    void Update()
    {
        FlowerTop.LookAt(PlayerPosition);
    }
}