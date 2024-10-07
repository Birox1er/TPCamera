using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Curve : MonoBehaviour
{
    public Vector3 A, B, C, D;
   // Start is called before the first frame update
   public Vector3 GetPosition(float t)
    {
        return MathUtils.CubicBezier(A, B, C, D, t);
    }
    public Vector3 GetPosition(float t, Matrix4x4 localToWorldMatrix) 
    {
        Vector3 result = MathUtils.CubicBezier(A, B, C, D, t);
        return localToWorldMatrix.MultiplyPoint(result);
    }
    void DrawGizmo(Color c, Matrix4x4 localToWorldMatrix)
    {
        Gizmos.color = c;
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(A), 0.15f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(B), 0.15f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(C), 0.15f);
        Gizmos.DrawSphere(localToWorldMatrix.MultiplyPoint(D), 0.15f);
        for (float f = 0f; f < 1; f += 0.001f)
        {
            Gizmos.color = new Color(5f+.25f*f, .5f-.45f*f, 1-0.75f);
            Gizmos.DrawSphere(GetPosition(f,localToWorldMatrix), 0.1f);
        }

    }
    private void OnDrawGizmos()
  {
        DrawGizmo(Color.red, transform.localToWorldMatrix);
   }
}
