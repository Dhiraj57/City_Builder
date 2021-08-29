using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    private void Start()
    {
        Grid grid = new Grid(4, 2, 10f, Vector3.zero);
    }
}
