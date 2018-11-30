using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexHullGenerator
{
    public List<Vector3> allPoints = new List<Vector3>();
    public List<Vector3> convexHullPoints;
    public List<Vector3> innerPoints = new List<Vector3>();

    private void Start()
    {

    }
    private void Update()
    {
        DrawConvexHull();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < allPoints.Count; i++)
        {
            Gizmos.DrawSphere(allPoints[i], 0.1f);
        }
    }
    public void DrawConvexHull()
    {
        Color color = Color.blue;
        for (int i = 0; i < convexHullPoints.Count - 1; i++)
        {
            if (i >= 1)
            {
                color = Color.green;
            }
            Debug.DrawLine(convexHullPoints[i], convexHullPoints[i + 1], color);
        }
        Debug.DrawLine(convexHullPoints[convexHullPoints.Count - 1], convexHullPoints[0], Color.red);
    }

    public void GeneratePoints(int roomCount)
    {
        for (int pointIndex = 0; pointIndex < roomCount; pointIndex++)
        {
            Vector2 point = Random.insideUnitCircle * roomCount / 10;
            allPoints.Add(new Vector3(point.x, 0.0f, point.y));
        }
    }

    public void SortByAngle(ref List<Vector3> points)
    {
        var startPoint = points[0];
        AngleComparer angleComparer = new AngleComparer(startPoint);
        points.Sort(1, points.Count - 1, angleComparer);
    }
    public void Sort(List<Vector3> points)
    {
        if (points.Count > 1)
        {
            points.Sort((a, b) => a.z.CompareTo(b.z));
            SortByAngle(ref points);
        }
    }

    public List<Vector3> GenerateConvexHull(List<Vector3> points)
    {
        List<Vector3> convexStack = new List<Vector3>();
        if (points.Count > 1)
        {
            convexStack.Add(points[0]);
            convexStack.Add(points[1]);


            for (int i = 2; i < points.Count; ++i)
            {
                Vector3 a = convexStack[convexStack.Count - 2];
                Vector3 b = convexStack[convexStack.Count - 1];
                Vector3 temp = points[i];

                float result = LeftRightCheck(a, b, temp);

                if (result > 0)
                {
                    convexStack.Add(temp);
                }
                else if (result < 0)
                {
                    convexStack.RemoveAt(convexStack.Count - 1);
                    --i;
                }
                else
                {
                    if (Vector3.SqrMagnitude(b - a) < Vector3.SqrMagnitude(temp - a))
                    {
                        convexStack.RemoveAt(convexStack.Count - 1);
                        convexStack.Add(temp);
                    }
                }
            }
        }
        return convexStack;
    }
    public void GetInnerPoints()
    {
        for (int i = 0; i < allPoints.Count; i++)
        {
            if (!convexHullPoints.Contains(allPoints[i]))
            {
                innerPoints.Add(new Vector3(allPoints[i].x, allPoints[i].y, allPoints[i].z));
            }
        }
    }

    float LeftRightCheck(Vector3 pointA, Vector3 pointB, Vector3 queryPoint)
    {
        Vector3 firstLine = queryPoint - pointA;
        Vector3 secondLine = pointB - queryPoint;
        return Mathf.Sign(Vector3.Cross(firstLine, secondLine).y);
    }

}
