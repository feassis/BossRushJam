using System;
using System.Collections.Generic;
using UnityEngine;

public class MechaPart : MonoBehaviour
{
    [SerializeField] private List<AvailableStat> statList = new List<AvailableStat>();
    [SerializeField] protected float abilityManaCost = 1;

    protected Rigidbody parent;
    protected MechaStats mechaStats;

    public List<AvailableStat> GetPartStatus() => statList;
    public virtual void Setup(Rigidbody parent, MechaStats stats)
    {
        this.mechaStats = stats;
        this.parent = parent;
    }

    protected void SpendManaAndAct(Action doIfHasMana)
    {
        if (abilityManaCost > mechaStats.GetCurrentMana())
        {
            return;
        }

        mechaStats.SpendMana(abilityManaCost);

        doIfHasMana?.Invoke();
    }
}