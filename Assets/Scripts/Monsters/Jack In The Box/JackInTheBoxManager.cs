using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackInTheBoxManager : MonoBehaviour
{
    [SerializeField] int StartAmountOfJackInTheBoxes;
    [SerializeField] List<Transform> JackInTheBoxSpawnPoints;
    [SerializeField] GameObject JackInTheBoxPrefab;

    List<Transform> JackInTheBoxUnusedSpawnPoints = new List<Transform>();
    List<GameObject> JackInTheBoxObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        JackInTheBoxUnusedSpawnPoints = JackInTheBoxSpawnPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRunBack()
    {

    }
}
