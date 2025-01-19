using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 targetPos;
    [SerializeField] float duration;
    [SerializeField] Transform player;
    Coroutine elevatorCoroutine;

    [SerializeField] UnityEvent OnTargetReached;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartElevator()
    {
        if (elevatorCoroutine != null) return;
        elevatorCoroutine = StartCoroutine(MoveElevator());
    }

    IEnumerator MoveElevator()
    {
        player.SetParent(transform);
        startPos = transform.position;
        float elapsedDuration = 0;
        while (elapsedDuration < duration)
        {
            yield return null;
            elapsedDuration += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedDuration / duration);
        }
        player.SetParent(null);
        OnTargetReached.Invoke();

    }
}
