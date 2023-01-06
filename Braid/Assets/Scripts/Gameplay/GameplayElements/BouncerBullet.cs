using AllosiusDevCore.Controller2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerBullet : MonoBehaviour
{
    #region Fields

    private float _timer;

    #endregion

    #region Properties

    public Canon canon { get; set; }

    public Collider2D Col => _col;

    public Rigidbody2D Rb => _rb;

    public float Timer => _timer;

    #endregion

    #region UnityInspector

    [SerializeField] private float _timeLife = 5f;

    [SerializeField] private float _explosionForce = 50;

    [SerializeField] private Collider2D _col;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private TimeBody _timeBody;

    #endregion

    #region Behaviour

    private void Update()
    {
        if(_timeBody != null && _timeBody.IsRewinding)
        {
            return;
        }

        if(gameObject.activeSelf)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeLife)
            {
                _timer = 0;
                gameObject.SetActive(false);
                transform.position = canon.SpawnMonstarPoint.position;
                canon.Shoot();
            }
        }
    }

    public void SetTimer(float newValue)
    {
        _timer = newValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPlayerController controller))
        {
            var dir = collision.transform.position - transform.position;
            controller.AddForce(dir.normalized * _explosionForce, PlayerForce.Decay);
        }
    }

    #endregion
}
