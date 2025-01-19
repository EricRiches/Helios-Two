using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private Animator animator;
    public FMODUnity.StudioEventEmitter emitter;
    [SerializeField] bool CanOpen = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CanOpen) return;
        if (other.CompareTag("Monster") || other.CompareTag("Player"))
        {
            animator.SetTrigger("OPEN");
            //emitter.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CanOpen) return;
        if (other.CompareTag("Monster") || other.CompareTag("Player"))
        {
            animator.SetTrigger("CLOSE");
            //emitter.Play();
        }
    }

    public void ForceClose()
    {
        animator.SetTrigger("CLOSE");
    }

    public void SetCanOpen(bool value) { CanOpen = value; }
}
