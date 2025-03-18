using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    bool isIn = false;
    [SerializeField] List<GameObject> objects = new List<GameObject>();

    private void Awake()
    {
        Disable(GetObjects(objects));
    }

    public static List<GameObject> GetObjects(List<GameObject> objects)
    {
        return objects;
    }

    public static void Disable(List<GameObject> objects)
    {
        Debug.Log("Disable");
        if (CarryOvers.paObj.Count > 0)
        {
            Debug.Log("Counted " + CarryOvers.paObj.Count);
            foreach (string obj in CarryOvers.GetPAObj())
            {
                Debug.Log(obj);
                try
                {
                    for (int i = 0; i < objects.Count; i++)
                    {
                        if (objects[i].name == obj)
                        {
                            objects[i].SetActive(false);
                            break;
                        }
                    }
                }
                catch { }
            }
        }
    }
}
