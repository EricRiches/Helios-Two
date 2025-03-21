using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    [SerializeField] Transform player;
    [SerializeField] bool atTop = false;
    Coroutine elevatorCoroutine;
    [SerializeField] UnityEvent EitherPositionReached;
    [SerializeField] UnityEvent EndPositionReached;
    [SerializeField] UnityEvent StartPositionReached;
    private void Start()
    {
        startPos = transform.position;
    }
    public void StartElevator()
    {
        if (elevatorCoroutine != null) return; // if coroutine is already running, don't start another one.
        elevatorCoroutine = StartCoroutine(MoveElevator()); // else, start the coroutine.
    }

    /// <summary>
    /// flips the value of atTop. made as a method so you can call it in unity fmodEvents.
    /// </summary>
    public void FlipAtTop()
    {
        atTop = !atTop;
    }

    IEnumerator MoveElevator()
    {
        var charController = player.GetComponent<CharacterController>();
        charController.enabled = false; // long story short, the character controller component does SOMETHING to the players position,
                                        // where if i set the position WITHOUT disabling it first, it instantly snaps the player back to their previous position. truely 0 idea why, but wtv.
        player.SetParent(transform); // parent player to elevator so they teleport with it.
        transform.position = atTop ? startPos : endPos; // tp elevator (and player). if you're at the top, return to start position.
        player.SetParent(null); // unparents the player from elevator.
        charController.enabled = true;
        yield return new WaitForSeconds(7.5f); // wait to give illusion of travel
       
        var eventToInvoke = atTop ? StartPositionReached : EndPositionReached; // if already at top, invoke the event for reaching the start position, since you are now going back there.
        eventToInvoke.Invoke(); // invoke event specific to being at the top or bottom of the elevator path.
        EitherPositionReached.Invoke(); // event for things that will happen regardless of top or bottom of elevator.
        elevatorCoroutine = null; // nullify coroutine variable so startelevator() doesnt return.

        /*   
         *   causes very buggy movement and hacky fixes also had bugs related. instead, just fake the elevator.
        startPos = transform.position;
        float elapsedDuration = 0;
        while (elapsedDuration < duration)
        {
            yield return null;
            elapsedDuration += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, elapsedDuration / duration);
        }
        player.SetParent(null);
        OnTargetReached.Invoke();
        
         */

    }
}
