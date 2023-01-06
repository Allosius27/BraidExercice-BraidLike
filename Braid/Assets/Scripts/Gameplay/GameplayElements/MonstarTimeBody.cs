using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstarTimeBody : TimeBody
{
    #region Fields

    BouncerBullet _bouncerBullet;

    #endregion

    public override void Start()
    {
        base.Start();

        _bouncerBullet = GetComponent<BouncerBullet>();
    }

    public override void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            MonstarPointInTime pointInTime = (MonstarPointInTime)pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            transform.localScale = pointInTime.scale;
            if (rb != null)
            {
                rb.velocity = pointInTime.velocity;
            }
            if(_bouncerBullet != null)
            {
                _bouncerBullet.SetTimer(pointInTime.lifeTimer);
                _bouncerBullet.gameObject.SetActive(pointInTime.objIsActive);
            }
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public override void Record()
    {
        if (pointsInTime.Count > Mathf.Round(GameCore.Instance.MaxRecordTime * (1f / Time.fixedDeltaTime)))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        if (rb != null)
        {
            pointsInTime.Insert(0, new MonstarPointInTime(transform.position, transform.rotation, transform.localScale, rb.velocity, _bouncerBullet.gameObject.activeSelf, _bouncerBullet.Timer));
        }
        else
        {
            pointsInTime.Insert(0, new MonstarPointInTime(transform.position, transform.rotation, transform.localScale, _bouncerBullet.gameObject.activeSelf, _bouncerBullet.Timer));
        }
    }

    public override void StopRewind()
    {
        base.StopRewind();

        if(_bouncerBullet != null && _bouncerBullet.Timer <= 0.01f)
        {
            _bouncerBullet.canon.Shoot();
        }
    }
}
