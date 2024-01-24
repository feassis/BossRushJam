using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FlameTorchBeacon : MechaArmPart
{
    [SerializeField] private float flamethrowerRange;
    [SerializeField] private SphereDetection detectionArea;
    [SerializeField] private float coneFlameWidth = 45;
    [SerializeField] private Transform flameTip;
    [SerializeField] private float tickSpeed = 0.2f;
    [SerializeField] private float manaRenewalInterval = 1f;
    [SerializeField] private ParticleSystem flameParticles;

    private bool isShooting;
    private float tickTimer;
    private float manaTimer;


    private void Start()
    {
        detectionArea.transform.localScale = new Vector3(flamethrowerRange, flamethrowerRange, flamethrowerRange);
        detectionArea.Setup(mechaStats.gameObject);
    }

    public override void OnAttackPressed(bool isplayerTarget = false)
    {
        if (isShooting)
        {
            return;
        }

        SpendManaAndAct(() =>
        {
            isShooting = true;
            tickTimer = tickSpeed;
            manaTimer = manaRenewalInterval;
            flameParticles.gameObject.SetActive(true);
        });
    }

    public override void OnAttackReleased()
    {
        isShooting = false;
        flameParticles.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        GenerateVectors();
    }
    private void GenerateVectors()
    {
        Vector3 rotatedVector = (Quaternion.Euler(0f, coneFlameWidth, 0f) * flameTip.forward).normalized;
        Debug.DrawLine(flameTip.position, flameTip.position + rotatedVector * flamethrowerRange);

        rotatedVector = (Quaternion.Euler(0f, -coneFlameWidth, 0f) * flameTip.forward).normalized;
        Debug.DrawLine(flameTip.position, flameTip.position + rotatedVector * flamethrowerRange);

        rotatedVector = (Quaternion.Euler(coneFlameWidth, 0f, 0f) * flameTip.forward).normalized;
        Debug.DrawLine(flameTip.position, flameTip.position + rotatedVector * flamethrowerRange);

        rotatedVector = (Quaternion.Euler(-coneFlameWidth, 0f, 0f) * flameTip.forward).normalized;
        Debug.DrawLine(flameTip.position, flameTip.position + rotatedVector * flamethrowerRange);

        rotatedVector = (Quaternion.Euler(0f, 0f, coneFlameWidth) * flameTip.forward).normalized;
        Debug.DrawLine(flameTip.position, flameTip.position + rotatedVector * flamethrowerRange);

        rotatedVector = (Quaternion.Euler(0f, 0f, coneFlameWidth) * flameTip.forward).normalized;
        Debug.DrawLine(flameTip.position, flameTip.position + rotatedVector * flamethrowerRange);
    }


    protected override void Update()
    {
        if (!isShooting)
        {
            return;
        }

        tickTimer -= Time.deltaTime;
        manaTimer -= Time.deltaTime;

        if(tickTimer <= 0)
        {
            foreach(var health in GetEnemiesInCone())
            {
                health.TakeDamage(GetDamage());
            }

            tickTimer = tickSpeed;
        }

        if(manaTimer <= 0)
        {
            bool hasMana = false;

            SpendManaAndAct(() =>
            {
                hasMana = true;
            });

            isShooting = hasMana;

            if(!hasMana)
            {
                flameParticles.gameObject.SetActive(false);
            }

            manaTimer = manaRenewalInterval;
        }
    }


    private List<Health> GetEnemiesInCone()
    {
        List<Health> enemies = new List<Health>();
        foreach (var health  in detectionArea.GetEntitiesInRange())
        {
            var targetV = (health.transform.position - transform.position).normalized;
            var towardsVector = flameTip.forward;
            var dotProduct = Vector3.Dot(targetV, towardsVector);
            var angle = Mathf.Rad2Deg * dotProduct;

            if(Mathf.Abs(angle) <= coneFlameWidth)
            {
                enemies.Add(health);
            }
        }
        return enemies;
    }
}
