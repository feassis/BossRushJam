using System;
using System.Collections;
using UnityEngine;

public class CoolDownLeg : MechaLegPart
{
    [SerializeField] private float abilityDuration;
    [SerializeField][Range(0, 1)] private float healthCostPercentage = 0.05f;
    [SerializeField][Range(0, 1)] private float healthHealPercentage = 0.5f;

    public override void OnLegAction()
    {
        SpendManaAndAct(() =>
        {
            var allMechas = MechaManager.Instance.GetAllMechas();

            var health = mechaStats.GetHealth();

            health.TakeDamage(mechaStats.GetStat(Stat.HP).Amount * healthCostPercentage);

            foreach (var mech in allMechas)
            {
                mech.GetMechaStats().AddStatusEffectWithLifetime(StatusEffect.SlowDown50, abilityDuration);
            }

            StartCoroutine(HealthRecoveryRoutine());
        });
    }

    private IEnumerator HealthRecoveryRoutine()
    {
        yield return new WaitForSeconds(abilityDuration);

        var health = mechaStats.GetHealth();
        health.Heal(mechaStats.GetStat(Stat.HP).Amount * healthHealPercentage);
    }
}