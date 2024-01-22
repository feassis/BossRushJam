using UnityEngine;

public class LeftHAndOfCorruptionBullet : HomingBullet
{
    [SerializeField] private float duration = 10f;
    protected override void ApplyBulletDamage(IDamageable damageable, GameObject hitedObj)
    {
        if (hitedObj.TryGetComponent<MechaStats>(out MechaStats targetStats))
        {
            targetStats.AddStatusEffectWithLifetime(StatusEffect.Wis90, duration);
        }

        base.ApplyBulletDamage(damageable, hitedObj);
    }
}
