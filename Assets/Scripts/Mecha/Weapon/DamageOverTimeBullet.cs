using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeBullet : Bullet
{
    [SerializeField] private float damageFraction = 0.3333f ;
    [SerializeField] private float duration = 5f;
    [SerializeField] private int numberOfTicks = 10;
    protected override void ApplyBulletDamage(IDamageable damageable)
    {
        damageable.TakeDamageOverTime(dmg * damageFraction, duration, numberOfTicks);
    }
}
