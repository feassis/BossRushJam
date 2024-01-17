using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float dmg;
    private float speed;
    private Vector3 movementDirection = Vector3.zero;

    private GameObject owner;

    public void Setup(float dmg, float speed, Vector3 directon, GameObject owner)
    {
        this.dmg = dmg;
        this.speed = speed;
        this.movementDirection = directon;
        this.owner = owner;

        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(10);

        Destroy(gameObject);
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
        other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable);
        if (damageable != null)
        {
            ApplyBulletDamage(damageable, other.gameObject);
            Destroy(gameObject);
        }
    }

    protected virtual void ApplyBulletDamage(IDamageable damageable, GameObject hitedObj)
    {
        damageable.TakeDamage(dmg);
    }
}
