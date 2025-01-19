using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] int NewGame_SceneIndex = 1;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(NewGame_SceneIndex);
    }
}