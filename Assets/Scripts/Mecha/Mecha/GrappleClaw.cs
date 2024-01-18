using System.Runtime.CompilerServices;
using UnityEngine;

public class GrappleClaw : MonoBehaviour
{
    [SerializeField] private Transform grabedPositionOffset;

    private Transform returnPoint;
    private Vector3 initialPoint;
    private Quaternion initialRotation;
    private float range;
    private float speed;
    private MechaStats stats;
    private Mecha owner;
    private Mecha grabedMecha;

    private ClawState clawState;

    private Vector3 grabedPosition;
    private float grabDuration;
    private float grabElapsedTime;
    private float damage;

    private Vector3 grabGravity = 3 * Physics.gravity;

    public bool CanFire() => clawState == ClawState.Idle;

    private enum ClawState
    {
        Idle = 0,
        Shooting = 1,
        GrabReturn = 2,
        EmptyReturn = 3,
    }

    public void ShootClaw(Transform returnPoint, float range, float speed, MechaStats stats, Mecha owner, float grabDuration, float damage)
    {
        if(clawState != ClawState.Idle)
        {
            return;
        }

        this.returnPoint = returnPoint;
        this.range = range;
        this.speed = speed;
        this.stats = stats;
        this.owner = owner;
        this.grabDuration = grabDuration;
        grabElapsedTime = 0;
        this.damage = damage;

        stats.AddStatusEffect(StatusEffect.GrabbingRooted);

        initialPoint = transform.position;
        initialRotation = transform.rotation;
        ChangeState(ClawState.Shooting);
    }

    private void Update()
    {
        switch (clawState)
        {
            case ClawState.Idle:
                break;
            case ClawState.Shooting:
                ShootingStateMovement();
                break;
            case ClawState.GrabReturn:
                GrabedReturnMovement();
                break;
            case ClawState.EmptyReturn:
                EmptyReturnMovement();
                break;
            default:
                break;
        }
    }

    private void ShootingStateMovement()
    {
        if (Vector3.SqrMagnitude(transform.position - initialPoint) > range * range)
        {
            ChangeState(ClawState.EmptyReturn);
            return;
        }


        transform.rotation = initialRotation;
        transform.position += transform.forward * speed * Time.deltaTime; 
    }

    private void EmptyReturnMovement()
    {
        if (Vector3.Distance(returnPoint.position, transform.position) < 0.5)
        {
            transform.position = returnPoint.position;
            transform.rotation = returnPoint.rotation;
            stats.RemoveStatusEffect(StatusEffect.GrabbingRooted);
            ChangeState(ClawState.Idle);
            return;
        }
        

        transform.rotation = initialRotation;

        transform.position = transform.position +  (returnPoint.position - transform.position).normalized * speed * Time.deltaTime;
    }

    private void GrabedReturnMovement()
    {
        if (Vector3.Distance(returnPoint.position, transform.position) < 0.5)
        {
            grabedMecha.GetMechaStats().RemoveStatusEffect(StatusEffect.Rooted);
            grabedMecha.GetMechaStats().GetHealth().TakeDamage(damage);
            transform.position = returnPoint.position;
            transform.rotation = returnPoint.rotation;
            stats.RemoveStatusEffect(StatusEffect.GrabbingRooted);
            ChangeState(ClawState.Idle);
            return;
        }

        grabElapsedTime = Mathf.Clamp(grabElapsedTime + Time.deltaTime, 0, grabDuration);

        transform.rotation = initialRotation;
        transform.position = CalculatePosition(grabElapsedTime);
        grabedMecha.transform.position = grabedPositionOffset.position;
    }

    private Vector3 CalculatePosition(float time)
    {
        var v0 = GetInitialVelocity();

        return grabedPosition + v0 * time + (grabGravity)*(time * time) / 2;
    }

    private Vector3 GetInitialVelocity()
    {
        return (returnPoint.position - grabedPosition - (grabGravity) * grabDuration * grabDuration / 2) / grabDuration;
    }

    private void ChangeState(ClawState clawState)
    {
        this.clawState = clawState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (clawState != ClawState.Shooting)
        {
            return;
        }

        if (other.gameObject == owner.gameObject)
        {
            return;
        }
               

        if(other.gameObject.TryGetComponent<Mecha>(out Mecha mecha))
        {
            mecha.GetMechaStats().AddStatusEffect(StatusEffect.Rooted);
            grabedMecha = mecha;
            grabedPosition = transform.position;
            ChangeState(ClawState.GrabReturn);
        }
    }
}


