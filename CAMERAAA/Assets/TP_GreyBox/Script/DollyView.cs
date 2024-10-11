using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class DollyView : AView
{
    public float roll;
    public float distance;
    public float fov;

    public GameObject target;

    public Rail _rail;
    public float _distanceOnRail;
    public float speed;

    public bool isAuto;

    Vector3 projection;
    public override CameraConfig GetConfiguration()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float _yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        float _pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        Vector3 pivot = transform.position;
        return new CameraConfig(_yaw, _pitch, roll, pivot, distance, fov);
    }

    private void FixedUpdate()
    {
        if (isAuto)
        {
            float smallestDist = -1;
            projection = _rail.children[0].transform.position;
            for (int i = 0; i < _rail.children.Count; i++)
            {

                if (i == _rail.children.Count - 1)
                {
                    if (_rail.isLoop)
                    {
                        Vector3 tempProjection = MathUtils.GetNearestPointOnSegment(_rail.children[i].transform.position, _rail.children[0].transform.position, target.transform.position);
                        
                        if (smallestDist == -1 || Vector3.Distance(target.transform.position, tempProjection) < smallestDist)
                        {
                            smallestDist = Vector3.Distance(target.transform.position, tempProjection);
                            projection = tempProjection;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Vector3 tempProjection = MathUtils.GetNearestPointOnSegment(_rail.children[i].transform.position, _rail.children[i + 1].transform.position, target.transform.position);
                    if (smallestDist == -1 || Vector3.Distance(target.transform.position, tempProjection) < smallestDist)
                    {
                        smallestDist = Vector3.Distance(target.transform.position, tempProjection);
                        projection = tempProjection;
                    }
                } 
            }
            transform.position = projection;


        }
        else
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            _distanceOnRail += horizontalMovement * Time.fixedDeltaTime * speed;
            transform.position = _rail.GetPosition(_distanceOnRail);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawLine(target.transform.position, projection);
    }

    
}
