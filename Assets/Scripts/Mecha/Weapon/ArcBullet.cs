using UnityEngine;

public class ArcBullet: MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    protected float dmg;
    protected GameObject owner;
    
    public void Setup(float damage, Vector3 initialVelocity, GameObject owner)
    {
        dmg = damage;
        rb.velocity = initialVelocity;
        this.owner = owner;
    }
}
