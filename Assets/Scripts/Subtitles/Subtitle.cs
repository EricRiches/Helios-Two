using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "HeliosTao/Subtitle")]
public class Subtitle : ScriptableObject
{

    [SerializeField] string[] subtitles;
    [SerializeField] EventReference[] events;
}
