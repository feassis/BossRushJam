using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaPartService : MonoBehaviour
{
    public static MechaPartService Instance;

    [SerializeField] private MechaPartsConfig config;

    public MechaBodyPart GetMechaBody(MechaTorso torso) => config.GetMechaBody(torso);

    public MechaArmPart GetArmPart(MechaArms arm) => config.GetMechaArm(arm);

    public MechaLegPart GetMechaLeg(MechaLegs leg) => config.GetMechaLeg(leg);

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
}
