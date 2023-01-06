using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonstarPointInTime : PointInTime
{
    public bool objIsActive;
    public float lifeTimer;

    public MonstarPointInTime(Vector3 _position, Quaternion _rotation, Vector3 _scale, bool _objIsActive, float _lifeTimer) : base(_position, _rotation, _scale)
    {
        position = _position;
        rotation = _rotation;
        scale = _scale;

        objIsActive = _objIsActive;
        lifeTimer = _lifeTimer;
    }

    public MonstarPointInTime(Vector3 _position, Quaternion _rotation, Vector3 _scale, Vector3 _velocity, bool _objIsActive, float _lifeTimer) : base(_position, _rotation, _scale, _velocity)
    {
        position = _position;
        rotation = _rotation;
        scale = _scale;
        velocity = _velocity;

        objIsActive = _objIsActive;
        lifeTimer = _lifeTimer;
    }
}
