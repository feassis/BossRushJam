using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChainedGhost : MonoBehaviour
{
    [SerializeField] private Health hp;
    [SerializeField] private MechaStats stats;
    [SerializeField] private SphereDetection detection;
    [SerializeField] private float speed;
    [SerializeField] private float tick = 0.2f;
    [SerializeField] private float lifetime = 20;
    private bool isPlayerTarget;
    private float damage;

    public Mecha target;
    private float tickTimer;
    private GameObject owner;

    public void Setup(bool isPlayerTarget, float damage, GameObject owner)
    {
        this.isPlayerTarget = isPlayerTarget;
        this.damage = damage;
        this.owner = owner;
        hp.Setup(1, stats);
        target = GetTarget();

        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private Mecha GetTarget()
    {
        if (isPlayerTarget)
        {
            return MechaManager.Instance.PlayerMecha[0];
        }

        return MechaManager.Instance.EnemyMecha[0];
    }

    protected virtual void Update()
    {
        transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;

        tickTimer -= Time.deltaTime;

        if (!target)
        {
            target = GetTarget();
        }

        if(tickTimer < 0)
        {
            tickTimer = tick;

            foreach (var health in detection.GetEntitiesInRange())
            {
                if (health.gameObject.TryGetComponent<ChainedGhost>(out ChainedGhost chainedGhost))
                {
                    chainedGhost.owner = owner;
                    continue;
                }

                health.TakeDamage(damage * tick);
            }
        }
    }
}
