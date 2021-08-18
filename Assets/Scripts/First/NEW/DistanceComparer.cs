using System.Collections;
using System;
using UnityEngine;

public class DistanceComparer //IComparer
{
    private Transform compareTransform;
    public DistanceComparer(Transform compTransform)
    {
        compareTransform =compTransform;
    }
    public int Comparer(object x, object y)
    {
        Collider xCollider = x as Collider;
        Collider yCollider = y as Collider;
        
        Vector3 offset = xCollider.transform.position - compareTransform.transform.position;
        float xDistance = offset.magnitude;

        offset = yCollider.transform.position - compareTransform.position;
        float yDistance = offset.sqrMagnitude;

        return xDistance.CompareTo(yDistance);
    }
}
