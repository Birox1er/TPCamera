using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereViewVolume : AViewVolume
{
    public GameObject target;
    public float innerRadius;
    public float outerRadius;
    private float _distance;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(.5f, .12f, .9f);
        Gizmos.DrawWireSphere(transform.position, innerRadius); 
        Gizmos.color = new Color(.9f, .12f, .5f);
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
    private void Start()
    {
        if (innerRadius > outerRadius)
        {
            innerRadius = outerRadius;
        }
    }
    private void Update()
    {
        _distance = Vector3.Distance(target.transform.position, transform.position);
        if (_distance <= outerRadius&&!IsActive)
        {
            SetActive(true);
        }
        else if (_distance > outerRadius && IsActive)
        {
            SetActive(false);
        }
    }
    public override float ComputeSelfWeight()
    {
        if (_distance < innerRadius)
        {
            return 1;
        }
        else
        {
            return Mathf.Lerp(0, 1,1-( _distance-innerRadius) / (outerRadius-innerRadius));
        }
    }
}
