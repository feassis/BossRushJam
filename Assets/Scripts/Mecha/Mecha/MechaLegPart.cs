using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class MechaLegPart : MechaPart
{
    [SerializeField] private float dashSpeedMultiplier = 3;
    [SerializeField] private float dashDuration;

    protected Vector3 direction;
    private Vector3 dashDirection;
    private float dashTimer;

    private bool isDashing;

    public virtual void OnMoveAction(Vector3 direction)
    {
        this.direction = direction;
    }

    public virtual void  SetDestination(NavMeshAgent agent, Vector3 destination)
    {
        var speedStat = mechaStats.GetStatValue(Stat.SPD);
        agent.speed = isDashing ? speedStat * dashSpeedMultiplier : speedStat;

        agent.SetDestination(destination);
    }

    public virtual void OnLegAction()
    {
        if(isDashing)
        {
            return;
        }


        SpendManaAndAct(() =>
        {
            isDashing = true;

            dashDirection = direction;

            if (dashDirection == Vector3.zero)
            {
                dashDirection = new Vector3(0, 0, 1);
            }
        });
    }

    protected virtual void Update()
    {
        if(mechaStats == null)
        {
            return;
        }

        if(isDashing) 
        {
            Dash();
        }
        else
        {
            Move();
        }
        
    }

    protected void Dash()
    {
        dashTimer += Time.deltaTime;

        if(dashTimer > dashDuration)
        {
            dashTimer = 0;
            isDashing = false;
            return;
        }

        var speedStat = mechaStats.GetStatValue(Stat.SPD);

        parent.velocity = dashDirection.normalized * speedStat * dashSpeedMultiplier + new Vector3(0, parent.velocity.y, 0);
    }

    protected virtual void Move()
    {
        var speedStat = mechaStats.GetStatValue(Stat.SPD);

        parent.velocity = direction.normalized * speedStat + new Vector3(0, parent.velocity.y, 0);
    }
}
