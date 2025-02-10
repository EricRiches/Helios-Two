using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [SerializeField] CanvasGroup canvasGroup;

    static bool isPaused;

    public static bool IsPaused { 
    get { return isPaused; }
    }

    private void Awake()
    {
        if(instance != null)Destroy(instance);
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public  void PauseGame()
    {
        isPaused = !isPaused;
        if (IsPaused)
        {
            Time.timeScale = 0;
            canvasGroup.alpha = 0.55f;
        }
        else
        {
            Time.timeScale = 1;
            canvasGroup.alpha = 0;
        }
       

        
    }
}
