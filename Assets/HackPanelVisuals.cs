using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class HackPanelVisuals : MonoBehaviour
{
    public static HackPanelVisuals instance;
    Animator animator;
    [SerializeField] TextMeshProUGUI panelText;
    const string defaultString = "Interact with a terminal\r\nto store it.\r\n\r\nStorage:";

    private void Awake()
    {
        if(instance!=null) { Destroy(instance); }
        instance = this;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Show()
    {
        
        animator.SetTrigger("Show");
    }

    public void SetStorageState(bool value)
    {
        string temp   = defaultString;
        if(value){
            temp += " <color=#00ff00>stored</color>";
            temp += "\nPress f to reactivate.";
        }
        else{
            temp += " <color=#ff0000>empty</color>";
        }
        panelText.text = temp;
       
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
        
    }
}
