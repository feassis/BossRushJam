using UnityEngine;

public class MechaArmSimpleProjectile: MechaArmPart
{
    [SerializeField] protected Transform projectileSpawnPos;
    [SerializeField] protected float fireRate = 3;
    [SerializeField] protected float bulletSpeed = 20;
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] protected Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);

    protected float GetCooldown() => 1 / fireRate;

    protected bool isShooting = false;
    protected float shootTimer;

    public override void OnAttackPressed(bool isplayerTarget = false)
    {
        base.OnAttackPressed(isplayerTarget);
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

                var damagePerBullet = GetDamage();
                bullet.Setup(damagePerBullet, bulletSpeed, GetShootDirection(), parent.gameObject);

                shootTimer = GetCooldown();
            });
            
        }  
    }

    protected Vector3 GetShootDirection()
    {
        Vector3 direction = projectileSpawnPos.transform.forward;

        direction += new Vector3(
            Random.Range(-bulletSpreadVariance.x, +-bulletSpreadVariance.x),
            Random.Range(-bulletSpreadVariance.y, +bulletSpreadVariance.y),
            Random.Range(-bulletSpreadVariance.z, +bulletSpreadVariance.z));

        return direction.normalized;
    }
}
