using UnityEngine;

public class CephaloMechaAssenbler : MechaAssembler
{
    [SerializeField] protected MechaArmPart middleArmPrefab;

    protected MechaArmPart middleArmPart;

    public MechaArmPart MiddleArmPart { get { return middleArmPart; } }


    protected override void Awake()
    {
        base.Awake();

        middleArmPart = Instantiate(middleArmPrefab, (bodyPart as CephaloTorso).GetMiddleArmSocket());
    }
}
