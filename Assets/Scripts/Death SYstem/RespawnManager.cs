using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] float GroundOffset;

    [Header("Death Canvas")]
    [SerializeField] GameObject DeathScreenUI;
    [SerializeField] VideoPlayer DeathScreenPlayer;

    List<RespawnSavePoint> RespawnablePoints = new List<RespawnSavePoint>();
    ResetDeathCamera[] camerasForDies = new ResetDeathCamera[0];
    MonsterBehavior[] monstersInScene = new MonsterBehavior[0];
    string lastHitSavePointID;
    SafeArea safeArea;

    SC_FPSController playerMove;
    float ResetPlayer = -1;

    private void Start()
    {
        playerMove = FindObjectOfType<SC_FPSController>();
        camerasForDies = FindObjectsOfType<ResetDeathCamera>();
        monstersInScene = FindObjectsOfType<MonsterBehavior>();
        safeArea = FindObjectOfType<SafeArea>();
    }

    private void Update()
    {
        if (ResetPlayer > 0)
        {
            ResetPlayer -= Time.deltaTime;

            if (ResetPlayer <= 0)
            {
                playerMove.enabled = true;
                ResetPlayer = -1;
            }
        }
    }

    public void SetRespawnPoint(string ID)
    {
        lastHitSavePointID = ID;
    }

    public void AddRespawnPointToManager(RespawnSavePoint point)
    {
        RespawnablePoints.Add(point);
    }

    public void OpenDeathUI()
    {
        Cursor.lockState = CursorLockMode.None;
        playerMove.enabled = false;
        DeathScreenUI.SetActive(true);
        DeathScreenPlayer.Play();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        SetCorrectMimic.hasVisitedBefore = false; //Set it so starter mimic spawn on first playthrough.
    }

    public void RespawnPlayer()
    {
        DeathScreenUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

        ResetPlayer = 0.5f;
        if (safeArea != null)
        {
            safeArea.ResetAfterDeath();
        }

        foreach (RespawnSavePoint point in RespawnablePoints)
        { 
            if (point.PointID == lastHitSavePointID)
            {
                playerMove.transform.position = point.transform.position + (Vector3.up * GroundOffset);
            }
        }

        foreach (ResetDeathCamera cameraRes in camerasForDies)
        {
            cameraRes.DeathCamer.SetActive(false);
        }

        foreach (MonsterBehavior mon in monstersInScene)
        {
            mon.PlayerReset();
        }
    }
}
