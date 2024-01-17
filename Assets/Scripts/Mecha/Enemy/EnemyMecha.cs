using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyMecha : Mecha
{
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected MechaAssembler assembler;
    

    protected MechaLegPart leg;
    protected MechaArmPart leftArm;
    protected MechaArmPart rightArm;
    protected MechaBodyPart body;

    protected virtual void Awake()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    protected virtual void Start()
    {
        MechaManager.Instance.AddMecha(this, false);
        var statList = new List<AvailableStat>();
        statList.AddRange(stat.Stats);

        leg = assembler.LegPart;
        leftArm = assembler.LeftArmPart;
        rightArm = assembler.RightArmPart;
        body = assembler.BodyPart;

        leg.Setup(rb, stat);
        leftArm.Setup(rb, stat);
        rightArm.Setup(rb, stat);
        body.Setup(rb, stat);

        statList.AddRange(leg.GetPartStatus());
        statList.AddRange(leftArm.GetPartStatus());
        statList.AddRange(rightArm.GetPartStatus());
        statList.AddRange(body.GetPartStatus());

        stat.Setup(statList);
    }

    protected virtual void Update()
    {
        body.SetLookUpTarget(PlayerControler.Instance.transform.position);
    }
}
