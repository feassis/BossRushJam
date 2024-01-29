using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour, IDamageable
{
    private float maxHealth;
    private float currentHealth; //Current Health That The Player Has
    private MechaStats mechaStat;
    private DamageType damageType; //ADDED
    public event Action<float, float> OnDamageTaken;
    public event Action<float, float> OnDamageHealed;
    public event Action OnSetupEnded;

    public void Setup(float hp, MechaStats stats)
    {
        maxHealth = hp;
        currentHealth = maxHealth;
        mechaStat = stats;
        OnSetupEnded?.Invoke();
    }

    public float GetMaxHealth() => maxHealth;
    public float GetCurrentHealth() => currentHealth;

    public void TakeDamage(float damageTaken, DamageType damageType) //Function Called As Declared By IDamageable Interface
    {
        if (mechaStat.HasStatus(StatusEffect.Invulnerable))
        {
            return;
        }

        currentHealth = CalculateDamage(damageTaken, damageType);
    }

    public void TakeDamageOverTime(float dmg, float duration, int numberOfTicks)
    {
        StartCoroutine(DamageOverTimeRoutine(dmg, duration, numberOfTicks));
    }

    private IEnumerator DamageOverTimeRoutine(float dmg, float duration, int numberOfTicks) 
    {
        float ticks = 0;
        while(ticks < numberOfTicks)
        {
            yield return new WaitForSeconds(duration / numberOfTicks);
            TakeDamage(dmg / numberOfTicks, DamageType.NONE); //ADDED
            ticks++;
        }
    }

    public void Heal(float healAmount) //Function Called Upon Player Heal
    {
        currentHealth = Mathf.Clamp(CaluclateHeal(healAmount), 0, maxHealth);
        OnDamageHealed?.Invoke(GetCurrentHealth(), GetMaxHealth());
    }

    private void Death() //Function Called Upon Player Health Reaching Zero
    {
        //Stop Accepting Player Input
        //Do Animation
    }

    private float CalculateDamage(float damageTaken, DamageType damageType) //Responsible For Calculating Damage Player Takes From Enemies
    {
        float calculatedDamage = 0;
        switch (damageType)
        {
            case DamageType.PHYSICAL:
            AvailableStat defenseStat = mechaStat.GetStat(Stat.DEF);
            float defenseValue = defenseStat.Amount;
            float reducedPhysicalPercentage = Random.Range(0f, 100f);

            if (reducedPhysicalPercentage <= defenseValue)
            {
                    calculatedDamage = damageTaken * 0.50f;
            }
                break;

            case DamageType.MAGIC:
            AvailableStat wisdomStat = mechaStat.GetStat(Stat.WIS);
            float wisdomValue = wisdomStat.Amount;
            float reducedMagicPercentage = Random.Range(0f, 100f);

            if (reducedMagicPercentage <= wisdomValue)
            {
                // Display Reduced Damage
                calculatedDamage = damageTaken * 0.50f;
            }
                break;

            default:
                calculatedDamage = damageTaken;
                break;
        }
        
        if (mechaStat.HasStatus(StatusEffect.DamageReduction50))
        {
            calculatedDamage *= 0.5f;
        }

        float newHealth = currentHealth - calculatedDamage; //No Reduced Damage Taken Place
        return newHealth; //Returns Value
    }

    private float CaluclateHeal(float healAmount) //Responsible For Calculating Health After Healing
    {
        float healthAfterHeal = currentHealth + healAmount;

        if(healthAfterHeal > maxHealth) //If Our Health After The Heal WOULD BE Above What Our Maximum Health Is
        {
            return maxHealth;
        }

        return healthAfterHeal;
    }

    
}
