using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaStats : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Mana mana;
    [SerializeField] private List<AvailableStat> stats = new List<AvailableStat>();

    private List<StatusEffect> effects = new List<StatusEffect>();

    public void AddStatusEffect(StatusEffect statusEffect)
    {
        effects.Add(statusEffect);
    }

    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        effects.Remove(statusEffect);
    }

    public bool HasStatus(StatusEffect statusEffect)
    {
        return effects.Contains(statusEffect);
    }

    public Health GetHealth()
    {
        return health;
    }


    public float GetStatValue(Stat stat)
    {
        if (stats.Count <= 0)
        {
            return 0;
        }

        return stat switch
        {
            Stat.SPD => HasStatus(StatusEffect.Rooted) ? 0 : GetStat(Stat.SPD).Amount,
            _ => GetStat(stat).Amount
        };
    }


    public AvailableStat GetStat(Stat stat) 
    {
        if(stats.Count <= 0)
        {
            return null;
        }

        return stats.Find(s => s.Type == stat);
    } 

    public float GetCurrentMana() => mana.GetCurrentMana();
    public void SpendMana(float amount) => mana.SpendMana(amount);
    
    public void Setup(List<AvailableStat> stats)
    {
        this.stats.Clear();

        foreach (var stat in stats)
        {
            AvailableStat inListStat = this.stats.Find(s => s.Type == stat.Type);

            if (inListStat != null)
            {
                inListStat.Amount += stat.Amount;
            }
            else
            {
                this.stats.Add(stat);
            }
        }

        health.Setup(GetStat(Stat.HP).Amount, this);
        mana.Setup(GetStat(Stat.MP).Amount, GetStat(Stat.MPRES).Amount);
    }
}

public enum StatusEffect
{
    None = 0,
    Rooted = 1,
    Invulnerable = 2,
}
