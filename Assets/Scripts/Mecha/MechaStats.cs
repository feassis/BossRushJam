using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaStats : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Mana mana;
    [SerializeField] private List<AvailableStat> stats = new List<AvailableStat>();

    public AvailableStat GetStat(Stat stat) 
    {
        if(stats.Count <= 0)
        {
            return null;
        }

        return stats.Find(s => s.Type == stat);
    } 
    
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

        health.Setup(GetStat(Stat.HP).Amount);
    }
}
