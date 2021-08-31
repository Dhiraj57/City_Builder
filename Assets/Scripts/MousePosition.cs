using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{  
    private static MousePosition instance;
    public static MousePosition Instance { get { return instance; } }

    [SerializeField] private LayerMask layer;
    [SerializeField] private LayerMask objectLayer;

    private Camera mainCamera;

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
        {
            return Vector3.zero;
        }       
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
        {
            return true;
        }         
    }

    public void Remove()
    {
        mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, objectLayer))
        {
            SoundManager.Instance.Play(SoundManager.Sounds.Remove);
            Destroy(raycastHit.collider.gameObject.transform.parent.gameObject);
        }
    }
}
