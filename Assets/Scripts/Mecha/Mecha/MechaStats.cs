using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaStats : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Mana mana;
    [SerializeField] private List<AvailableStat> stats = new List<AvailableStat>();

    private List<StatusEffect> effects = new List<StatusEffect>();

    private List<StatusDuration> durations = new List<StatusDuration>();

    private class StatusDuration
    {
        public StatusEffect Effect;
        public float Duration;

        public StatusDuration(StatusEffect effect, float duration)
        {
            Effect = effect;
            Duration = duration;
        }
    }

    private void Update()
    {
        List<StatusDuration> tempDurations = new List<StatusDuration>();
        tempDurations.AddRange(durations);

        foreach(var duration in tempDurations)
        {
            duration.Duration -= Time.deltaTime;

            if (duration.Duration <= 0)
            {
                RemoveStatusEffect(duration.Effect);
            }
        }
    }

    public void AddStatusEffect(StatusEffect statusEffect)
    {
        if (effects.Contains(statusEffect))
        {
            return;
        }

        effects.Add(statusEffect);
    }

    public void AddStatusEffectWithLifetime(StatusEffect statusEffect, float duration)
    {
        AddStatusEffect(statusEffect);

        var effectDuration = durations.Find(d => d.Effect == statusEffect);

        if (effectDuration != null)
        {
            effectDuration.Duration = duration;
        }
        else
        {
            durations.Add(new StatusDuration(statusEffect, duration));
        }

    }

    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        effects.Remove(statusEffect);

        var effectDuration = durations.Find(d => d.Effect == statusEffect);

        if (effectDuration != null)
        {
            durations.Remove(effectDuration);
        }
    }

    public bool HasStatus(StatusEffect statusEffect)
    {
        return effects.Contains(statusEffect);
    }

    public List<AvailableStat> Stats { get { return stats; } }

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
            Stat.SPD => GetSpeedStat(),
            Stat.DEF => HasStatus(StatusEffect.DefenseDown30) ? GetStat(Stat.DEF).Amount * (0.7f) : GetStat(Stat.DEF).Amount, 
            _ => GetStat(stat).Amount
        };
    }

    private float GetSpeedStat()
    {
        float rawSpeedStatus = GetStat(Stat.SPD).Amount;

        if (HasStatus(StatusEffect.Rooted) || HasStatus(StatusEffect.GrabbingRooted))
        {
            return 0;
        }

        if (HasStatus(StatusEffect.SlowDown50))
        {
            return rawSpeedStatus * 0.5f;
        }

        return rawSpeedStatus;
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
    DefenseDown30 =3,
    DamageReduction50 = 4,
    SlowDown50 = 5,
    GrabbingRooted = 6
}
