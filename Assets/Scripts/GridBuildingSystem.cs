using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private ObjectHandler objectHandler;
    [SerializeField] private List<ObjectTypeSO> spawnObjectList;
    [SerializeField] private List<GameObject> showObjectList;

    private ObjectTypeSO.Dir dir;
    private ObjectTypeSO spawnObject;
    private GameObject showObject;

    private GridSystem grid;

    private Vector3 mousePosition;
    private Vector3 offsetPosition;
    private Vector2Int spawnPosition;

    private int gridWidth;
    private int gridHeight;

    private float cellSize;

    private void Awake()
    {
        gridWidth = 10;
        gridHeight = 10;
        cellSize = 10f;
        offsetPosition = new Vector3(-33, 0, -66);

        grid = new GridSystem(gridWidth, gridHeight, cellSize, offsetPosition);

        dir = ObjectTypeSO.Dir.Right;
        spawnObject = spawnObjectList[0];
    }

    private void Update()
    {
        SetObjectRotation();
        SpawnObject();
        ShowObject();
        RemoveObject();
    }

    private void ShowObject()
    {
        if ((MousePosition.Instance.GetWorldMousePosition() == Vector3.zero))
        {
            objectHandler.isVisible = false;
            if(showObject != null) { Destroy(showObject); }
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
                    objectHandler.AddObject(spawnPosition.x - rotationOffset.x, spawnPosition.y - rotationOffset.y);
                    SoundManager.Instance.Play(SoundManager.Sounds.Spawn);
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
}
