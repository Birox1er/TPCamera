using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFollowView : AView
{
    public float yaw; 
    public float yawSpeed;
    public float[] pitch =new float[3];
    public float[] roll= new float[3];
    public float[] fov=new float[3];
    public GameObject target;
    public Curve curve;
    public float curvePosition;
    public float curveSpeed;
    // Start is called before the first frame update
    
    public override CameraConfig GetConfiguration()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        curvePosition += Input.GetAxisRaw("Vertical2")*curveSpeed;
        if (curvePosition > 1)
        {
            curvePosition = 1;
        }
        if (curvePosition <0)
        {
            curvePosition = 0;
        }
        yaw += Input.GetAxisRaw("Horizontal2")*yawSpeed;
        int i = 0;
        if (curvePosition < 0.9 && curvePosition > 0.2)
        {
            i = 1;
        }
        if (curvePosition > 0.9)
        {
            i = 2;
        }
        Vector3 pivot = curve.GetPosition(curvePosition, transform.localToWorldMatrix) ;
        float distance = 0;
        return new CameraConfig(yaw, pitch[i], roll[i], pivot, distance, fov[i]);
    }
    private void Update()
    {
    }
}
