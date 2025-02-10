using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    //[SerializeField] private FMODUnity.EventReference _PAsystem;
    //private FMOD.Studio.EventInstance PAsystem;
    public int footstepType = 0;
    public float footstepInterval = 0.5f;
    private float yet;
    public StudioEventEmitter footstepEmitter;
    public StudioEventEmitter paEmitter;
    public CharacterController characterController;
    
    void Awake()
    {
       // if (!_PAsystem.IsNull)
        //{
        //    PAsystem = FMODUnity.RuntimeManager.CreateInstance(_PAsystem);
       // }
    }

    void Update()
    {
        if (Input.GetKey((KeyCode.LeftShift)))
        {
            footstepInterval = 0.35f;
        }
        else
        {
            footstepInterval = 0.5f;
        }
        
        yet += Time.deltaTime;

        if (yet >= footstepInterval)
        {
            yet -= footstepInterval;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (characterController.isGrounded)
                {
                    footstepEmitter.Play();
                }
                
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PATrigger"))
        {
            paEmitter.Play();
            Destroy(other);
        }
    }

    public void PaPlay()
    {
        paEmitter.Play();
    }

    private void PlayFootSteps()
    {
        
    }

    
}
