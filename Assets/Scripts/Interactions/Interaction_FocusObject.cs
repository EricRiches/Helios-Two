using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interaction_FocusObject : MonoBehaviour, IInteractable
{
    [SerializeField] Transform target;
    [SerializeField] UnityEvent OnFocus;
    [SerializeField] UnityEvent OnUnFocus;

    float baseDuration = 1;
    const float distanceThreshold = 8f;
    const float durationMultiplier = 0.5f;
    Coroutine LerpCoroutine;
    public bool forceExit;
    public bool interactable = false;

    public bool Interactable => interactable;

    //public bool ExitFocus => Input.GetKeyDown(KeyCode.Escape) || forceExit;

    private void Start()
    {
        OnFocus.AddListener(ListenForExit);
    }

    public void OnInteractDown()
    {
        if (!Interactable) return;
        if(LerpCoroutine == null)  LerpCoroutine = StartCoroutine(LerpCameraToPosition());
        ButtonPrompts.instance.SetInteractionPrompt(false);
    }

    public void OnInteractableHoverEnter()
    {
        if (!Interactable) return;
        ButtonPrompts.instance.SetInteractionPrompt(true);
    }

    public void OnInteractableHoverExit()
    {
        if (!Interactable) return;
        ButtonPrompts.instance.SetInteractionPrompt(false);
    }

    public void TramFirstUse()
    {
        CarryOvers.OnTramFirstUse();
    }
    public void OnInteractUp() { }

    public void SetInteractable(bool value) { interactable = value; }
    public IEnumerator LerpCameraToPosition()
    {


        OnFocus.Invoke();
        Transform player = SC_FPSController.instance.transform;
        Transform camera = SC_FPSController.instance.playerCamera.transform;
        Vector3 startPosition = player.position;
        Vector3 targetPosition = target.position;
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


    }

    void ListenForExit()
    {
       
        StartCoroutine(WaitForExit());

        IEnumerator WaitForExit()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape) || forceExit);
            if(LerpCoroutine != null) StopCoroutine(LerpCoroutine); // if you try to exit while in the lerp process, cancel it.
            LerpCoroutine = null;
            OnUnFocus.Invoke();
            forceExit = false;
        }
    }


    public void TakeTramToLocation(string sceneName)
    {
        SceneTransition.instance.LoadSceneToTram(sceneName);
    }


    private void OnDestroy() 
    {
        OnFocus.RemoveListener(ListenForExit);
    }

}
