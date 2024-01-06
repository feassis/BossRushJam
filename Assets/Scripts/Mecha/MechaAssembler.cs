using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaAssembler : MonoBehaviour
{
    [SerializeField] private MechaBodyPart bodyPrefab;
    [SerializeField] private MechaArmPart leftArmPrefab;
    [SerializeField] private MechaArmPart rightArmPrefab;
    [SerializeField] private MechaLegPart legPrefab;

    private MechaBodyPart bodyPart;
    private MechaArmPart leftArmPart;
    private MechaArmPart rightArmPart;
    private MechaLegPart legPart;

    private void Awake()
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
