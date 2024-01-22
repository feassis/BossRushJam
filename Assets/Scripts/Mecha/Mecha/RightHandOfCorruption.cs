using UnityEngine;

public class RightHandOfCorruption : MechaArmPart
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float gasLifetime = 12;
    [SerializeField] private float coolDown = 15;
    [SerializeField] private float tick = 0.2f;
    [SerializeField] private float manaPercentageDamage = 0.02f;
    [SerializeField] private RightHandOfCorruptionMist prefab;

    private float timer = 0;

    public override void OnAttackPressed(bool isplayerTarget = false)
    {
        base.OnAttackPressed(isplayerTarget);

        if (timer > 0)
        {
            return;
        }

        SpendManaAndAct(() =>
        {
            var inst = Instantiate(prefab);
            inst.transform.position = spawnPoint.position;

            inst.Setup(mechaStats.gameObject);
            inst.SetupDamage(GetDamage(), tick, gasLifetime, manaPercentageDamage);
            timer = coolDown;
        });
    }

    protected override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;
    }
}
