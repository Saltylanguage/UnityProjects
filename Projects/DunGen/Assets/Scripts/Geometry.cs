using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry
{
    public static float kPi = 3.14159f;
    public static float kRadToDeg = 180 / kPi;
    public static float kDegToRad = kPi / 180;

    [System.Serializable]
    public enum CellType
    {
        Empty = 0,
        MajorRoom,
        MinorRoom,
        Hallway,

        MAXTYPE
    }

    [System.Serializable]
    public class Coord
    {
        public int x;
        public int y;
        public CellType type;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
            type = CellType.Empty;
        }

        public static bool operator ==(Coord a, Coord b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Coord a, Coord b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public bool Equals(Coord other)
        {
            return this == other;
        }

    }

    [System.Serializable]
    public class Line
    {
        public Line() { }
        public Line(Vector3 pointA, Vector3 pointB)
        {
            start = pointA;
            start.y = 0;
            end = pointB;
            end.y = 0;

            rise = pointA.z - pointB.z;
            run = pointA.x - pointB.x;

            c = (rise * start.x) + (run * end.z);

            float deltaX = pointA.x - pointB.x;
            float deltaY = pointA.y - pointB.y;
            lenghth = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
        }

        public Vector3 start;
        public Vector3 end;

        public int startIndex;
        public int endIndex;

        public float rise;
        public float run;
        public float c;

        public float lenghth;
    }

    [System.Serializable]
    public class Triangle
    {
        public Triangle() { }
        public Triangle(Vector3 a, Vector3 b, Vector3 c)
        {
            pointA = a;
            pointB = b;
            pointC = c;

            AB = new Line(a, b);
            BC = new Line(b, c);
            CA = new Line(c, a);

            lines = new Line[3];
            lines[0] = AB;
            lines[1] = BC;
            lines[2] = CA;

            numPointsInCircle = 0;
            circumCircle = new Circle();

            circumCircle = CalculateCircumcircle(this);
        }

        public Vector3 pointA;
        public Vector3 pointB;
        public Vector3 pointC;

        public Line AB;
        public Line BC;
        public Line CA;

        public Line[] lines;

        public Circle circumCircle;
        public int numPointsInCircle;
    }


    public class Circle
    {
        public Circle()
        {
            center = Vector3.zero;
            radius = 1.0f;
        }
        public Circle(Vector3 position, float scale)
        {
            center = position;
            radius = scale;
        }

        public Vector3 center;
        public float radius;
    }

    public static Vector3 CalculateMidpoint(Line line)
    {
        float x = (line.end.x + line.start.x) / 2.0f;
        float y = 0.0f;
        float z = (line.end.z + line.start.z) / 2.0f;

        Vector3 midpoint = new Vector3(x, y, z);

        return midpoint;
    }

    public static Vector3 FindIntersection(Line a, Line b)
    {
        float a1 = a.end.z - a.start.z;
        float b1 = a.start.x - a.end.x;
        float c1 = a1 * a.start.x + b1 * a.start.z;

        float a2 = b.end.z - b.start.z;
        float b2 = b.start.x - b.end.x;
        float c2 = a2 * b.start.x + b2 * b.start.z;

        float det = a1 * b2 - a2 * b1;

        if (det == 0)
        {
            return Vector3.up;
        }
        else
        {
            float x = (b2 * c1 - b1 * c2) / det;
            float z = (a1 * c2 - a2 * c1) / det;
            return new Vector3(x, 0, z);
        }
    }
    public static Circle CalculateCircumcircle(Triangle triangle)
    {
        Vector3[] midpoints = new Vector3[3];
        Vector3[] bisectors = new Vector3[3];

        Line[] edges = new Line[3];

        for (int i = 0; i < 3; i++)
        {
            midpoints[i] = CalculateMidpoint(triangle.lines[i]);

            Vector3 temp = midpoints[i] - triangle.lines[i].start;

            bisectors[i].x = -temp.z;
            bisectors[i].y = 0.0f;
            bisectors[i].z = temp.x;
        }

        edges[0] = new Line(midpoints[0], bisectors[0] + midpoints[0]);
        edges[1] = new Line(midpoints[1], bisectors[1] + midpoints[1]);
        edges[2] = new Line(midpoints[2], bisectors[2] + midpoints[2]);

        Vector3[] intersectionPoints = new Vector3[3];
        intersectionPoints[0] = FindIntersection(edges[0], edges[1]);
        intersectionPoints[1] = FindIntersection(edges[1], edges[2]);
        intersectionPoints[2] = FindIntersection(edges[2], edges[0]);

        bool isCenter = (intersectionPoints[0] == intersectionPoints[1] && intersectionPoints[1] == intersectionPoints[2]);

        Vector3 circlePosition = intersectionPoints[0];
        float distanceA = Vector3.Distance(triangle.pointA, circlePosition);
        float distanceB = Vector3.Distance(triangle.pointB, circlePosition);
        float distanceC = Vector3.Distance(triangle.pointC, circlePosition);

        float maxDistance = Mathf.Max(distanceA, distanceB, distanceC);
        return new Circle(circlePosition, maxDistance);
    }

    public static bool PointIsInsideCircle(Circle circle, Vector3 point)
    {
        float distance = Vector3.Distance(point, circle.center);

        return distance <= circle.radius;
    }

    public static float CalculateAngleInDegs(Vector3 start, Vector3 pivot, Vector3 end)
    {
        float deltaABx = pivot.x - start.x;
        float deltaBCx = end.x - pivot.x;
        float deltaABy = pivot.z - start.z;
        float deltaBCy = end.z - pivot.z;

        float slopeAB = Mathf.Sqrt((deltaABx * deltaABx) + (deltaABy * deltaABy));
        float slopeBC = Mathf.Sqrt((deltaBCx * deltaBCx) + (deltaBCy * deltaBCy));

        float x = (deltaABx * deltaBCx + deltaABy * deltaBCy);
        float product = slopeAB * slopeBC;

        float y = Mathf.Acos(x / product);

        return y * kRadToDeg;
    }

    public static Vector3 FindUncommonPoint(Triangle a, Triangle b)
    {
        List<Vector3> aPoints = new List<Vector3>();
        aPoints.Add(a.pointA);
        aPoints.Add(a.pointB);
        aPoints.Add(a.pointC);

        for (int i = 0; i < 3; i++)
        {
            if (aPoints[i] != b.pointA && aPoints[i] != b.pointB && aPoints[i] != b.pointC)
            {
                return aPoints[i];
            }
        }
        return new Vector3(0, 0, 0);
    }

    public static List<Vector3> FindPointsFormingAngle(Triangle a, Vector3 pivotPoint)
    {
        List<Vector3> pointsInOrder = new List<Vector3>();

        if (a.pointA == pivotPoint)
        {
            pointsInOrder.Add(a.pointB);
            pointsInOrder.Add(a.pointA);
            pointsInOrder.Add(a.pointC);
        }
        else if (a.pointB == pivotPoint)
        {
            pointsInOrder.Add(a.pointA);
            pointsInOrder.Add(a.pointB);
            pointsInOrder.Add(a.pointC);
        }
        else if (a.pointC == pivotPoint)
        {
            pointsInOrder.Add(a.pointA);
            pointsInOrder.Add(a.pointC);
            pointsInOrder.Add(a.pointB);
        }

        return pointsInOrder;
    }

}
