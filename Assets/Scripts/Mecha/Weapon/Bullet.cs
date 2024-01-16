using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float dmg;
    private float speed;
    private Vector3 movementDirection = Vector3.zero;
    private DamageType damageType;

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
            ApplyBulletDamage(damageable);
            Destroy(gameObject);
        }
    }

    protected virtual void ApplyBulletDamage(IDamageable damageable)
    {
        damageable.TakeDamage(dmg, DamageType.NONE);
    }
}
