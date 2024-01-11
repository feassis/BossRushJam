using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float _timer = 2f;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPoint;
    private float _bulletTime;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    void Update()
    {
        transform.LookAt(_target);
        _agent.SetDestination(_target.position);
        float distance = Vector3.Distance(_target.transform.position, transform.position);
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
        bulletRig.AddForce(bulletRig.transform.forward * _agent.speed * 800);
        Destroy(bulletEnemy, 10f);
    }
}
