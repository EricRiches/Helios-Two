using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMonsterManager : MonoBehaviour
{
    public LayerMask PlayerMask;
    [SerializeField] PlantMonsterStaringPlants[] killPoints;
    [SerializeField] float killPointRange;

    SC_FPSController playerLocation;

    private void Start()
    {
        playerLocation = FindObjectOfType<SC_FPSController>();
    }

    void Update()
    {
        if (killPoints.Length > 0)
        {
            foreach (PlantMonsterStaringPlants point in killPoints)
            {
                if (Physics.CheckSphere(point.transform.position, killPointRange, PlayerMask))
                {
                    HitPlayer(point.gameObject.name);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (killPoints.Length > 0)
        {
            foreach (PlantMonsterStaringPlants point in killPoints)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(point.transform.position, killPointRange);
            }
        }
    }

    public void HitPlayer(string MurdererName)
    {
        Debug.Log("Player Died to " + MurdererName);
        playerLocation.
    }
}
