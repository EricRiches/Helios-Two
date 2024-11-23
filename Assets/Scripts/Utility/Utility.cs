using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Utility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class VectorExtensions
{

    public static Vector3 Vec2SetY(this Vector2 vector, float y)
    {
        return new(vector.x, y, vector.y);
    }

    public static Vector3 SetY(this Vector3 vector, float y)
    {
        return new(vector.x, y, vector.z);
    }

    

}
