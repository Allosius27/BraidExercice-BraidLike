using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimPointInTime
{
    public Vector3 localPosition;
    public Quaternion localRotation;
    public Vector3 localScale;

    public string clipAnim;
    public float clipLength;
    public int clipDesiredFrame;

    public AnimPointInTime(Vector3 _localPosition, Quaternion _localRotation, Vector3 _localScale, string _clipAnim, float _clipLength, int _clipDesiredFrame)
    {
        localPosition = _localPosition;
        localRotation = _localRotation;
        localScale = _localScale;

        clipAnim = _clipAnim;
        clipLength = _clipLength;
        clipDesiredFrame = _clipDesiredFrame;
    }
}
