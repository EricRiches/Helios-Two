using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] byte activatedLevers = 0;
    [SerializeField] int totalLeverCount = 0;

    [SerializeField] UnityEvent OnAllLeversFlipped;
    // Start is called before the first frame update
    void Start()
    {
        totalLeverCount = FindObjectsOfType<Interaction_GeneratorSwitch>().Length;
    }

    public void AddLever()
    {
        activatedLevers++;
        CheckActivatedLeverCount();
    }
    public void CheckActivatedLeverCount()
    {
        if (activatedLevers >= totalLeverCount)
        {
            Debug.Log("ended");
            OnAllLeversFlipped.Invoke();
            // end game.
        }
    }
}
