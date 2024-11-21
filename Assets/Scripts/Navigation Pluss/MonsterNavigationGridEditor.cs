using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MonsterNavigationGrid))]
public class MonsterNavigationGridEditor : MonoBehaviour
{
    [SerializeField] Vector3 Grid_OriginLocation;
    [SerializeField] Vector3Int Grid_GridSize;
    [SerializeField] float Grid_BlockSize_Half;
    [SerializeField] LayerMask WalkableMask;

    [SerializeField] Color GridColor_NoGround;

    [Header("Generate")]
    [SerializeField] bool StartGeneration;
    [SerializeField] bool ResetGeneration;

    [SerializeField]NavGridStep currentStep = NavGridStep.Prep;
    Vector3Int OnGeneratingGridSection = Vector3Int.zero;

    MonsterNavigationGrid gridForInPlay;

    [Header("DO NOT TOUCH")]
    [SerializeField]List<Vector3> FinishedNavGridSpaces = new List<Vector3>();

    private void Update()
    {
        if (gridForInPlay == null)
        {
            gridForInPlay = GetComponent<MonsterNavigationGrid>();
        }

        if (StartGeneration)
        {
            StartGeneration = false;

            if (currentStep == NavGridStep.Prep)
            {
                currentStep = NavGridStep.Generating;
                OnGeneratingGridSection = Vector3Int.zero;
                if (FinishedNavGridSpaces.Count > 0)
                {
                    FinishedNavGridSpaces.Clear();
                }
            }
        }

        if (ResetGeneration)
        {
            ResetGeneration = false;
            currentStep = NavGridStep.Prep;
        }

        while (currentStep == NavGridStep.Generating)
        {
            SetUpGridMesh();
        }
    }

