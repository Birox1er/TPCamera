using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

static public class MathUtils
{

    static public Vector3 GetNearestPointOnSegment(Vector3 a, Vector3 b, Vector3 target)
    {
        Vector3 AB = b - a;
        Vector3 AC = target - a;
        float scal = Vector3.Dot(AC, AB.normalized);
        scal = Mathf.Clamp(scal, 0, Vector3.Distance(a, b));
        Vector3 closestPos = a + (AB.normalized * scal);
        return closestPos;
    }
    static public Vector3 LinearBezier(Vector3 A, Vector3 B, float t)
    {
        Vector3 result=(1-t)*A+t*B;
        return result;
    }
    static public Vector3 QuadraticBezier(Vector3 A, Vector3 B, Vector3 C, float t) 
    {
        Vector3 result = (1-t)*LinearBezier(A,B,t)+t*LinearBezier(B,C,t);
        return result;
    }
    static public Vector3 CubicBezier(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t) 
    {
        Vector3 result = (1-t)*QuadraticBezier(A,B,C,t)+t*QuadraticBezier(B,C,D,t);
        return result;
    }


}
