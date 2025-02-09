using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToArea : MonoBehaviour
{

    [SerializeField] string targetScene;
    [SerializeField] Vector3 targetPosition;
    public void OnTriggerEnter(Collider other)
    {

        Debug.Log("Entered Collider", gameObject);
        if (other.CompareTag("Player"))
        {
            SceneTransition.instance.LoadScene(targetScene, targetPosition );
        }
    }



}