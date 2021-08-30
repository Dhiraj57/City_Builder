using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ObjectTypeSO : ScriptableObject
{
    public string nameString;
    public GameObject prefab;
    public Transform visual;
    public int width;
    public int height;
}
