using System.Collections.Generic;
using UnityEngine;

public class SandSlasherBultet : Bullet
{
    [SerializeField] private float targetRenualInterval = 0.5f;

    private List<HitedTargets> targets;

    private class HitedTargets
    {
        public IDamageable Damageable;
        public float MyTime;

        public HitedTargets(IDamageable damageable)
        {
            Damageable = damageable;
            MyTime = Time.time;
        }
    }

    protected override void Update()
    {
        base.Update();
    }


    protected override void ApplyBulletDamage(IDamageable damageable, GameObject hitedObj)
    {
        var alredyDameged = targets.Find(t => t.Damageable == damageable);

        if(alredyDameged != null)
        {
            if(Time.time <= alredyDameged.MyTime + targetRenualInterval)
            {
                alredyDameged.Damageable.TakeDamage(dmg, DamageType.MAGIC);
            }
        }
        else
        {
            damageable.TakeDamage(dmg, DamageType.MAGIC);
            targets.Add(new HitedTargets(damageable));
        }

        
    }
}
