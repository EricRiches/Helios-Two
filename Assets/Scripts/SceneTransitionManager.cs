using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    static Dictionary<int, string> mapNames = new()
    {
        { 1,"Medical Wing" },
        { 2,"Bridge" },
        { 3,"Crew Deck" },
        { 4,"Engineering Wing" },
        { 5,"Hangar Bay" }
    };

    Animator animator;
    TextMeshProUGUI locationText;
    private void Awake()
    {
        if (instance != null&&instance!=this) Destroy(instance);
        instance = this;

        animator = GetComponent<Animator>();

        locationText = GetComponentInChildren<TextMeshProUGUI>();
        if (instance.locationText != null)
        {
            instance.locationText.text = mapNames[SceneManager.GetActiveScene().buildIndex];
        }
    }
    
    // Start is called before the first frame update
    public static IEnumerator PlayExitScene()
    {
        if (instance.animator != null)
        {
            instance.animator.SetTrigger("Exit");

        }
        yield return new WaitForSeconds(1.66f);
    }
}