using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  单独储存 position
///  原 transform.position 代表 displayPosition
/// </summary>
public abstract class Object2D : MonoBehaviour
{
    private Vector3 _worldPosition;

    public Vector3 WorldPosition
    {
        get => _worldPosition;
        set
        {
            _worldPosition = value;
            transform.position = new Vector3(value.x, value.y + value.z, value.z);
        }
    }

    public Vector3 PlanePosition => new(_worldPosition.x, _worldPosition.y, 0);
}