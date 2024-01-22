using UnityEngine;

public class SimpleHommingShooterArm : MechaArmSimpleProjectile
{
    bool isPlayerTarget = false;
    public override void OnAttackPressed(bool isplayerTarget = false)
    {
        isPlayerTarget = isplayerTarget;
        base.OnAttackPressed(isplayerTarget);
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
                ((HomingBullet)bullet).SetHommingTarget(GetTarget());
                shootTimer = GetCooldown();
            });

        }
    }

    private GameObject GetTarget()
    {
        if (isPlayerTarget)
        {
            return PlayerControler.Instance.gameObject;
        }

        return MechaManager.Instance.EnemyMecha[0].gameObject;
    }
}
