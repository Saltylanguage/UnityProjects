using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class Triangulator : MonoBehaviour
{
    public ConvexHullGenerator ch_Gen = new ConvexHullGenerator();
    public List<Geometry.Triangle> mTriangles = new List<Geometry.Triangle>();
    RoomGenerator roomGen;

    [SerializeField]
    public int roomCount;
    [SerializeField]
    int circleIndex = 0;

    private void Start()
    {
        ch_Gen.GeneratePoints(roomCount);
        ch_Gen.Sort(ch_Gen.allPoints);
        ch_Gen.convexHullPoints = ch_Gen.GenerateConvexHull(ch_Gen.allPoints);
        ch_Gen.GetInnerPoints();
        GenerateTriangleFan(ch_Gen.convexHullPoints);
        SubdivideTriangleFan(mTriangles);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int numPoints = DelaunayTriangulation(mTriangles);
            Debug.Log("Result: " + numPoints);
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (circleIndex < mTriangles.Count)
            {
                circleIndex++;
            }
            else
            {
                circleIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (circleIndex > 0)
            {
                circleIndex--;
            }
            else
            {
                circleIndex = mTriangles.Count - 1;
            }
        }
        if (mTriangles.Count > 0)
        {
            DrawTriangles();
        }
    }

    private void OnDrawGizmos()
    {
        if (ch_Gen != null)
        {
            if (mTriangles.Count > circleIndex)
            {
                Handles.DrawWireDisc(mTriangles[circleIndex].circumCircle.center, Vector3.up, mTriangles[circleIndex].circumCircle.radius);
            }
            for (int i = 0; i < ch_Gen.allPoints.Count; i++)
            {
                Gizmos.DrawSphere(ch_Gen.allPoints[i], 0.2f);
            }
        }
    }

    void GenerateTriangleFan(List<Vector3> points)
    {
        if(points.Count > 3)
        {

        for (int i = 1; i < points.Count - 2; i++)
        {
            mTriangles.Add(new Geometry.Triangle
                          (points[0],
                           points[i],
                           points[i + 1]));
        }
        mTriangles.Add(new Geometry.Triangle
                      (points[0],
                       points[points.Count - 2],
                       points[points.Count - 1]));
        }
    }
    void SubdivideTriangleFan(List<Geometry.Triangle> triangles)
    {
        bool triangleFound = false;
        for (int i = 0; i < ch_Gen.innerPoints.Count; i++)
        {
            for (int j = 0; j < triangles.Count; j++)
            {
                triangleFound = PointIsInsideTriangle(ch_Gen.innerPoints[i], triangles[j]);
                if (triangleFound)
                {
                    List<Vector3> innerPoints = ch_Gen.innerPoints;
                    Geometry.Triangle temp = triangles[j];
                    triangles.Add(new Geometry.Triangle(innerPoints[i], temp.pointA, temp.pointB));
                    triangles.Add(new Geometry.Triangle(innerPoints[i], temp.pointB, temp.pointC));
                    triangles.Add(new Geometry.Triangle(innerPoints[i], temp.pointC, temp.pointA));
                    triangles.RemoveAt(j);
                    break;
                }
                if (triangleFound)
                {
                    break;
                }
            }
        }
    }
    public int DelaunayTriangulation(List<Geometry.Triangle> triangles)
    {
        int ret = 0;

        for (int triIndex = 0; triIndex < triangles.Count; triIndex++) // use the original list's size. go until the end of the list.
        {
            int numPoints = 0;
            triangles[triIndex].numPointsInCircle = 0;
            for (int pointIndex = 0; pointIndex < ch_Gen.allPoints.Count; pointIndex++)
            {
                if (Geometry.PointIsInsideCircle(triangles[triIndex].circumCircle, ch_Gen.allPoints[pointIndex]))
                {
                    triangles[triIndex].numPointsInCircle++;
                    numPoints++;
                }
            }
            ret = Mathf.Max(ret, numPoints);
            if (numPoints >= 4)
            {
                for (int subTriIndex = 0; subTriIndex < triangles.Count; subTriIndex++)
                {
                    if (triIndex != subTriIndex)
                    {
                        if (Adjacent(triangles[triIndex], triangles[subTriIndex]))
                        {
                            Vector3[] edgeFlipPoints = new Vector3[4];
                            //find the uncommon points
                            Vector3 uncommonPoint = Geometry.FindUncommonPoint(triangles[triIndex], triangles[subTriIndex]);
                            edgeFlipPoints[0] = uncommonPoint;
                            List<Vector3> pointsFormingAngle = Geometry.FindPointsFormingAngle(triangles[triIndex], uncommonPoint);
                            edgeFlipPoints[1] = pointsFormingAngle[0];
                            edgeFlipPoints[2] = pointsFormingAngle[2];
                            float theta1 = Mathf.Ceil(180 - Geometry.CalculateAngleInDegs(pointsFormingAngle[0], pointsFormingAngle[1], pointsFormingAngle[2]));

                            uncommonPoint = Geometry.FindUncommonPoint(triangles[subTriIndex], triangles[triIndex]);
                            edgeFlipPoints[3] = uncommonPoint;
                            pointsFormingAngle.Clear();
                            pointsFormingAngle = Geometry.FindPointsFormingAngle(triangles[subTriIndex], uncommonPoint);

                            float theta2 = Mathf.Ceil(180 - Geometry.CalculateAngleInDegs(pointsFormingAngle[0], pointsFormingAngle[1], pointsFormingAngle[2]));

                            bool invalidAngle1 = false;
                            if (theta1 == float.NaN || theta1 == 180 )
                            {
                                Debug.Log("Theta1 = " + theta1);
                                invalidAngle1 = true;
                            }
                            bool invalidAngle2 = false;
                            if (theta2 == float.NaN || theta2 == 180 )
                            {
                                Debug.Log("Theta2 = " + theta2);
                                invalidAngle2 = true;
                            }
                            if (theta1 + theta2 >= 180 || invalidAngle1 || invalidAngle2)
                            {
                                Geometry.Triangle temp1 = new Geometry.Triangle(edgeFlipPoints[0], edgeFlipPoints[1], edgeFlipPoints[3]);
                                Geometry.Triangle temp2 = new Geometry.Triangle(edgeFlipPoints[0], edgeFlipPoints[3], edgeFlipPoints[2]);
                                //check if the sum of the angles made of their uncommon edges is greater than 180 degrees

                                triangles[triIndex] = temp1;
                                triangles[subTriIndex] = temp2;
                            }
                        }
                    }
                }
            }

            //TODO check all adjacent triangles and flip only the one with the largest angle
            //Triangles P1P2P3 and P2P4P3 when flipped form new triangles  P1P2P4 and P1P4P3
            numPoints = 0;
        }
        return ret;
    }
    public void TriangulateRooms()
    {
        roomGen = GetComponent<RoomGenerator>();
        ch_Gen.allPoints = new List<Vector3>(new Vector3[roomGen.allRooms.Count]);
        for (int i = 0; i < roomGen.allRooms.Count; i++)
        {
            ch_Gen.allPoints[i] = roomGen.allRooms[i].transform.position;
        }
        ch_Gen.Sort(ch_Gen.allPoints);
        ch_Gen.convexHullPoints = ch_Gen.GenerateConvexHull(ch_Gen.allPoints);
        ch_Gen.GetInnerPoints();
        GenerateTriangleFan(ch_Gen.convexHullPoints);
        SubdivideTriangleFan(mTriangles);
    }

    public bool Adjacent(Geometry.Triangle a, Geometry.Triangle b)
    {
        int count = 0;

        bool AA = a.pointA == b.pointA || a.pointA == b.pointB || a.pointA == b.pointC;
        bool AB = a.pointB == b.pointA || a.pointB == b.pointB || a.pointB == b.pointC;
        bool AC = a.pointC == b.pointA || a.pointC == b.pointB || a.pointC == b.pointC;

        if (AA)
        {
            count++;
        }
        if (AB)
        {
            count++;
        }
        if (AC)
        {
            count++;
        }
        return count == 2;
    }
    public int FindTriangleIndex(Geometry.Triangle a)
    {
        for (int i = 0; i < mTriangles.Count; i++)
        {
            bool aTrue = false;
            bool bTrue = false;
            bool cTrue = false;

            if (Mathf.Approximately(a.pointA.x, mTriangles[i].pointA.x))
            {
                if (Mathf.Approximately(a.pointA.y, mTriangles[i].pointA.y))
                {
                    aTrue = true;
                }
            }
            if (Mathf.Approximately(a.pointB.x, mTriangles[i].pointB.x))
            {
                if (Mathf.Approximately(a.pointB.y, mTriangles[i].pointB.y))
                {
                    bTrue = true;
                }
            }
            if (Mathf.Approximately(a.pointC.x, mTriangles[i].pointC.x))
            {
                if (Mathf.Approximately(a.pointC.y, mTriangles[i].pointC.y))
                {
                    cTrue = true;
                }
            }

            if (aTrue && bTrue && cTrue)
            {
                return i;
            }

        }
        return -1;
    }
    public bool PointIsInsideTriangle(Vector3 point, Geometry.Triangle triangle)
    {
        List<Vector3> trianglePoints = new List<Vector3>();
        trianglePoints.Add(triangle.pointA);
        trianglePoints.Add(triangle.pointB);
        trianglePoints.Add(triangle.pointC);
        trianglePoints.Add(point);

        ch_Gen.Sort(trianglePoints);
        List<Vector3> hull = ch_Gen.GenerateConvexHull(trianglePoints);

        if (hull.Count == 3)
        {
            if (hull.Contains(point))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    private void DrawTriangles()
    {
        Color color = Color.blue;
        for (int i = 0; i < mTriangles.Count; i++)
        {
            Debug.DrawLine(mTriangles[i].pointA, mTriangles[i].pointB, color);
            Debug.DrawLine(mTriangles[i].pointB, mTriangles[i].pointC, color);
            Debug.DrawLine(mTriangles[i].pointC, mTriangles[i].pointA, color);
        }
    }
}
