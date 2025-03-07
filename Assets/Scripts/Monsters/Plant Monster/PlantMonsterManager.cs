using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMonsterManager : MonoBehaviour
{
    public bool isPlantDead = false;
    public LayerMask PlayerMask;
    //[SerializeField] PlantMonsterStaringPlants[] killPoints;
    //[SerializeField] float killPointRange;

    public Vector2 VineSwingTimes;
    [Header("Visual Goop")]
    [SerializeField] GameObject VisualGoop;
    [SerializeField] float GoopTime;
    float GoopTimer;

    SC_FPSController playerLocation;

    private void Start()
    {
        playerLocation = FindObjectOfType<SC_FPSController>();
        VisualGoop.SetActive(false);
    }

    void Update()
    {
        /*if (killPoints.Length > 0)
        {
            foreach (PlantMonsterStaringPlants point in killPoints)
            {
                if (Physics.CheckSphere(point.transform.position, killPointRange, PlayerMask))
                {
                    HitPlayer(point.gameObject.name);
                }
            }
        }*/

        if (GoopTimer > 0)
        {
            GoopTimer -= Time.deltaTime;

            if (GoopTimer <= 0)
            {
                VisualGoop.SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        /*if (killPoints.Length > 0)
        {
            foreach (PlantMonsterStaringPlants point in killPoints)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(point.transform.position, killPointRange);
            }
        }*/
    }

    /*public void HitPlayer(string MurdererName)
    {
        Debug.Log("Player Died to " + MurdererName);
    }*/

    public void PlantDied()
    {
        isPlantDead = true;
    }

    public void ApplyGoop()
    {
        VisualGoop.SetActive(true);
        GoopTimer = GoopTime;
    }
}
