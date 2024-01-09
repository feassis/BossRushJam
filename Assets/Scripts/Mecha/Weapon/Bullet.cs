using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float dmg;
    private float speed;
    private Vector3 movementDirection = Vector3.zero;

    private GameObject owner;

    public void Setup(float dmg, float speed, Vector3 directon, GameObject owner)
    {
        this.dmg = dmg;
        this.speed = speed;
        this.movementDirection = directon;
        this.owner = owner;
    }

    private void Update()
    {
        transform.position += movementDirection.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == owner)
        {
            return;
        }
        TryGetComponent<IDamageable>(out IDamageable damageable);
        if (damageable != null)
        {
            damageable.TakeDamage(dmg);
        }
    }
}
