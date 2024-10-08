using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyView : AView
{
    public float roll;
    public float distance;
    public float fov;

    public GameObject target;

    public Rail _rail;
    public float _distanceOnRail;
    public float speed;

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
        float horizontalMovement = Input.GetAxis("Horizontal");
        _distanceOnRail += horizontalMovement * Time.fixedDeltaTime * speed;
        transform.position = _rail.GetPosition(_distanceOnRail);
    }
}
