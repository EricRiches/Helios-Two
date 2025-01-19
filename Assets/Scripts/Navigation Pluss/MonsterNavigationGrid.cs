using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNavigationGrid : MonoBehaviour
{
    [SerializeField]float Grid_BlockSize_Half = 0;
    [SerializeField]LayerMask WalkableMask = new LayerMask();

    [SerializeField]List<Vector3> FinishedNavGridSpaces = new List<Vector3>();


    public void InitializeGrid(float newGridSize, LayerMask walkMask, List<Vector3> GridSpaces)
    {
        Grid_BlockSize_Half = newGridSize;
        WalkableMask = walkMask;
        FinishedNavGridSpaces = GridSpaces;
    }

    #region Roam

    public bool FindRoamPosition(out Vector3 OutputedPosition)
    {
        if (FinishedNavGridSpaces.Count > 0)
        {
            bool hasFoundMovablePosition = false;
            int RandomGridSection = Random.Range(0, FinishedNavGridSpaces.Count);
            Vector3 output = Vector3.zero;

            Vector3 TestOrigin = FinishedNavGridSpaces[RandomGridSection];
            TestOrigin.y += Grid_BlockSize_Half;

            while (!hasFoundMovablePosition)
            {
                Vector3 CheckPosition = TestOrigin;

                CheckPosition.z += Random.Range(-Grid_BlockSize_Half, Grid_BlockSize_Half);
                CheckPosition.x += Random.Range(-Grid_BlockSize_Half, Grid_BlockSize_Half);

                RaycastHit hit;
                if (Physics.Raycast(CheckPosition, Vector3.down, out hit, Grid_BlockSize_Half * 2, WalkableMask))
                {
                    output = hit.point;
                    hasFoundMovablePosition = true;
                }
            }

            OutputedPosition = output;
            return true;
        }
        else
        {
            Debug.LogError("There is no NavGrid created, cannot locate move position");
            OutputedPosition = Vector3.zero;
            return false;
        }
    }

    #endregion
}
