using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private ObjectHandler objectHandler;
    [SerializeField] private List<ObjectTypeSO> spawnObjectList;
    [SerializeField] private List<GameObject> showObjectList;
    private List<GameObject> gameObjectList;
    private List<string> objectNameList;

    private int listSize;

    private ObjectTypeSO spawnObject;
    private GameObject showObject;

    private ObjectTypeSO.Dir dir = ObjectTypeSO.Dir.Right;
    private GridSystem<GridObject> grid;
    private Vector3 mousePosition;
    private Vector2Int spawnPosition;

    private void Awake()
    {
        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        grid = new GridSystem<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-33, 0, -66));
        spawnObject = spawnObjectList[0];
        listSize = 0;

        //isVisible = false;
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
        SetObjectRotation();
        //SelectObjectType();
        SpawnObject();
        ShowObject();
        RemoveObject();
    }

    private void ShowObject()
    {
        if ((MousePosition.Instance.GetWorldMousePosition() == Vector3.zero))
        {
            objectHandler.isVisible = false;
            if (showObject != null) { Destroy(showObject); }
        }
        else if(objectHandler.isSpawn)
        {
            objectHandler.isVisible = true;
        }      

        Vector2Int rotationOffset = spawnObject.GetRotationOffset(dir);

        mousePosition = MousePosition.Instance.GetWorldMousePosition();
        spawnPosition = grid.GetXZ(mousePosition, (int)mousePosition.x, (int)mousePosition.z) + rotationOffset;

        if (showObject == null && objectHandler.isVisible)
        {
            showObject = Instantiate(showObjectList[objectHandler.objectIndex], grid.GetSpawnPosition(spawnPosition.x, spawnPosition.y), Quaternion.Euler(0, spawnObject.GetRotationAngle(dir), 0));
        }

        if (showObject != null)
        {
            showObject.transform.position = grid.GetSpawnPosition(spawnPosition.x, spawnPosition.y);
            showObject.transform.rotation = Quaternion.Euler(0, spawnObject.GetRotationAngle(dir), 0);
        }           

        //if (objectHandler.CheckGridPosition(new Vector3(spawnPosition.x, 0, spawnPosition.y)))
        {
            //Instantiate(spawnObject.prefab, grid.GetSpawnPosition(spawnPosition.x, spawnPosition.y), Quaternion.identity);
            //objectHandler.AddObject(spawnPosition.x, spawnPosition.y);
        }
    }

    private void SpawnObject()
    {
        Vector2Int rotationOffset = spawnObject.GetRotationOffset(dir);

        mousePosition = MousePosition.Instance.GetWorldMousePosition();
        spawnPosition = grid.GetXZ(mousePosition, (int)mousePosition.x, (int)mousePosition.z) + rotationOffset;

        if(objectHandler.isAddObject)
        {
            if (Input.GetMouseButtonDown(0) && MousePosition.Instance.CheckCollision() && (MousePosition.Instance.GetWorldMousePosition() != Vector3.zero))
            {
                if (objectHandler.CheckGridPosition(new Vector3(spawnPosition.x - rotationOffset.x, 0, spawnPosition.y - rotationOffset.y)))
                {
                    Instantiate(spawnObjectList[objectHandler.objectIndex].prefab, grid.GetSpawnPosition(spawnPosition.x, spawnPosition.y), Quaternion.Euler(0, spawnObject.GetRotationAngle(dir), 0));
                    //listSize++;
                    //objectNameList.Add(gameObjectList[listSize].name);
                    objectHandler.AddObject(spawnPosition.x - rotationOffset.x, spawnPosition.y - rotationOffset.y);
                }
            }
        }      
    }

    private void SetObjectRotation()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            dir = spawnObject.GetNextDir(dir);
        }
    }

    private void RemoveObject()
    {
        if (Input.GetMouseButtonDown(0) && objectHandler.isRemoveObject)
        {
            MousePosition.Instance.Remove();
        }
    }

    /*private void SelectObjectType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        { 
            spawnObject = spawnObjectList[0];
            spawnIndex = 0;
            Destroy(showObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            spawnObject = spawnObjectList[1];
            spawnIndex = 1;
            Destroy(showObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) 
        { 
            spawnObject = spawnObjectList[2];
            spawnIndex = 2;
            Destroy(showObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) 
        { 
            spawnObject = spawnObjectList[3];
            spawnIndex = 3;
            Destroy(showObject);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) 
        { 
            spawnObject = spawnObjectList[4];
            spawnIndex = 4;
            Destroy(showObject);
        }
    }*/
}
