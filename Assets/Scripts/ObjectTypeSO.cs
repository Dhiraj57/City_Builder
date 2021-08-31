using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ObjectTypeSO : ScriptableObject
{
    public GameObject prefab;
    public Transform visual;
    public string nameString;
    public int width;
    public int height;  

    public enum Dir
    {
        Down,
        Left,
        Up,
        Right,
    }

    public Dir GetNextDir(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down: return Dir.Left;
            case Dir.Left: return Dir.Up;
            case Dir.Up: return Dir.Right;
            case Dir.Right: return Dir.Down;
        }
    }

    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down: return 0;
            case Dir.Left: return 90;
            case Dir.Up: return 180;
            case Dir.Right: return 270;
        }
    }

    public Vector2Int GetRotationOffset(Dir dir)
    {
        switch (dir)
        {
            default:

            case Dir.Down: return new Vector2Int(0, 0);
            case Dir.Left: return new Vector2Int(width, 0);
            case Dir.Up: return new Vector2Int(width, -height);
            case Dir.Right: return new Vector2Int(0, -height);
        }
    }
}
