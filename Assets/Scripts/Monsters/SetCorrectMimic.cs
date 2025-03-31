using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCorrectMimic : MonoBehaviour
{
    public static bool hasVisitedBefore = false;
    [SerializeField] GameObject StarterMimic;
    [SerializeField] GameObject NormalMimic;

    // Start is called before the first frame update
    void Start()
    {
        StarterMimic.SetActive(!hasVisitedBefore);
        NormalMimic.SetActive(hasVisitedBefore);
        hasVisitedBefore = true;
    }
}
