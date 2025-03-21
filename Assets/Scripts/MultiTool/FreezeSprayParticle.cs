using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSprayParticle : MonoBehaviour
{
    public static FreezeSprayParticle instance;
    [SerializeField] ParticleSystem particle;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
            instance = this;
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (particle == null) { GetComponentInChildren<ParticleSystem>(); } 
    }


    public static void Toggle()
    {
        if (instance?.particle == null){ Debug.LogError("Particle system not set for FreezeSprayParticle"); return; }
        if (instance.particle.isPlaying)
        {
            instance.particle.Stop();
        }
        else
        {
            instance.particle.Play();
        }
    }
    static int a, b;
    public static void SetActive(bool value)
    {
        if (value)
        {
            instance.particle.Play(); instance.particle.Play();
            a++;

        }
        else
        {
            instance.particle.Stop();
            b++;
        }
        Debug.Log(a + "   |  " + b);
    }

}
