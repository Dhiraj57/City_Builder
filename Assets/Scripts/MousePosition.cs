using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{  
    private static MousePosition instance;
    public static MousePosition Instance { get { return instance; } }

    private Camera mainCamera;
    [SerializeField] private LayerMask layer;
    [SerializeField] private LayerMask objectLayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        GetWorldMousePosition();
    }

    public Vector3 GetWorldMousePosition()
    {
        mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layer))
        {
            transform.position = raycastHit.point;
            return raycastHit.point;
        }
        else
            return Vector3.zero;
    }

    public bool CheckCollision()
    {
        mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, objectLayer))
        {
            return false;        
        }
        else
            return true;
    }

    public void Remove()
    {
        mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, objectLayer))
        {
            Destroy(raycastHit.collider.gameObject.transform.parent.gameObject);
        }
    }
}
