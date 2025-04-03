using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableHud : MonoBehaviour
{
    public static EnableHud instance;
    static bool isEnabled = false;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance!= null) Destroy(instance);
        instance = this;
        
        
    }
    private void Start()
    {
        SetHudEnabled(isEnabled);
    }
    public void SetHudEnabled(bool value)
    {
        isEnabled = value;
        gameObject.SetActive(value);
    }
    // Update is called once per frame

    private void OnEnable()
    {
        SetHudEnabled(isEnabled);
    }
}
