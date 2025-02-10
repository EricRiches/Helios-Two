using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Panel_UI : MonoBehaviour
{
    [SerializeField] Interaction_FocusObject focusObject;
    [SerializeField] Transform cursor;
    public UI_Option[] options;
    [SerializeField] int defaultIndex= 0;
    [SerializeField] int selectedIndex = 0;
    bool vertical = true;
    bool invert = false;
    string axisName;
    [SerializeField] Vector3 cursorOffset = new Vector3(-160, 12.5f, 0);
    [SerializeField] UnityEvent OnOptionSelect;

    Coroutine OnFocusCoroutine;
    private void Start()
    {
        if (vertical) axisName = "Vertical";
        else { axisName = "Horizontal"; }
    }

    public void OnFocus()
    {
        if (OnFocusCoroutine != null) StopCoroutine(OnFocusCoroutine); OnFocusCoroutine = null;
        OnFocusCoroutine = StartCoroutine(FocusCoroutine());
    }

    IEnumerator FocusCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.15f);
        while (true)
        {
            yield return new WaitUntil(() => Input.GetAxisRaw(axisName) != 0);
            int input = Math.Sign(Input.GetAxisRaw(axisName));
            if (input != 0)
            {
                selectedIndex -= ( invert ? -1:1 )*input;

                // Wrap around to avoid underflow
                if (selectedIndex < 0)
                {
                    selectedIndex += options.Length;
                }
                selectedIndex %= options.Length;
            }
            SetCursor();
            yield return wait;
        }
    }

    void SetCursor()
    {
        cursor.SetParent(options[selectedIndex].transform);
        cursor.localPosition = cursorOffset;
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F)) { OnSelect(); }
    }

    void OnSelect()
    {
        if (OnFocusCoroutine == null) return;
        options[selectedIndex].Activate.Invoke(); // invoke wtv we subscribe to the event for that option.
        StopCoroutine(OnFocusCoroutine); // stop checking input for changing index.
        OnFocusCoroutine = null;
        if (focusObject != null) // if using a interaction_focusObject, stop focusing.
        {
            focusObject.forceExit = true;
        }

        selectedIndex = 0;
        OnOptionSelect.Invoke();
    }
}
