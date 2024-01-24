using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAcidPuddle : MonoBehaviour
{
    private float dmg = 1;
    private float dmgPeriod= 0.1f;
    private float duration = 1;
    private GameObject owner;
    private DamageType damageType;

    private float damageTimer = 0;

    private List<IDamageable> damageables = new List<IDamageable>();

    public void Setup(float dmg, float dmgPeriod, float duration, GameObject owner)
    {
        this.dmg = dmg;
        this.dmgPeriod = dmgPeriod;
        this.duration = duration;
        this.owner = owner;

        StartCoroutine(LifeTimeRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == owner)
        {
            return;
        }

        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageble))
        {
            damageables.Add(damageble);
        }
    }

    private IEnumerator LifeTimeRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(duration);

            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == owner)
        {
            return;
        }

        if (TryGetComponent<IDamageable>(out IDamageable damageble))
        {
            damageables.Remove(damageble);
        }
    }

    private void Update()
    {
        if (damageables.Count <= 0)
        {
            return;
        }

        damageTimer += Time.deltaTime;

        if (damageTimer >= dmgPeriod)
        {
            damageTimer = 0;

            foreach (var damageble in damageables)
            {
                if (damageble != null)
                {
                    damageble.TakeDamage(dmg, DamageType.NONE);
                }
            }
        }
    }
}
