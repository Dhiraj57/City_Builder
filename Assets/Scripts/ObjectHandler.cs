using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHandler : MonoBehaviour
{
    private List<Transform> objectTransformList;
    [HideInInspector] public int objectIndex;

    private int listSize;
    private bool isUIvisible;

    [HideInInspector] public bool isSpawn;
    [HideInInspector] public bool isAddObject;
    [HideInInspector] public bool isRemoveObject;
    [HideInInspector] public bool isVisible;

    [SerializeField] private GameObject infoText;
    [SerializeField] private GameObject infoText2;

    private void Awake()
    {
        objectTransformList = new List<Transform>() ;
        listSize = 0;
        objectIndex = 0;
        isSpawn = false;
        isVisible = false;
        isRemoveObject = false;
        isUIvisible = false;

        StartCoroutine(CloseInstructions());
    }

    public void AddObject(int x, int z)
    {
        objectTransformList.Add(transform);
        objectTransformList[listSize].position = new Vector3(x, 0, z);      
        listSize++;
    }

    public void RemoveObject()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        objectIndex = 5;
        infoText.SetActive(true);
        isSpawn = true;
        isRemoveObject = true;
        isAddObject = false;
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
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        objectIndex = Id;
        isSpawn = true;
        isAddObject = true;
        isRemoveObject = false;
    }

    public void ViewMode()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
        isSpawn = false;
        isAddObject = false;
        isRemoveObject = false;

        if(isUIvisible)
        {
            isUIvisible = false;
            infoText.SetActive(false);
        }
        else
        {
            isUIvisible = true;
            infoText.SetActive(true);
        }
    }

    private IEnumerator CloseInstructions()
    {
        yield return new WaitForSeconds(6);

        infoText2.SetActive(false);

        yield break;
    }

}
