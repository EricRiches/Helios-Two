using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMonsterBullet : MonoBehaviour
{
    [SerializeField] float BulletSpeed;
    public PlantMonsterManager manager;

    void Update()
    {
        transform.position += BulletSpeed * Time.deltaTime * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            manager.ApplyGoop();
            Destroy(gameObject);
        }
        else if (!other.tag.Contains("Plant"))
        {
            Destroy(gameObject);
        }
    }
}
