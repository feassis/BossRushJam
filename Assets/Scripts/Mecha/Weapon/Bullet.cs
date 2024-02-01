using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceShooting;
    [SerializeField] private AudioSource audioSourceHited;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private GameObject trail;

    bool hitedSomething = false;

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

        audioSourceShooting.Play();

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

        if (hitedSomething)
        {
            return;
        }

        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Floor")
        {
            StartCoroutine(HitedSomething());
        }

        other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable);
        if (damageable != null)
        {
            ApplyBulletDamage(damageable, other.gameObject);
            StartCoroutine(HitedSomething());
        }
    }

    private IEnumerator HitedSomething()
    {
        hitedSomething = true;
        audioSourceHited.Play();
        mesh.enabled = false;
        trail.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    protected virtual void ApplyBulletDamage(IDamageable damageable, GameObject hitedObj)
    {
        damageable.TakeDamage(dmg, DamageType.NONE);
    }
}
