using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private float maxHealth; //Maximum Health That The Player Has
    private float currentHealth; //Current Health That The Player Has
    private float iDuration; //Duration In Which The Player Is Invincible

    public enum HealthState //Current Health State Of The Player
    {
        Undamaged,
        Damaged,
        Death
    }

    public HealthState currentHealthState; //Current State Of Player Health

    void Start()
    {
        //Grab Reference To Our Torso Defensive Stat
    }

    public void Setup(float hp)
    {
        maxHealth = hp;
        currentHealth = maxHealth;
    }


    public void TakeDamage(float damageTaken) //Function Called As Declared By IDamageable Interface
    {
        if(currentHealthState == HealthState.Undamaged)
        {
            currentHealth = CalculateDamage(damageTaken);
            if(currentHealth > 0)
            {
                ChangeHealthState(HealthState.Damaged);
            } else if(currentHealth <= 0)
            {
                ChangeHealthState(HealthState.Death);
            }
        } 
    }

    public void Heal(float healAmount) //Function Called Upon Player Heal
    {
        currentHealth = CaluclateHeal(healAmount);
    }

    private void Death() //Function Called Upon Player Health Reaching Zero
    {
        //Stop Accepting Player Input
        //Do Animation
    }

    private float CalculateDamage(float damageTaken) //Responsible For Calculating Damage Player Takes From Enemies
    {
        //Grab Our Defensive Value From Torso & See If We Reduced Damage This Turn

        float reducedPercentage = Random.Range(0f, 100f); // Random Range Is Generated Between 0 And 100
        
        /*if(reducedPercentage <= "Insert Torso Defense Value Here")
        {
            float newHealth = currentHealth - damageTaken * .50f; //Reduced Damage Taken By Player In Half
            return newHealth; //Returns Value
        }
        */

        float newHealth = currentHealth - damageTaken; //No Reduced Damage Taken Place
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

    private IEnumerator Invincible() //Gets Called Once Player Is Damaged 
    {
        //Ability To Not Be Damaged Already In Affect Due To State Not Matching
        yield return new WaitForSeconds(iDuration);
        ChangeHealthState(HealthState.Undamaged); //Invinciblity Is Over Therefore The Player Is Vulnerable
    }

    private void ChangeHealthState(HealthState newState)
    {
        if(currentHealthState != newState)
        {
            currentHealthState = newState;

            HandleHealthState(newState);
        }
    }

    private void HandleHealthState(HealthState healthState)
    {
        switch(healthState)
        {
            case HealthState.Undamaged:
            //Enter Logic
            break;

            case HealthState.Damaged:
            StartCoroutine("Invincible"); //Triggers Our IFrames
            break; 

            case HealthState.Death:
            Death(); //Triggers Death
            break;

            default:
            Debug.LogError("HealthState Does Not Exist"); //Logs A Warning
            break;
        }
    }
}
