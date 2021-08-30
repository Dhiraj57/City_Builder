using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHandler : MonoBehaviour
{
    private List<Transform> objectTransformList;

    [HideInInspector] public int objectIndex;
    private int listSize;
    [HideInInspector] public bool isSpawn;
    [HideInInspector] public bool isAddObject;
    [HideInInspector] public bool isRemoveObject;
    [HideInInspector] public bool isVisible;
    [SerializeField] private GameObject infoText;

    private void Awake()
    {
        objectTransformList = new List<Transform>() ;
        listSize = 0;
        objectIndex = 0;
        isSpawn = false;
        isVisible = false;
        isRemoveObject = false;
    }

    public void AddObject(int x, int z)
    {
        objectTransformList.Add(transform);
        objectTransformList[listSize].position = new Vector3(x, 0, z);      
        //Debug.Log(objectTransformList[listSize].position);
        listSize++;
    }

    public void RemoveObject()
    {
        objectIndex = 5;
        isSpawn = true;
        infoText.SetActive(true);
        isRemoveObject = true;
        isAddObject = false;

        //listSize--;

        for (int i = 0; i < listSize; i++)
        {
            //Debug.Log(objectTransformList[i].position);
        }
    }

    public bool CheckGridPosition(Vector3 position)
    {
        for ( int i=0; i < listSize; i++ )
        {
            if (position == objectTransformList[i].position)
            {
                return false;
            }
        }
        return true;
    }

    public void ObjectIndexSelector(int Id)
    {
        objectIndex = Id;
        isSpawn = true;
        isAddObject = true;
        infoText.SetActive(true);
        isRemoveObject = false;
    }

    public void ViewMode()
    {
        isSpawn = false;
        isAddObject = false;
        infoText.SetActive(false);
        isRemoveObject = false;
    }

}
