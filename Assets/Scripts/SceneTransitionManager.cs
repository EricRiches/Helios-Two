using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    Animator animator;
    private void Awake()
    {

        if (instance != null&&instance!=this) Destroy(instance);
        instance = this;


        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public static IEnumerator PlayExitScene(string sceneName)
    {
        if (instance.animator != null)
        {
            instance.animator.SetTrigger("Exit");
        }
        yield return new WaitForSeconds(1.66f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}