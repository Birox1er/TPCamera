using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFollowView : AView
{
    public float roll;
    public float fov;
    public GameObject target;
    public GameObject centralPoint;
    public float yawOffsetMax;
    public float pitchOffsetMax;
    private float _yaw;
    private float _pitch;
    // Start is called before the first frame update
    public override CameraConfig GetConfiguration()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        Vector3 centralDir = (centralPoint.transform.position - transform.position).normalized;
        _yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
         float centralYaw = Mathf.Atan2(centralDir.x, centralDir.z) * Mathf.Rad2Deg;
        _pitch = -Mathf.Asin(dir.y) * Mathf.Rad2Deg;
        float centralPitch = -Mathf.Asin(centralDir.y) * Mathf.Rad2Deg;
        float yawDif = _yaw - centralYaw;
        while (yawDif > 180)
        {
            yawDif -= 360;
        }
        while (yawDif < -180)
        {
            yawDif += 360;
        }
        if (Mathf.Abs(yawDif) > yawOffsetMax)
        {
            if (yawDif < 0)
            {
                _yaw = centralYaw - yawOffsetMax;
            }
            else
            {
                _yaw = centralYaw + yawOffsetMax;
            }
        }
        if (Mathf.Abs(_pitch - centralPitch) > pitchOffsetMax)
        {
            if (_pitch - centralPitch < 0)
            {
                _pitch = (centralPitch - pitchOffsetMax);
            }
            else
            {
                _pitch = (centralPitch + pitchOffsetMax);
            }
        }
        Vector3 pivot = transform.position;
        float distance = 0;
        return new CameraConfig(_yaw, _pitch, roll, pivot, distance, fov);
    }
}
