using System.Collections;
using UnityEngine;

public class AcidShieldBubbleTorso : MechaBodyPart
{
    [SerializeField] private float shieldDuration = 10;
    [SerializeField] private AcidBubbleShield shield;
    private bool isShieldOn = false;

    public override void OnDefensePerformed()
    {
        if(isShieldOn)
        {
            return;
        }

        SpendManaAndAct(() =>
        {
            isShieldOn = true;
            var shieldInst = Instantiate(shield, transform);
            shieldInst.Setup(shieldDuration);

            mechaStats.AddStatusEffect(StatusEffect.Rooted);
            mechaStats.AddStatusEffect(StatusEffect.Invulnerable);

            StartCoroutine(RemoveStatusRoutine());
        });

        
    }

    private IEnumerator RemoveStatusRoutine()
    {
        yield return new WaitForSeconds(shieldDuration);
        mechaStats.RemoveStatusEffect(StatusEffect.Rooted);
        mechaStats.RemoveStatusEffect(StatusEffect.Invulnerable);
    }
}