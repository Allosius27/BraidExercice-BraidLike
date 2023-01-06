using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    #region Fields

    private bool _isRewinding;


    #endregion

    #region Properties

    public List<PointInTime> pointsInTime { get; protected set; }

    public Rigidbody2D rb { get; protected set; }

    public bool IsRewinding => _isRewinding;

    #endregion

    #region UnityInspector

    #endregion

    #region Behaviour

    public virtual void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody2D>();

        GameCore.Instance.AddTimeBody(this);
    }

    public virtual void FixedUpdate()
    {
        if(GameCore.Instance == null)
        {
            return;
        }

        if(_isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    public virtual void Rewind()
    {
        if(pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            transform.localScale = pointInTime.scale;
            if(rb != null)
            {
                rb.velocity = pointInTime.velocity;
            }
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public virtual void Record()
    {
        if(pointsInTime.Count > Mathf.Round(GameCore.Instance.MaxRecordTime * (1f / Time.fixedDeltaTime)))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        if(rb != null)
        {
            pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, transform.localScale, rb.velocity));
        }
        else
        {
            pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, transform.localScale));
        }
       
    }

    public virtual void StartRewind()
    {
        _isRewinding = true;

        if(rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public virtual void StopRewind()
    {
        _isRewinding = false;

        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }

    #endregion
}
