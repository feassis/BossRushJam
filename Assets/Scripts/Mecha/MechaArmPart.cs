﻿using UnityEngine;

public class MechaArmPart : MechaPart
{
    [SerializeField] private float atkUsage = 1;
    [SerializeField] private float intUsage = 1;

    public float GetDamage() => mechaStats.GetStat(Stat.ATK).Amount * atkUsage + mechaStats.GetStat(Stat.INT).Amount * intUsage;

    public virtual void OnAttackPressed()
    {

    }

    public virtual void OnAttackReleased() 
    { 
        
    }

    protected virtual void Update()
    {
        
    }
}
