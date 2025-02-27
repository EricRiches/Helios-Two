using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackInTheBoxManager : MonoBehaviour
{
    [SerializeField] int StartAmountOfJackInTheBoxes;
    [SerializeField] List<Transform> JackInTheBoxSpawnPoints;
    [SerializeField] GameObject JackInTheBoxMotionlessPrefab;
    [SerializeField] JackInTheBoxMonster JackInTheBoxPrefab;
    [SerializeField] Vector2 TimeBeforeNextBox;

    List<Transform> JackInTheBoxUnusedSpawnPoints = new List<Transform>();
    List<GameObject> JackInTheBoxObjects = new List<GameObject>();
    List<JackInTheBoxMonster> JackInTheBoxMonster = new List<JackInTheBoxMonster>();

    [SerializeField]float TimerForBoxes;

    // Start is called before the first frame update
    void Start()
    {
        TimerForBoxes = Random.Range(TimeBeforeNextBox.x, TimeBeforeNextBox.y);
        foreach (Transform SpawnPointThingy in JackInTheBoxSpawnPoints)
        {
            JackInTheBoxUnusedSpawnPoints.Add(SpawnPointThingy);
        }
        for (int i = 0; i < StartAmountOfJackInTheBoxes; i++)
        {
            AddAnotherBox();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (JackInTheBoxUnusedSpawnPoints.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                AddAnotherBox();
            }

            TimerForBoxes -= Time.deltaTime;
            if (TimerForBoxes <= 0)
            {
                TimerForBoxes = Random.Range(TimeBeforeNextBox.x, TimeBeforeNextBox.y);
                AddAnotherBox();
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (JackInTheBoxObjects.Count > 0)
            {
                StartRunBack();
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerRespawn();
        }
    }

    public void StartRunBack()
    {
        for (int i = 0; i < JackInTheBoxObjects.Count; i++)
        {
            JackInTheBoxMonster.Add(Instantiate(JackInTheBoxPrefab, JackInTheBoxObjects[i].transform.position, JackInTheBoxObjects[i].transform.rotation).GetComponent<JackInTheBoxMonster>());
            Destroy(JackInTheBoxObjects[i]);
        }
        JackInTheBoxObjects.Clear();
    }

    public void AddAnotherBox()
    {
        int RandomLocation = Random.Range(0, JackInTheBoxUnusedSpawnPoints.Count);

        Transform BoxPosition = JackInTheBoxUnusedSpawnPoints[RandomLocation];
        JackInTheBoxObjects.Add(Instantiate(JackInTheBoxMotionlessPrefab, BoxPosition.position, BoxPosition.rotation));
        JackInTheBoxUnusedSpawnPoints.Remove(BoxPosition);
    }

    public void PlayerRespawn()
    {
        if (JackInTheBoxMonster.Count > 0)
        {
            for (int i = 0; i < JackInTheBoxMonster.Count; i++)
            {
                JackInTheBoxMonster[i].RemoveFromPlay();
            }

            JackInTheBoxMonster.Clear();
        }

        TimerForBoxes = Random.Range(TimeBeforeNextBox.x, TimeBeforeNextBox.y);
        JackInTheBoxUnusedSpawnPoints.Clear();
        foreach (Transform SpawnPointThingy in JackInTheBoxSpawnPoints)
        {
            JackInTheBoxUnusedSpawnPoints.Add(SpawnPointThingy);
        }
        for (int i = 0; i < StartAmountOfJackInTheBoxes; i++)
        {
            AddAnotherBox();
        }
    }
}