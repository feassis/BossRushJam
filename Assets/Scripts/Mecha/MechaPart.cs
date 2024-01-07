using System.Collections.Generic;
using UnityEngine;

public class MechaPart : MonoBehaviour
{
    [SerializeField] private List<AvailableStat> statList = new List<AvailableStat>();

    protected Rigidbody parent;
    protected MechaStats mechaStats;

    public List<AvailableStat> GetPartStatus() => statList;
    public virtual void Setup(Rigidbody parent, MechaStats stats)
    {
        this.mechaStats = stats;
        this.parent = parent;
    }
}