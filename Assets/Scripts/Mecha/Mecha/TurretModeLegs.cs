using System.Collections;
using UnityEngine;

public class TurretModeLegs : MechaLegPart
{
    [SerializeField] private float duration;

    private bool isOnTurretMode;


    public override void OnLegAction()
    {
        if(isOnTurretMode)
        {
            return;
        }

        SpendManaAndAct(() =>
        {
            isOnTurretMode = true;

            mechaStats.AddStatusEffectWithLifetime(StatusEffect.Rooted, duration);
            mechaStats.AddStatusEffectWithLifetime(StatusEffect.Atk200, duration);
            mechaStats.AddStatusEffectWithLifetime(StatusEffect.Int200, duration);
            mechaStats.AddStatusEffectWithLifetime(StatusEffect.Def125, duration);
            mechaStats.AddStatusEffectWithLifetime(StatusEffect.Wis125, duration);

            StartCoroutine(CoolDownCycle());
        });
    }


    private IEnumerator CoolDownCycle()
    {
        yield return new WaitForSeconds(duration);
        isOnTurretMode = false;
    }
}