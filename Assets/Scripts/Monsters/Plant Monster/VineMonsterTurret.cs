using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineMonsterTurret : MonoBehaviour
{
    [SerializeField] PlantMonsterBullet BulletPrefab;
    [SerializeField] Transform ShootPoint;
    [SerializeField] Vector2 RandomShootTime;
    float Timer;
    PlantMonsterManager manager;
    SC_FPSController player;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<PlantMonsterManager>();
        player = FindObjectOfType<SC_FPSController>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        
        if (Timer <= 0)
        {
            ShootPoint.LookAt(player.transform.position + (Vector3.up * 10));
            PlantMonsterBullet bulletOBJ = Instantiate(BulletPrefab, ShootPoint.position, ShootPoint.rotation);
            bulletOBJ.manager = manager;
            Timer = Random.Range(RandomShootTime.x, RandomShootTime.y);
        }
    }
}
