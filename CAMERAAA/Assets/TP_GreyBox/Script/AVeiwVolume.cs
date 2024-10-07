using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AVeiwVolume : MonoBehaviour
{

    public int priority = 0;
    public AView view;

    int uid;

    static int nextUid = 0;

    public virtual float ComputeSelfWeight() { return 1.0f; }

    void Awake()
    {
        uid = nextUid;
        nextUid++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
