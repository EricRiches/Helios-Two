using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_VertCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>();
        GetMaxVertCount(meshFilters);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GetMaxVertCount(MeshFilter[] meshFilters)
    {
        int maxVertCount = -10;
        MeshFilter maxVertRenderer = null;
        foreach (MeshFilter mesh in meshFilters)
        {
            if(mesh.mesh.vertexCount > maxVertCount)
            {
                maxVertRenderer = mesh;
                maxVertCount = mesh.mesh.vertexCount;
            }
        }

    }
}
