using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class FixedView : AView
{
    public float yaw;
    public float pitch;
    public float roll;
    public float fov;

    public override CameraConfig GetConfiguration()
    {
        Vector3 pivot = transform.position;
        float distance = 0;
        return new CameraConfig(yaw, pitch, roll, pivot, distance, fov);
    }
}
