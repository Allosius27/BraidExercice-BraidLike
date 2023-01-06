using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Vector3 velocity;

    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _scale)
    {
        position = _position;
        rotation = _rotation;
        scale = _scale;
    }

    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _scale, Vector3 _velocity)
    {
        position = _position;
        rotation = _rotation;
        scale = _scale;
        velocity = _velocity;
    }
}
