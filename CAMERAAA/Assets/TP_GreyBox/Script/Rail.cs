using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public bool isLoop = false;

    List<GameObject> children = new List<GameObject>();

    private float length = 0;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).gameObject);

            //Calculate length
            if(i > 0)
            {
                length += Vector3.Distance(children[i - 1].transform.position, children[i].transform.position);
            }
            
        }
        if (isLoop)
        {
            length += Vector3.Distance(children[transform.childCount - 1].transform.position, children[0].transform.position);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0.66f, 0.42f);
        for(int i = 0;i < children.Count;i++)
        {
            if (i > 0)
            {
                Gizmos.DrawLine(children[i - 1].transform.position, children[i].transform.position);
            }
        }
        if(isLoop)
        {
            Gizmos.DrawLine(children[children.Count - 1].transform.position, children[0].transform.position);
        }
    }

    public float GetLength() { return length;}

    public Vector3 GetPosition(float distance)
    {
          
        
    }

}
