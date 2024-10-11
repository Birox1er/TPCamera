using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AViewVolume : MonoBehaviour
{

    public int priority = 0;
    public AView view;
    public bool isCutOnSwitch;

    public int uid {  get; private set; }

    static int nextUid = 0;

    protected bool IsActive {  get; private set; }

    public virtual float ComputeSelfWeight() { return 1.0f; }

    void Awake()
    {
        uid = nextUid;
        nextUid++;
    }

    protected void SetActive(bool isActive) 
    {
        ViewVolumeBlender.Instance.Refresh();
        CameraController.Instance.Cut(isCutOnSwitch);
        if (isActive) { ViewVolumeBlender.Instance.AddVolume(this); }
        else { ViewVolumeBlender.Instance.RemoveVolume(this); }
        IsActive = isActive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
