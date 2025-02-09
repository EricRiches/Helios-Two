using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private Animator animator;
    public FMODUnity.StudioEventEmitter emitter;
    [SerializeField] bool CanOpen = true;
    [SerializeField] GameObject[] redPanelLights;
    [SerializeField] GameObject[] greenPanelLights;
    private void Start()
    {
        animator = GetComponent<Animator>();
        SetPanelColour(CanOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CanOpen) return;
        if (other.CompareTag("Monster") || other.CompareTag("Player"))
        {
            animator.SetTrigger("OPEN");
            emitter.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CanOpen) return;
        if (other.CompareTag("Monster") || other.CompareTag("Player"))
        {
            animator.SetTrigger("CLOSE");
            emitter.Play();
        }
    }


    public void SetPanelColour(bool value)
    {
        if (value)
        {
            foreach (var light in greenPanelLights) { light.SetActive(true); }
            foreach (var light in redPanelLights) { light.SetActive(false); }
        }
        else
        {
            foreach (var light in redPanelLights) { light.SetActive(true); }
            foreach (var light in greenPanelLights) { light.SetActive(false); }
        }
    }
    public void ForceClose()
    {
        animator.SetTrigger("CLOSE");
    }

    public void SetCanOpen(bool value) {
        CanOpen = value;
        SetPanelColour(value);
    }
}
