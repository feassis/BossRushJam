using UnityEngine;

public class MechaArmSimpleProjectile: MechaArmPart
{
    [SerializeField] private Transform projectileSpawnPos;
    [SerializeField] private float fireRate = 3;
    [SerializeField] private float damagePerBullet;
    [SerializeField] private float bulletSpeed = 20;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);

    private float GetCooldown() => 1 / fireRate;

    private bool isShooting = false;
    private float shootTimer;

    public override void OnAttackPressed()
    {
        base.OnAttackPressed();
        isShooting = true;
    }

    public override void OnAttackReleased()
    {
        base.OnAttackReleased();
        isShooting = false;
        shootTimer = 0;
    }

    protected override void Update()
    {
        if (!isShooting)
        {
            return;
        }

        shootTimer -= Time.deltaTime;

        if (shootTimer < 0)
        {
            SpendManaAndAct(() =>
            {
                var bullet = Instantiate(bulletPrefab);
                bullet.transform.position = projectileSpawnPos.position;
                bullet.transform.rotation = projectileSpawnPos.rotation;

                bullet.Setup(damagePerBullet, bulletSpeed, GetShootDirection(), parent.gameObject);

                shootTimer = GetCooldown();
            });
            
        }  
    }

    private Vector3 GetShootDirection()
    {
        Vector3 direction = projectileSpawnPos.transform.forward;

        direction += new Vector3(
            Random.Range(-bulletSpreadVariance.x, +-bulletSpreadVariance.x),
            Random.Range(-bulletSpreadVariance.y, +bulletSpreadVariance.y),
            Random.Range(-bulletSpreadVariance.z, +bulletSpreadVariance.z));

        return direction.normalized;
    }
}
