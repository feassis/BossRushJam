using System;
using System.Threading;
using UnityEngine;

public class MechaLegPart : MechaPart
{
    [SerializeField] private float dashSpeedMultiplier = 3;
    [SerializeField] private float dashDuration;

    private Vector3 direction;
    private Vector3 dashDirection;
    private float dashTimer;

    private bool isDashing;

    public void OnMoveAction(Vector3 direction)
    {
        this.direction = direction;
    }

    public void OnDashAction()
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

    private void Update()
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

    private void Dash()
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

    private void Move()
    {
        var speedStat = mechaStats.GetStatValue(Stat.SPD);

        parent.velocity = direction.normalized * speedStat + new Vector3(0, parent.velocity.y, 0);
    }
}
