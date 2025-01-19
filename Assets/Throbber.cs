using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throbber : MonoBehaviour
{
    public static Throbber instance;
    [SerializeField] Image fillImage;
    [SerializeField] Image background;
    private void Awake()
    {
        if(instance != null) Destroy(instance);
        instance = this;
    }

    public void SetVisibility(bool value)
    {
        background.gameObject.SetActive(value);
        fillImage.gameObject.SetActive(value);
    }
    public void UpdateFillAmount(float value)
    {
        // Ensure value is clamped between 0 and 1
        fillImage.fillAmount = Mathf.Clamp01(value);
    }
}
