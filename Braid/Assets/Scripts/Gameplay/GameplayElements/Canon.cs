using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    #region Fields

    private Collider2D _col;

    #endregion

    #region Properties

    public Transform SpawnMonstarPoint => _spawnMonstarPoint;

    #endregion

    #region UnityInspector

    [SerializeField] private Transform _spawnMonstarPoint;

    [SerializeField] private BouncerBullet _bouncerBullet;

    [SerializeField] private Vector2 _shootDir;
    [SerializeField] private float _shootForce;

    #endregion

    #region Behaviour

    private void Start()
    {
        _col = GetComponent<Collider2D>();

        _bouncerBullet.canon = this;
        Physics2D.IgnoreCollision(_bouncerBullet.Col, _col);
        _bouncerBullet.gameObject.SetActive(false);
        Shoot();
    }

    public void Shoot()
    {
        _bouncerBullet.gameObject.SetActive(true);
        _bouncerBullet.Rb.AddForce(_shootDir * _shootForce);
    }
    #endregion
}
