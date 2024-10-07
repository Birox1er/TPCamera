using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Rail : MonoBehaviour
{
    public bool isLoop = false;

    List<GameObject> children = new List<GameObject>();

    [SerializeField] private float length = 0;
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
        if(children.Count > 0)
        {
            Gizmos.color = new Color(0, 0.66f, 0.42f);
            for (int i = 0; i < children.Count; i++)
            {
                if (i > 0)
                {
                    Gizmos.DrawLine(children[i - 1].transform.position, children[i].transform.position);
                }
            }
            if (isLoop)
            {
                Gizmos.DrawLine(children[children.Count - 1].transform.position, children[0].transform.position);
            }
        }
        
    }

    public float GetLength() { return length;}

    public Vector3 GetPosition(float distance)
    {
        float distanceWithinBornes = 0;
        
        Vector3 position = children[0].transform.position;
        if (isLoop)
        {
            distanceWithinBornes = distance % length;
        }
        else
        {
            distanceWithinBornes = Mathf.Clamp(distance, 0, length);
        }
        float currentDist = 0;
        Vector3 nextPoint = Vector3.zero;
        float percent = 0;
        for (int i = 0; i < children.Count; i++)
        {
            
            if (i == children.Count - 1)
            {
                if (isLoop)
                {
                    nextPoint = children[0].transform.position;
                }
                else
                {
                    return children[children.Count - 1].transform.position;
                }
            }
            else
            {
                nextPoint = children[i + 1].transform.position;
            }

            if (distanceWithinBornes >= currentDist && distanceWithinBornes < currentDist + Vector3.Distance(children[i].transform.position, nextPoint))
            {
                percent = (distanceWithinBornes - currentDist) / Vector3.Distance(children[i].transform.position, nextPoint);
                
                position = Vector3.Lerp(children[i].transform.position, nextPoint, percent);
                break;
            }
            else
            {

                currentDist += Vector3.Distance(children[i].transform.position, nextPoint);
            }

        }
        
        return position;
    }

}
