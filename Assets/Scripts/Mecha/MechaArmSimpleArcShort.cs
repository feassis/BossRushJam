using UnityEngine;

public class MechaArmSimpleArcShort : MechaArmPart
{
    [SerializeField] private Transform projectileSpawnPos;
    [SerializeField] private float damage = 1;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float timeToHitTarget = 2;
    [SerializeField] private ArcBullet bulletPrefab;

    private float GetCooldown() => 1 / fireRate;

    private float shootTimer = 0;

    public override void OnAttackPressed()
    {
        base.OnAttackPressed();
        Shoot();
    }

    public override void OnAttackReleased()
    {
        base.OnAttackReleased();
    }

    protected override void Update()
    {
        base.Update();

        if(shootTimer > 0 )
        {
            shootTimer = Mathf.Clamp(shootTimer -= Time.deltaTime, 0, GetCooldown());
        }
    }

    private void Shoot()
    {
        if(shootTimer > 0)
        {
            return;
        }

        SpendManaAndAct(() =>
        {
            shootTimer = GetCooldown();
            var target = MouseWorld.GetMousePosition();

            Vector3 initialVelocity = (target - projectileSpawnPos.position - (Physics.gravity * timeToHitTarget * timeToHitTarget) / 2) / timeToHitTarget;

            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = projectileSpawnPos.position;
            bullet.transform.rotation = projectileSpawnPos.rotation;
            bullet.Setup(damage, initialVelocity);
        });
    }
}
