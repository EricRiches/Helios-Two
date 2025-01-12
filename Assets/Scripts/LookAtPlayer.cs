using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    public GameObject camera;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(camera.transform);
    }
}
