using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_FocusObject : MonoBehaviour, IInteractable
{
    [SerializeField] Transform target;

    float baseDuration = 1;
    const float distanceThreshold = 8f;
    const float durationMultiplier = 0.5f;

    public void OnInteract()
    {
        StartCoroutine(LerpCameraToPosition());
    }

    public void OnInteractableHoverEnter()
    {
        ButtonPrompts.instance.SetInteractionPrompt(true);
    }

    public void OnInteractableHoverExit()
    {
        ButtonPrompts.instance.SetInteractionPrompt(false);
    }


    public IEnumerator LerpCameraToPosition()
    {
        SC_FPSController.instance.canMove = false;


        Transform player = SC_FPSController.instance.transform;
        Vector3 startPosition = player.position;
        Vector3 targetPosition = target.position;// - ( Vector3.up * player.localScale.y); //since players actual origin is their feet, subtracts players height to stop you from floating up.
        Quaternion startRotation = player.rotation;
        float duration = baseDuration;
        if (Vector3.Distance(startPosition, targetPosition) <= distanceThreshold) duration *= durationMultiplier;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            // calc lerp factor based on the elapsed time
            float lerpFactor = timeElapsed / duration;

            // lerp position and rotation
            player.position = Vector3.Lerp(startPosition, targetPosition, lerpFactor);
            player.rotation = Quaternion.Lerp(startRotation, target.rotation, lerpFactor);

            // add time
            timeElapsed += Time.deltaTime;

            yield return null; // next frame :)
        }

        // set final position precicely
        player.position = target.position;
        player.rotation = target.rotation;
        OnFocusEnter();


    }

    public void OnFocusEnter()
    {

    }

    public void OnFocusExit()
    {
        SC_FPSController.instance.canMove = true;
    }
}
