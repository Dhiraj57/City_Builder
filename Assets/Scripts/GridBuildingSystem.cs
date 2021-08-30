using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private ObjectTypeSO spawnObject;
    [SerializeField] private ObjectHandler objectHandler;

    private GridSystem<GridObject> grid;
    private Vector3 mousePosition;
    private Vector2Int spawnPosition;
    [SerializeField]private GameObject showObject;

    private void Awake()
    {
        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        grid = new GridSystem<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-33, 0, -66) /*, (GridSystem<GridObject> g, int x, int z) => new GridObject(g, x, z)*/);

        //Grid grid = new Grid(10, 10, 10f, Vector3.zero);

    }

    public class GridObject
    {
        private GridSystem<GridObject> grid;
        private int x;
        private int z;

        public GridObject(GridSystem<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }
    }

    private void Update()
    {
        SpawnObject();
        ShowObject();
    }

    private void ShowObject()
    {
        mousePosition = MousePosition.Instance.GetWorldMousePosition();
        spawnPosition = grid.GetXZ(mousePosition, (int)mousePosition.x, (int)mousePosition.z);

        showObject.transform.position = grid.GetSpawnPosition(spawnPosition.x, spawnPosition.y);
        //showObject.transform.position = new Vector3((int)mousePosition.x, (int)mousePosition.y, (int)mousePosition.z );

        if (objectHandler.CheckGridPosition(new Vector3(spawnPosition.x, 0, spawnPosition.y)))
        {
            //Instantiate(spawnObject.prefab, grid.GetSpawnPosition(spawnPosition.x, spawnPosition.y), Quaternion.identity);
            //objectHandler.AddObject(spawnPosition.x, spawnPosition.y);
        }
    }

    private void SpawnObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = MousePosition.Instance.GetWorldMousePosition();
            spawnPosition = grid.GetXZ(mousePosition, (int)mousePosition.x, (int)mousePosition.z);

            if (objectHandler.CheckGridPosition(new Vector3(spawnPosition.x, 0, spawnPosition.y)))
            {
                Instantiate(spawnObject.prefab, grid.GetSpawnPosition(spawnPosition.x, spawnPosition.y), Quaternion.identity);
                objectHandler.AddObject(spawnPosition.x, spawnPosition.y);
            }
        }
    }
}
