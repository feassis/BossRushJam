using System.Collections;
using UnityEngine;

public class OverheatLegs : MechaLegPart
{
    [SerializeField] private float overheatSpeedMultiplier = 2;
    [SerializeField] private float overheatDuration = 20;
    [SerializeField] private float selfDamagePercentage = 0.05f;
    private bool isOverheated;

    public override void OnLegAction()
    {
        if (isOverheated)
        {
            return;
        }

        SpendManaAndAct(() =>
        {
            isOverheated = true;

            var health = mechaStats.GetHealth();

            health.TakeDamage(health.GetMaxHealth() * selfDamagePercentage);

            StartCoroutine(OverheatLifeTime());
        });
    }

    protected override void Update()
    {
        if (mechaStats == null)
        {
            return;
        }

        Move();
    }

    protected override void Move()
    {
        var speedStat = mechaStats.GetStatValue(Stat.SPD);

        if (isOverheated)
        {
            speedStat *= overheatSpeedMultiplier;
        }

        parent.velocity = direction.normalized * speedStat + new Vector3(0, parent.velocity.y, 0);
    }

    private IEnumerator OverheatLifeTime()
    {
        if(overheatDuration > 0)
        {
            yield return new WaitForSeconds(overheatDuration);
            isOverheated = false;
        }
    }
}
