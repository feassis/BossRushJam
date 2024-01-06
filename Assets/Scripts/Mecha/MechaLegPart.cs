using System;
using System.Threading;
using UnityEngine;

public class MechaLegPart : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float dashSpeed = 50;
    [SerializeField] private float dashDuration;

    private Rigidbody parent;

    private Vector3 direction;
    private Vector3 dashDirection;
    private float dashTimer;

    private bool isDashing;
    
    public void Setup(Rigidbody parent)
    {
        this.parent = parent;
    }

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

        isDashing = true;

        dashDirection = direction;

        if(dashDirection == Vector3.zero)
        {
            dashDirection = new Vector3(0,0,1);
        }
    }

    private void Update()
    {
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

        parent.velocity = dashDirection.normalized * dashSpeed + new Vector3(0, parent.velocity.y, 0);
    }

    private void Move()
    {
        parent.velocity = direction.normalized * speed + new Vector3(0, parent.velocity.y, 0);
    }
}
