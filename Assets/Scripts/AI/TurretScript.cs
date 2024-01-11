using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private float _timer = 2f;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPoint;
    private float _bulletTime;

    void Update()
    {
        _spawnPoint.transform.LookAt(_target);
        ShootAtPlayer();
    }

    void ShootAtPlayer()
    {
        _bulletTime -= Time.deltaTime;

        if (_bulletTime > 0) return;

        _bulletTime = _timer;

        GameObject bulletEnemy = Instantiate(_bulletPrefab, _spawnPoint.transform.position,
            _spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletEnemy.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * 2200);
        Destroy(bulletEnemy, 10f);
    }
}
