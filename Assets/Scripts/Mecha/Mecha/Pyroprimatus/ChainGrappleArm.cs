using UnityEngine;

public class ChainGrappleArm : MechaArmPart
{
    [SerializeField] private Transform clawBase;
    [SerializeField] private LineRenderer chainLineRenderer = null;
    [SerializeField] private GrappleClaw claw = null;
    [SerializeField] private float range = 10;
    [SerializeField] private float chainGrappleSpeed = 20;
    [SerializeField] private float grabDuration = 0.5f;


    private void Awake()
    {
        chainLineRenderer.enabled = true;
        chainLineRenderer.SetPosition(1, claw.transform.position);
    }

    public override void OnAttackPressed(bool isplayerTarget = false)
    {
        if (!claw.CanFire())
        {
            return;
        }

        SpendManaAndAct(() =>
        {
            claw.ShootClaw(clawBase, range, chainGrappleSpeed, 
                mechaStats, mechaStats.gameObject.GetComponent<Mecha>(), 
                grabDuration, GetDamage());
        });
    }

    private void LateUpdate()
    {
        chainLineRenderer.enabled = true;
        chainLineRenderer.SetPosition(0, clawBase.transform.position);
        chainLineRenderer.SetPosition(1, claw.transform.position);
    }
}