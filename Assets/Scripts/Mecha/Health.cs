using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private float maxHealth;
    private float currentHealth; //Current Health That The Player Has
    private MechaStats mechaStat;

    public event Action<float, float> OnDamageTaken;
    public event Action<float, float> OnDamageHealed;
    public event Action OnSetupEnded;

    void Start()
    {
        //Grab Reference To Our Torso Defensive Stat
    }

    public void Setup(float hp, MechaStats stats)
    {
        maxHealth = hp;
        currentHealth = maxHealth;
        mechaStat = stats;
        OnSetupEnded?.Invoke();
    }

    public float GetMaxHealth() => maxHealth;
    public float GetCurrentHealth() => currentHealth;

    public void TakeDamage(float damageTaken) //Function Called As Declared By IDamageable Interface
    {
        if (mechaStat.HasStatus(StatusEffect.Invulnerable))
        {
            return;
        }

        currentHealth = Mathf.Clamp(CalculateDamage(damageTaken), 0, maxHealth);
        OnDamageTaken?.Invoke(GetCurrentHealth(), GetMaxHealth());

        if(currentHealth <= 0)
        {
            Death();
        }
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
            TakeDamage(dmg / numberOfTicks);
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

        Debug.Log("Death");
    }

    private float CalculateDamage(float damageTaken) //Responsible For Calculating Damage Player Takes From Enemies
    {
        //Grab Our Defensive Value From Torso & See If We Reduced Damage This Turn

        float reducedPercentage = UnityEngine.Random.Range(0f, 100f); // Random Range Is Generated Between 0 And 100

        float calculatedDamage = damageTaken;
        /*if(reducedPercentage <= "Insert Torso Defense Value Here")
        {
            float newHealth = currentHealth - damageTaken * .50f; //Reduced Damage Taken By Player In Half
            return newHealth; //Returns Value
        }
        */

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
