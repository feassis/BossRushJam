using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float dmg, DamageType damgeType);

    public void TakeDamageOverTime(float dmg, float duration, int numberOfTick);
}
