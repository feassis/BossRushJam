using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CephaloMecha : EnemyMecha
{
    [Header("Movement Configuration")]
    [SerializeField] private float maxPlayerDistance = 20;
    [SerializeField] private float minPlayerDistance = 10;
    [SerializeField] private float movementCoroutineInterval = 0.5f;
    [SerializeField] private float[] playerAvoidanceAngles = new float[2];
    [SerializeField] private float strafeAngle = 20f;
    [SerializeField] private float legAbilityInterval = 30f;

    [Header("Attack Configuration")]
    [SerializeField] private float attackModeInterval = 10;

    [Header("Defense Configuration")]
    [SerializeField][Range(0, 1)] private float defensePercentage = 0.5f;
    [SerializeField] private float defenseCooldown = 60f;


    private MechaArmPart middleArm;
    Vector3 movementTarget;
    private float attackRoutineTimer = 0;
    private int attackModeIndex = 0;
    private AttackMode attackMode;

    private float defenseTimer = 0;

    private enum AttackMode
    {
        PinkAcidLazer = 0,
        BlueAcidMode = 1,
        RedAcidLayer = 2
    }


    protected override void Start()
    {
        base.Start();

        var statList = new List<AvailableStat>();
        statList.AddRange(stat.Stats);

        middleArm = (assembler as CephaloMechaAssenbler).MiddleArmPart;
        middleArm.Setup(rb, stat);
        statList.AddRange(middleArm.GetPartStatus());

        stat.Setup(statList);

        StartCoroutine(MovementCoroutine());
        StartCoroutine(LegAbilityRoutine());
    }

    private IEnumerator LegAbilityRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(legAbilityInterval);

            leg.OnLegAction();
        }
    }

    private IEnumerator MovementCoroutine()
    {
        while (true)
        {
            var playerPos = PlayerControler.Instance.transform.position;
            var distance = Vector3.Distance(playerPos, transform.position);
            
            if (distance > maxPlayerDistance)
            {
                movementTarget = playerPos;
            }
            else if (distance > minPlayerDistance)
            {
                var destination = GetRandomPointWithinAngle(
                    PlayerControler.Instance.transform.position,
                    transform.position,
                    distance,
                    strafeAngle);

                movementTarget = destination;
            }
            else
            {
                var movementVector = transform.position - playerPos;
                
                movementVector = movementVector.normalized * ((maxPlayerDistance - minPlayerDistance) / 2 + minPlayerDistance);

                Quaternion rotation = Quaternion.Euler(0f, Random.Range(playerAvoidanceAngles[0], playerAvoidanceAngles[1]), 0f);

                movementVector = rotation * movementVector;

                var targetPos = transform.position + movementVector;

                movementTarget = targetPos;
            }

            leg.SetDestination(_agent, movementTarget);
            yield return new WaitForSeconds(movementCoroutineInterval);
        }
    }

    private void ChangeAttackState()
    {
        attackModeIndex++;
        attackRoutineTimer = 0;

        if(attackMode == AttackMode.PinkAcidLazer)
        {
            middleArm.OnAttackReleased();
        }
        else if (attackMode == AttackMode.RedAcidLayer)
        {
            leftArm.OnAttackReleased();
        }

        switch (attackModeIndex % 3) 
        {
            case 0:
                attackMode = AttackMode.PinkAcidLazer;
                break; 
            case 1:
                attackMode = AttackMode.BlueAcidMode;
                break;
            case 2:
                attackMode = AttackMode.RedAcidLayer;
                break;
        }
    } 


    private Vector3 GetRandomPointWithinAngle(Vector3 center, Vector3 pointOnCircumference, float radius, float maxAngle)
    {
        // Calculate the angle between the center, the point, and the new random point
        float angleBetweenPoints = Mathf.Atan2(pointOnCircumference.z - center.z, pointOnCircumference.x - center.x) * Mathf.Rad2Deg;

        // Generate a random angle within the specified range
        float randomAngle = Random.Range(angleBetweenPoints - maxAngle, angleBetweenPoints + maxAngle);

        // Calculate the position of the random point using the random angle
        float newX = center.x + radius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float newY = center.z + radius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);

        return new Vector3(newX, center.y, newY);
    }

    protected override void Update()
    {
        base.Update();

        attackRoutineTimer += Time.deltaTime;

        if(attackRoutineTimer > attackModeInterval)
        {
            ChangeAttackState();
        }

        Attack();

        Defense();
    }

    private void Defense()
    {
        var health = stat.GetHealth();
        if (health.GetCurrentHealth() / health.GetMaxHealth() > defensePercentage)
        {
            return;
        }

        defenseTimer -= Time.deltaTime;

        if(defenseTimer < 0)
        {
            defenseTimer = defenseCooldown;
            body.OnDefensePerformed();
        }
    }

    private void Attack()
    {
        switch (attackMode)
        {
            case AttackMode.RedAcidLayer:
                RedAcidAttack();
                break;
            case AttackMode.BlueAcidMode:
                BlueAcidAttack();
                break;
            case AttackMode.PinkAcidLazer:
                PinkAcidAttack();
                break;
        }
    }

    private void PinkAcidAttack()
    {
        middleArm.OnAttackPressed();
    }

    private void BlueAcidAttack()
    {
        rightArm.OnAttackPressed(true);
        rightArm.OnAttackReleased();
    }

    private void RedAcidAttack()
    {
        leftArm.OnAttackPressed();
    }
}
