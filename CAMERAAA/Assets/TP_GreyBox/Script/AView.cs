using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AView : MonoBehaviour
{
    [SerializeField]CameraController camCtrl;
    
    public bool isActiveOnStart;

    public float weight;

    public virtual CameraConfig GetConfiguration() { return new CameraConfig(); }

    public void SetActive(bool isActive) 
    {
        camCtrl.AddView(this);
    }

    void Start()
    {
        if (isActiveOnStart) SetActive(true);
    }
    public void OnDrawGizmos()
    {
        GetConfiguration().DrawGizmos(new Color(1, 0.75f, 0.8f));
    }
}
