using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredViewVolume : AViewVolume
    //MUST BE ATTACHED TO A COLLIDER OBJECT IN MODE 'TRIGGER'
{
    GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (other == target)
        {
            SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == target)
        {
            SetActive(false);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