    #region GridSetUp
    void SetUpGridMesh()
    {
        Vector3 CheckPosition = new Vector3(
                Grid_OriginLocation.x + (Grid_BlockSize_Half * 2 * OnGeneratingGridSection.x),
                Grid_OriginLocation.y + (Grid_BlockSize_Half * 2 * OnGeneratingGridSection.y),
                Grid_OriginLocation.z + (Grid_BlockSize_Half * 2 * OnGeneratingGridSection.z));

        if (Physics.CheckBox(CheckPosition, Vector3.one * Grid_BlockSize_Half, Quaternion.identity, WalkableMask))
        {
            int CoverPercent = 0;
            float GridAxisSize = Grid_BlockSize_Half * 2;
            float TestLength = GridAxisSize / 10;

            for (int x = 0; x < 10; x++)
            {
                for (int z = 0; z < 10; z++)
                {
                    Vector3 TestOrigin = new Vector3
                        (CheckPosition.x - Grid_BlockSize_Half + (x * TestLength),
                        CheckPosition.y + Grid_BlockSize_Half,
                        CheckPosition.z - Grid_BlockSize_Half + (z * TestLength));

                    if (Physics.Raycast(TestOrigin, Vector3.down, GridAxisSize, WalkableMask))
                    {
                        CoverPercent++;
                    }
                }
            }

            if (CoverPercent > 50)
            {
                FinishedNavGridSpaces.Add(CheckPosition);
            }
        }

        OnGeneratingGridSection.x++;
        if (OnGeneratingGridSection.x == Grid_GridSize.x)
        {
            OnGeneratingGridSection.x = 0;
            OnGeneratingGridSection.z++;
        }
        if (OnGeneratingGridSection.z > Grid_GridSize.z)
        {
            OnGeneratingGridSection.z = 0;
            OnGeneratingGridSection.y++;
        }
        if (OnGeneratingGridSection.y > Grid_GridSize.y)
        {
            currentStep = NavGridStep.Finished;
            gridForInPlay.InitializeGrid(Grid_BlockSize_Half, WalkableMask, FinishedNavGridSpaces);
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        if (currentStep == NavGridStep.Prep)
        {
            #region Prep
            List<Vector3> GridPoints = new List<Vector3>();

            for (int x = 0; x < Grid_GridSize.x; x++)
            {
                for (int z = 0; z < Grid_GridSize.z; z++)
                {
                    for (int y = 0; y < Grid_GridSize.y; y++)
                    {
                        Vector3 newPoint = new Vector3(
                        Grid_OriginLocation.x + (Grid_BlockSize_Half * 2 * x),
                        Grid_OriginLocation.y + (Grid_BlockSize_Half * 2 * y),
                        Grid_OriginLocation.z + (Grid_BlockSize_Half * 2 * z));
                        GridPoints.Add(newPoint);
                    }
                }
            }
            

            Gizmos.color = GridColor_NoGround;
            foreach (Vector3 point in GridPoints)
            {
                Vector3 Point1 = new Vector3(point.x - Grid_BlockSize_Half, point.y - Grid_BlockSize_Half, point.z - Grid_BlockSize_Half);
                Vector3 Point2 = new Vector3(point.x - Grid_BlockSize_Half, point.y - Grid_BlockSize_Half, point.z + Grid_BlockSize_Half);
                Vector3 Point3 = new Vector3(point.x + Grid_BlockSize_Half, point.y - Grid_BlockSize_Half, point.z + Grid_BlockSize_Half);
                Vector3 Point4 = new Vector3(point.x + Grid_BlockSize_Half, point.y - Grid_BlockSize_Half, point.z - Grid_BlockSize_Half);

                Gizmos.DrawLine(Point1, Point2);
                Gizmos.DrawLine(Point2, Point3);
                Gizmos.DrawLine(Point3, Point4);
                Gizmos.DrawLine(Point4, Point1);


                Vector3 Point5 = Point1;
                Vector3 Point6 = Point2;
                Vector3 Point7 = Point3;
                Vector3 Point8 = Point4;
                Point5.y += (Grid_BlockSize_Half * 2);
                Point6.y += (Grid_BlockSize_Half * 2);
                Point7.y += (Grid_BlockSize_Half * 2);
                Point8.y += (Grid_BlockSize_Half * 2);

                Gizmos.DrawLine(Point5, Point6);
                Gizmos.DrawLine(Point6, Point7);
                Gizmos.DrawLine(Point7, Point8);
                Gizmos.DrawLine(Point8, Point5);

                Gizmos.DrawLine(Point1, Point5);
                Gizmos.DrawLine(Point2, Point6);
                Gizmos.DrawLine(Point3, Point7);
                Gizmos.DrawLine(Point4, Point8);
            }
            #endregion
        }
        else if (currentStep == NavGridStep.Finished)
        {
            #region Finished
            if (FinishedNavGridSpaces.Count > 0)
            {
                Gizmos.color = Color.blue;
                foreach (Vector3 point in FinishedNavGridSpaces)
                {
                    Vector3 Point1 = new Vector3(point.x - Grid_BlockSize_Half, point.y - Grid_BlockSize_Half, point.z - Grid_BlockSize_Half);
                    Vector3 Point2 = new Vector3(point.x - Grid_BlockSize_Half, point.y - Grid_BlockSize_Half, point.z + Grid_BlockSize_Half);
                    Vector3 Point3 = new Vector3(point.x + Grid_BlockSize_Half, point.y - Grid_BlockSize_Half, point.z + Grid_BlockSize_Half);
                    Vector3 Point4 = new Vector3(point.x + Grid_BlockSize_Half, point.y - Grid_BlockSize_Half, point.z - Grid_BlockSize_Half);

                    Gizmos.DrawLine(Point1, Point2);
                    Gizmos.DrawLine(Point2, Point3);
                    Gizmos.DrawLine(Point3, Point4);
                    Gizmos.DrawLine(Point4, Point1);


                    Vector3 Point5 = Point1;
                    Vector3 Point6 = Point2;
                    Vector3 Point7 = Point3;
                    Vector3 Point8 = Point4;
                    Point5.y += (Grid_BlockSize_Half * 2);
                    Point6.y += (Grid_BlockSize_Half * 2);
                    Point7.y += (Grid_BlockSize_Half * 2);
                    Point8.y += (Grid_BlockSize_Half * 2);

                    Gizmos.DrawLine(Point5, Point6);
                    Gizmos.DrawLine(Point6, Point7);
                    Gizmos.DrawLine(Point7, Point8);
                    Gizmos.DrawLine(Point8, Point5);

                    Gizmos.DrawLine(Point1, Point5);
                    Gizmos.DrawLine(Point2, Point6);
                    Gizmos.DrawLine(Point3, Point7);
                    Gizmos.DrawLine(Point4, Point8);

                    //Gizmos.DrawSphere(point, 0.1f);
                }
            }
            #endregion
        }
    }
}

public enum NavGridStep
{
    Prep,
    Generating,
    Finished
}