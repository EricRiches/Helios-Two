using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Objectives : MonoBehaviour
{
    public static Objectives instance;
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] List<string> objectiveList = new List<string>();
    public int index = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        textField.text = objectiveList[index];
    }

    public void NextObjective()
    {
        if (objectiveList.Count > 0 && objectiveList.Count >= index + 1)
        {
            index++;
            textField.text = objectiveList[index];
        }
        else if (objectiveList.Count > 0 && objectiveList.Count! >= index + 1)
        {
            ResetIndex();
        }
    }

    public void ResetIndex()
    {
        index = 0;
        textField.text = objectiveList[index];
    }

    public void DestroyThyself()
    {
        Destroy(gameObject);
    }
}
