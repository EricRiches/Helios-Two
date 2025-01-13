using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    public readonly float interactRange = 15f; // The distance the raycast will travel
    public LayerMask interactableLayer;

    [SerializeField] bool isHoveringInteractable;
    IInteractable currentInteractable;
    Renderer currentInteractableRenderer;

    private void Start()
    {
        CheckHoveringInteractable();
        /* this is what i think is happening from googling.
         * When running the game normally, MonoBehaviour will compile frequently used 
         * code AT RUNTIME, not during the build proccess or wtv. This is more efficient,
         * but the instant it begins compiling the code there is a noticable lag spike.
         * this only happens once, the very first time the code is run, so we now 
         * have a ~100 ms lag spike at the time of loading, instead of mid gameplay.
         * which is preferable to looking at your first interactable object and lagging
         */
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHoveringInteractable && currentInteractable != null)
            {
                currentInteractable.OnInteract();
            }
        }
    }
    public void FixedUpdate()
    {
        CheckHoveringInteractable();

    }

    private Ray ray;
    private RaycastHit hit;

    void CheckHoveringInteractable()
    {
        ray.origin = playerCamera.position;
        ray.direction = playerCamera.forward;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            if (!isHoveringInteractable)
            {
                if (hit.collider.gameObject.TryGetComponent(out currentInteractable))
                {
                    currentInteractableRenderer = hit.collider.GetComponent<Renderer>();
                    currentInteractable.OnInteractableHoverEnter();
                    currentInteractable.SetObjectOutline(currentInteractableRenderer, true);
                    isHoveringInteractable = true;
                }
            }
        }
        else if (isHoveringInteractable)
        {
            currentInteractable.OnInteractableHoverExit();
            currentInteractable.SetObjectOutline(currentInteractableRenderer, false);
            isHoveringInteractable = false;
        }
    }

}


