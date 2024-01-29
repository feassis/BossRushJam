using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance;

    public MechaTorso torso = MechaTorso.EnergyShield;
    public MechaArms leftArm = MechaArms.RapidCaster;
    public MechaArms rightArm = MechaArms.ShieldDestroyer;
    public MechaLegs leg = MechaLegs.CoolDown;

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
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
