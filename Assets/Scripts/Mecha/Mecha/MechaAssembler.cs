using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaAssembler : MonoBehaviour
{
    [SerializeField] protected MechaBodyPart bodyPrefab;
    [SerializeField] protected MechaArmPart leftArmPrefab;
    [SerializeField] protected MechaArmPart rightArmPrefab;
    [SerializeField] protected MechaLegPart legPrefab;

    protected MechaBodyPart bodyPart;
    protected MechaArmPart leftArmPart;
    protected MechaArmPart rightArmPart;
    protected MechaLegPart legPart;

    protected virtual void Awake()
    {
        bodyPart = Instantiate(bodyPrefab, transform);
        leftArmPart = Instantiate(leftArmPrefab, bodyPart.GetLeftArmSocket());
        rightArmPart = Instantiate(rightArmPrefab, bodyPart.GetRightArmSocket());
        legPart = Instantiate(legPrefab, bodyPart.GetLegSocket());
    }

    public MechaBodyPart BodyPart { get { return bodyPart; } }
    public MechaArmPart LeftArmPart { get { return leftArmPart; } }
    public MechaArmPart RightArmPart { get { return rightArmPart; } }
    public MechaLegPart LegPart { get { return legPart; } }
}
