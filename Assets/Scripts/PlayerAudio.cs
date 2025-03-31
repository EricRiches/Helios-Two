using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    //public FMODUnity.EventReference _PAsystem;
    //public FMOD.Studio.EventInstance PAsystem;
    public int footstepType = 0;
    public float footstepInterval = 0.5f;
    private float yet;
    private int PaNum = 0;
    private int PaInstance = 0;
    public StudioEventEmitter footstepEmitter;
    public StudioEventEmitter paEmitter;
    public CharacterController characterController;
    public List<Subtitle> subtitles;
    
    void Awake()
    {
      // if (!_PAsystem.IsNull)
      // {
         //  PAsystem = FMODUnity.RuntimeManager.CreateInstance(_PAsystem);
       //}
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
            other.gameObject.GetComponent<StudioEventEmitter>().Play();
            //PaInstance = other.gameObject.GetComponent<ValueHolder>().value;
            SubtitleManager.instance.SetSubtitle(other.gameObject.GetComponent<ValueHolder>().subtitle);
            SubtitleManager.instance.PlayCurrentSubtitles();
            other.gameObject.SetActive(false);
            
            CarryOvers.AppendObj(other.gameObject.name);
        }
    }

    public void PaPlay()
    {
        if (PaNum >= subtitles.Count) { Debug.LogError("Indexing out of List bounds"); }
        else
        {
            
            //SubtitleManager.instance.SetSubtitle(subtitles[PaInstance);
           // SubtitleManager.instance.PlayCurrentSubtitles();
           // PaNum++;
        }


        //Destroy(other);
    }

    public void BridgeCorrection()
    {
        PaInstance = 1;
    }

    private void PlayFootSteps()
    {
        
    }
}