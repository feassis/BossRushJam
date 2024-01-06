using UnityEngine;

public class ArcBullet: MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private float dmg;
    
    public void Setup(float damage, Vector3 initialVelocity)
    {
        dmg = damage;
        rb.velocity = initialVelocity;
    }
}