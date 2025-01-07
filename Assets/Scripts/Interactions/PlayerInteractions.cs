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

    void CheckHoveringInteractable()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;                                                     // basic raycast from camera, forwards. only interacts with the layer " Interactable "


        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {

            if (!isHoveringInteractable) // don't try to hover any new objects until you have unhovered the previous one.
            {
                if (hit.collider.gameObject.TryGetInterface(out currentInteractable)) // if hit, search gameobjects components for any component which implements IInteractable interface.
                {
                    currentInteractableRenderer = hit.collider.GetComponent<Renderer>(); // store this to avoid re getting it later.
                    currentInteractable.OnInteractableHoverEnter(); // tell the IInteractable class that was found you are hovering over it.
                    currentInteractable.SetObjectOutline(currentInteractableRenderer, true);
                    isHoveringInteractable = true;
                }
            }

        }
        else if (isHoveringInteractable) // if raycast missed, and you were previously hovering something, exit hovering mode.
        {
            currentInteractable.OnInteractableHoverExit();
            currentInteractable.SetObjectOutline(currentInteractableRenderer, false);
            isHoveringInteractable = false;
        }


    }

}
