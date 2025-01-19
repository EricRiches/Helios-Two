using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToArea : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {

        Debug.Log("Entered Collider", gameObject);
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Bridge", LoadSceneMode.Single);
        }
    }
}