using System.Collections;
using UnityEngine;

public class RightHandOfCorruptionMist : SphereDetection
{
    private float manaPercentage;
    private float damage;
    private float tick;
    private float duration;

    private float tickTimer = 0;

    public void SetupDamage(float dmgPerSecond, float tick, float duration, float manaPercentage)
    {
        damage = dmgPerSecond;
        this.tick = tick;
        this.duration = duration;
        this.manaPercentage = manaPercentage;

        StartCoroutine(Lifetime());
    }

    private void Update()
    {
        tickTimer -= Time.deltaTime;

        if(tickTimer < 0)
        {
            tickTimer = tick;
            foreach (var entitie in entitiesInRange)
            {
                entitie.TakeDamage(damage * tick, DamageType.MAGIC);
                if(entitie.gameObject.TryGetComponent<Mana>(out Mana mana))
                {
                    mana.SpendMana(mana.GetMaxMana() * manaPercentage);
                }
            }
        }
    }


    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}