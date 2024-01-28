using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaAssembler : MonoBehaviour
{
    [SerializeField] protected MechaTorso torso;
    [SerializeField] protected MechaArms leftArm;
    [SerializeField] protected MechaArms rightArm;
    [SerializeField] protected MechaLegs leg;

    protected MechaBodyPart bodyPart;
    protected MechaArmPart leftArmPart;
    protected MechaArmPart rightArmPart;
    protected MechaLegPart legPart;

    protected virtual void Awake()
    {
        bodyPart = Instantiate(MechaPartService.Instance.GetMechaBody(torso), transform);
        leftArmPart = Instantiate(MechaPartService.Instance.GetArmPart(leftArm), bodyPart.GetLeftArmSocket());
        rightArmPart = Instantiate(MechaPartService.Instance.GetArmPart(rightArm), bodyPart.GetRightArmSocket());
        legPart = Instantiate(MechaPartService.Instance.GetMechaLeg(leg), bodyPart.GetLegSocket());
    }

    public MechaBodyPart BodyPart { get { return bodyPart; } }
    public MechaArmPart LeftArmPart { get { return leftArmPart; } }
    public MechaArmPart RightArmPart { get { return rightArmPart; } }
    public MechaLegPart LegPart { get { return legPart; } }
}
