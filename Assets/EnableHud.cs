using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableHud : MonoBehaviour
{
    public static EnableHud instance;
    public static bool isEnabled = true;
    Canvas canvas;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance!= null) Destroy(instance);
        instance = this;
        
        
    }
    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        SetHudEnabled(isEnabled);
    }
    public void SetHudEnabled(bool value)
    {
        isEnabled = value;
        canvas.enabled = value;
    }
    // Update is called once per frame

    private void OnEnable()
    {
        if (canvas == null) return;
        SetHudEnabled(isEnabled);
    }
}
