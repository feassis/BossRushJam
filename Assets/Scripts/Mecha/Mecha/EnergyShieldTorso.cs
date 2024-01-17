using System.Collections;
using System.Threading;
using UnityEngine;

public class EnergyShieldTorso : MechaBodyPart
{
    [SerializeField] private float shieldManaSpendingInterval = 1f;
    [SerializeField] private GameObject shield;
    private bool isShieldOn = false;

    private GameObject shieldInst;
    private float shieldTimer = 0;

    public override void OnDefensePerformed()
    {
        SpendManaAndAct(() =>
        {
            isShieldOn = true;
            shieldInst = Instantiate(shield, transform);
            shieldTimer = shieldManaSpendingInterval;
            mechaStats.AddStatusEffect(StatusEffect.DamageReduction50);
        });
    }

    public override void OnDefenseCanceled()
    {
        mechaStats.RemoveStatusEffect(StatusEffect.DamageReduction50);
        isShieldOn = false;

        if(shieldInst != null)
        {
            Destroy(shieldInst);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!isShieldOn)
        {
            return;
        }

        shieldTimer -= Time.deltaTime;

        if(shieldTimer <= 0)
        {
            bool keepShiledOn = false;
            SpendManaAndAct(() =>
            {
                keepShiledOn = true;
                shieldTimer = shieldManaSpendingInterval;
            });

            if(!keepShiledOn)
            {
                OnDefenseCanceled();
            }
        }
    }
}
