using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door_Animation : MonoBehaviour
{
    [SerializeField] bool CanOpen;
    Animator animator;

    [SerializeField] UnityEvent OnEnter;
    [SerializeField] UnityEvent OnExit;
    [SerializeField] UnityEvent OnLockedEnter;
    [SerializeField] UnityEvent OnLockedExit;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            if (!CanOpen)
            {
                OnLockedEnter.Invoke();
            }
            else
            {
                animator.SetBool("IsOpen", true);
                OnEnter.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (!CanOpen)
            {
                OnLockedExit.Invoke();
            }
            else
            {
                animator.SetBool("IsOpen", false);
                OnExit.Invoke();
            }
        }
    }

    public void SetCanOpen(bool value) { CanOpen = value; }
}
