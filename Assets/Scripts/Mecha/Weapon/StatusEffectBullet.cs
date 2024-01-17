using UnityEngine;

public class StatusEffectBullet : Bullet
{
    [SerializeField] private StatusEffect effect;
    [SerializeField] private float duration = 15f;
    protected override void ApplyBulletDamage(IDamageable damageable, GameObject hitedObj)
    {
        base.ApplyBulletDamage(damageable, hitedObj);

        if(hitedObj.TryGetComponent<MechaStats>(out MechaStats stats))
        {
            stats.AddStatusEffectWithLifetime(effect, duration);
        }
    }
}