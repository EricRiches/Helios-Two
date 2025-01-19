using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    //[SerializeField] private FMODUnity.EventReference _Footsteps;
    //private FMOD.Studio.EventInstance Footsteps;
    public int footstepType = 0;
    public float footstepInterval = 0.5f;
    private float yet;
    public StudioEventEmitter emitter;
    public CharacterController characterController;
    
    void Awake()
    {
        //if (!_Footsteps.IsNull)
        //{
        //    Footsteps = FMODUnity.RuntimeManager.CreateInstance(_Footsteps);
       // }
    }

    void Update()
    {
        yet += Time.deltaTime;

        if (yet >= footstepInterval)
        {
            yet -= footstepInterval;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (characterController.isGrounded)
                {
                    emitter.Play();
                }
                
            }
            
        }
    }

    private void PlayFootSteps()
    {
        
    }

    
}
