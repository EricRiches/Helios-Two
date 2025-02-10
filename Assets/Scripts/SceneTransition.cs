using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    [SerializeField] bool toTram;
    [SerializeField] Vector3 positionOnLoad = Vector3.negativeInfinity;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // destroy duplicates
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // persist through scene transitions.
    }

    public void SetPositionOnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if(positionOnLoad == Vector3.negativeInfinity && !toTram) { Debug.LogError("Loading zone position is: " + positionOnLoad.ToString() + "Please double check the target position variable is set."); return; }
        var playerController = FindObjectOfType<CharacterController>();
        if (playerController != null)
        {

            if (toTram)
            {

                try
                {
                    Transform tramSP = GameObject.Find("Tram_SpawnPoint").transform;
                    positionOnLoad = tramSP.position;
                }
                catch
                {
                    Debug.LogError("Tram_SpawnPoint   object not found."); 
                    return; 
                }
            }


    
            try {
                playerController.enabled = false;
                playerController.transform.position = positionOnLoad;
                Debug.Log($"Player position set to: {positionOnLoad}");
                playerController.enabled = true;
                toTram = false;
                positionOnLoad = Vector3.negativeInfinity; // set to negative infinity means "this loading zone has no target position set".
            } 
            catch { 
                Debug.LogError("tryied to assign negative infinity to Player position"); 
            }

        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetPositionOnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetPositionOnSceneLoad;
    }

    public void LoadScene(string sceneName, Vector3 newPosition)
    {
        positionOnLoad = newPosition;
        if (!SceneExists(sceneName)) { Debug.LogError("Scene name: " + sceneName + " Not found."); return; }
        if(positionOnLoad == Vector3.negativeInfinity) { 
            Debug.LogError("Loading zone position is: " + positionOnLoad.ToString() + "Please double check the target position variable is set.");
            return;
        }
        positionOnLoad = newPosition;
        SceneManager.LoadScene(sceneName);
        
    }
    /// <summary>
    /// load the scene name given, but send player to tram spawn point.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadSceneToTram(string sceneName)
    {
        if (!SceneExists(sceneName)) { Debug.LogError("Scene name: " + sceneName + " Not found."); return; }
        positionOnLoad = Vector3.negativeInfinity;
        toTram = true;
        SceneManager.LoadScene(sceneName);

    }



    /// <summary>
    /// checks if a scene exists.
    /// </summary>
    /// <param name="sceneName"> name of the scene to check for.</param>
    /// <returns> whether or not a scene of this name is in the build settings.</returns>
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
