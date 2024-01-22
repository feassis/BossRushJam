using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float dmg;
    protected float speed;
    private Vector3 movementDirection = Vector3.zero;
    private DamageType damageType;

    private GameObject owner;

    public void Setup(float dmg, float speed, Vector3 directon, GameObject owner)
    {
        this.dmg = dmg;
        this.speed = speed;
        this.movementDirection = directon;
        this.owner = owner;

        StartCoroutine(Lifetime());
    }

    protected IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(10);

        Destroy(gameObject);
    }

    protected virtual void Update()
    {
        transform.position += movementDirection.normalized * speed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
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
        damageable.TakeDamage(dmg, DamageType.NONE);
    }
}
