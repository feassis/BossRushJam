using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBubbleShield : MonoBehaviour, IDamageable
{
    private float lifetime;
    public void Setup(float duration)
    {
        lifetime = duration;

        StartCoroutine(LifetimeRoutine());
    } 

    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    public void TakeDamage(float dmg)
    {
        
    }

    public void TakeDamageOverTime(float dmg, float duration, int numberOfTick)
    {
        
    }

    
}
