using UnityEngine;

public class CephaloMechaAssenbler : MechaAssembler
{
    [SerializeField] protected MechaArms middleArm;

    protected MechaArmPart middleArmPart;

    public MechaArmPart MiddleArmPart { get { return middleArmPart; } }


    protected override void Awake()
    {
        base.Awake();

        middleArmPart = Instantiate(MechaPartService.Instance.GetArmPart(middleArm), (bodyPart as CephaloTorso).GetMiddleArmSocket());
    }
}
