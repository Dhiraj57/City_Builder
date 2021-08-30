using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    private List<Transform> objectTransformList;
    private int listSize;
    private void Awake()
    {
        objectTransformList = new List<Transform>() ;
        listSize = 0;
    }

    public void AddObject(int x, int z)
    {
        objectTransformList.Add(transform);
        objectTransformList[listSize].position = new Vector3(x, 0, z);
        listSize++;
    }

    public void RemoveObject()
    {
        listSize--;
    }

    public bool CheckGridPosition(Vector3 position)
    {
        Debug.Log(position);
        for ( int i=0; i < listSize; i++ )
        {
            if (position == objectTransformList[i].position)
            {          
                return false;
            }
        }
        return true;
    }
}
