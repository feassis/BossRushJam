using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mecha Parts Config", menuName = "Configs/Mecha Part Configs")]
public class MechaPartsConfig : ScriptableObject
{
    [SerializeField] private List<TorsoOption> torsoOptions;
    [SerializeField] private List<ArmOption> armOptions;
    [SerializeField] private List<LegOption> legOptions;

    public MechaBodyPart GetMechaBody(MechaTorso torso)
    {
        return torsoOptions.Find(t => t.Type == torso).BodyPart;
    }

    public MechaArmPart GetMechaArm(MechaArms arm)
    {
        return armOptions.Find(a => a.Type == arm).ArmPart;
    }

    public MechaLegPart GetMechaLeg(MechaLegs leg)
    {
        return legOptions.Find(l => l.Type == leg).LegPart;
    }

    [Serializable]
    private struct TorsoOption
    {
        public MechaTorso Type;
        public MechaBodyPart BodyPart;
    }

    [Serializable]
    private struct ArmOption
    {
        public MechaArms Type;
        public MechaArmPart ArmPart;
    }

    [Serializable]
    private struct LegOption
    {
        public MechaLegs Type;
        public MechaLegPart LegPart;
    }
}

public enum MechaTorso
{
    EnergyShield = 0,
    ChainedGhost = 1,
    CephaloTorso = 2,
}

public enum MechaArms
{
    RapidCaster = 0,
    ShieldDestroyer = 1,
    ChainGrappled = 2,
    FlameTorchBeacon = 3,
    BlueAcidFlashBomb = 4,
    PinkAcidLazer = 5,
    RedAcidCannon = 6,
    SandHammer = 7,
    SandSlasher = 8,
    LeftHandOfCorruption = 9,
    RightHandOfCorruption = 10,
}

public enum MechaLegs
{
    CoolDown = 0,
    OverHeat = 1,
    TurretMode = 2,

}