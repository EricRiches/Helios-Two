using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    [SerializeField] GameObject pauseUI;

    static bool isPaused;

    public static bool IsPaused
    {
        get { return isPaused; }
    }

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        isPaused = !isPaused;
        if (IsPaused)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }



    }

    public void ReturnToMenu()
    {
        SceneTransition.instance.LoadScene("Main Menu");
        SetCorrectMimic.hasVisitedBefore = false; //Set it so starter mimic spawn on first playthrough.
    }
}
