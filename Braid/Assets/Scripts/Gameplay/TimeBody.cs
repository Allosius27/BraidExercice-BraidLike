using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    #region Fields

    private bool _isRewinding;

    private List<PointInTime> _pointsInTime;

    private Rigidbody2D _rb;

    #endregion

    #region Properties

    public bool IsRewinding => _isRewinding;

    #endregion

    #region UnityInspector

    #endregion

    #region Behaviour

    private void Start()
    {
        _pointsInTime = new List<PointInTime>();
        _rb = GetComponent<Rigidbody2D>();

        GameCore.Instance.AddTimeBody(this);
    }

    private void FixedUpdate()
    {
        if(_isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Rewind()
    {
        if(_pointsInTime.Count > 0)
        {
            PointInTime pointInTime = _pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            _pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    private void Record()
    {
        if(_pointsInTime.Count > Mathf.Round(GameCore.Instance.MaxRecordTime * (1f / Time.fixedDeltaTime)))
        {
            _pointsInTime.RemoveAt(_pointsInTime.Count - 1);
        }

        _pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        _isRewinding = true;

        if(_rb != null)
        {
            _rb.isKinematic = true;
        }
    }

    public void StopRewind()
    {
        _isRewinding = false;

        if (_rb != null)
        {
            _rb.isKinematic = false;
        }
    }

    #endregion
}
