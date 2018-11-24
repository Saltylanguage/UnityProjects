using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleComparer : IComparer<Vector3>
{
    Vector3 mReferencePoint;
    
    public AngleComparer(Vector3 referencePoint)
    {
        mReferencePoint = referencePoint;
    }

    public int Compare(Vector3 a, Vector3 b)
    {
        var first = Vector3.Normalize(a - mReferencePoint);
        var second = Vector3.Normalize(b - mReferencePoint);
        return -Vector3.Dot(first, Vector3.right).CompareTo(Vector3.Dot(second, Vector3.right));
    }
}
