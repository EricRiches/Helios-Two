using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class JackVisionChecker : MonoBehaviour
{
    Camera cam;
    [SerializeField] Transform ObjectChecker;
    [SerializeField] LayerMask hitMask;

    public List<JackInTheBoxMonster> JackInTheBoxes = new List<JackInTheBoxMonster>();

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (JackInTheBoxes.Count > 0)
        {
            foreach (JackInTheBoxMonster Monster in JackInTheBoxes)
            {
                Vector3 ViewportPositionOfMonster = cam.WorldToViewportPoint(Monster.transform.position);
                bool isInCamera = true;

                if (ViewportPositionOfMonster.x < 0 || ViewportPositionOfMonster.x > 1)
                {
                    isInCamera = false;
                }
                if (ViewportPositionOfMonster.y < 0 || ViewportPositionOfMonster.y > 1)
                {
                    isInCamera = false;
                }
                if (ViewportPositionOfMonster.z <= 0)
                {
                    isInCamera = false;
                }

                if (isInCamera)
                {
                    ObjectChecker.LookAt(Monster.transform.position);

                    RaycastHit hit;
                    if (Physics.Raycast(ObjectChecker.position, ObjectChecker.forward, out hit, Mathf.Infinity, hitMask, QueryTriggerInteraction.Ignore))
                    {
                        //Debug.Log(hit.transform.name);

                        if (hit.transform.GetComponent<JackInTheBoxMonster>() != Monster)
                        {
                            isInCamera = false;
                        }
                    }
                    else
                    {
                        isInCamera = false;
                    }
                }

                if (isInCamera)
                {
                    Monster.setIsCurrentlySeen(transform.position);
                }
            }
        }
    }
}
