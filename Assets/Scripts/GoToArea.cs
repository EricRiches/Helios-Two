using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToArea : MonoBehaviour
{

    [SerializeField] string targetLocation;
    public void OnTriggerEnter(Collider other)
    {

        Debug.Log("Entered Collider", gameObject);
        if (other.CompareTag("Player"))
        {
            if (!SceneExists(targetLocation)) { Debug.LogError("scene: " + targetLocation + " does not exist"); return; }
            SceneManager.LoadScene(targetLocation, LoadSceneMode.Single);
        }
    }

    public static bool SceneExists(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
            string extractedSceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (extractedSceneName.Equals(sceneName, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }


}