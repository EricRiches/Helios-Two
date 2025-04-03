using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public static PlayerInteractions instance;
    public GameObject HUD;
    [SerializeField] Transform playerCamera;
    [SerializeField] float interactRange = 15f; // The distance the raycast will travel
    public LayerMask interactableLayer;

    [SerializeField] bool isHoveringInteractable;
    IInteractable currentInteractable;
    Renderer currentInteractableRenderer;

    private void Awake()
    {
        if(instance!= null) Destroy(instance);
        instance = this;
    }

    private void Start()
    {
       // CheckHoveringInteractable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHoveringInteractable && currentInteractable != null)
            {
                currentInteractable.OnInteractDown();

                if (MultiTool.instance.currentTool != null && MultiTool.instance.currentTool.CheckToolType(Tool.Hack_Panel))
                {
                    MultiTool.hack_Panel.SetInteractable(currentInteractable);
                }
            }
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            if(currentInteractable!= null)
            {
                currentInteractable.OnInteractUp();
            }
        }
    }
    public void FixedUpdate()
    {
        CheckHoveringInteractable();

    }

    public IInteractable GetCurrentInteractable()
    {
        return currentInteractable;
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
                    if (currentInteractable.Interactable)
                    {
                        currentInteractableRenderer = hit.collider.GetComponent<Renderer>();
                        currentInteractable.OnInteractableHoverEnter();
                        if (currentInteractableRenderer != null) currentInteractable.SetObjectOutline(currentInteractableRenderer, true);
                        isHoveringInteractable = true;
                    }
                }
            }
        }
        else if (isHoveringInteractable)
        {
            
            currentInteractable.OnInteractableHoverExit();
            if (currentInteractableRenderer != null) currentInteractable.SetObjectOutline(currentInteractableRenderer, false);
            isHoveringInteractable = false;
            currentInteractable = null;
        }
    }

}

