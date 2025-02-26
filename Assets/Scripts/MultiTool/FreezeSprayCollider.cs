using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeSprayCollider : MonoBehaviour
{
    Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    public void Toggle()
    {
        collider.enabled = !collider.enabled;
    }
    private void OnTriggerStay(Collider other)
    {

        if (!other.CompareTag("Monster")) return;
        if (other.TryGetComponent(out MonsterBehavior monster))
        {
            if (monster.canMove) StartCoroutine(FreezeCoroutine(monster));

        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }

    IEnumerator FreezeCoroutine(MonsterBehavior monster)
    {
        if( monster.TryGetComponent(out NavMeshAgent agent)){ // get navmesh agent and stop it.
            agent.isStopped = true;
        }
        monster.canMove = false;
        yield return new WaitForSeconds(8);
        monster.canMove = true;
        if (agent != null)
        { // unstop navmesh agent if there was one.
            agent.isStopped = false;
        }
    }
}
