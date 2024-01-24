using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pyroprimatus : EnemyMecha
{
    [Header("Movement Configuration")]
    [SerializeField] private float maxPlayerDistance = 20;
    [SerializeField] private float minPlayerDistance = 15;
    [SerializeField] private float movementCoroutineInterval = 0.5f;
    [SerializeField] private float[] playerAvoidanceAngles = new float[2];
    [SerializeField] private float strafeAngle = 20f;
    [SerializeField] private float legAbilityInterval = 15f;
    [SerializeField] private float legAbilityHealthUperThreshold = 0.5f;
    [SerializeField] private float legAbilityHealthLowerThreshold = 0.1f;
    
    Vector3 movementTarget;
    private float legTimer;

    [Header("Arm Configuration")]
    [SerializeField] private float playerDistanceToFlame = 15f;
    [SerializeField] private float grappleInterval = 5f;
    [SerializeField] private float fancingAngle = 5;
    private float grappleTimer = 0;

    [Header("Torso Configuration")]
    [SerializeField] private float torsoAbilityInterval = 8f;
    private float torsoTimer = 8f;


    protected override void Start()
    {
        base.Start();

        StartCoroutine(MovementCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Draw a wire sphere at the object's position with the specified radius
        Gizmos.DrawWireSphere(transform.position, playerDistanceToFlame);

        if(body != null)
        {
            Gizmos.DrawLine(body.transform.position, body.transform.forward * playerDistanceToFlame);
        }

        
    }

    protected override void Update()
    {
        base.Update();
        var health = stat.GetHealth();

        if (health.GetCurrentHealth() <= health.GetMaxHealth() * legAbilityHealthUperThreshold 
            && health.GetCurrentHealth() > health.GetMaxHealth() * legAbilityHealthLowerThreshold)
        {
            legTimer -= Time.deltaTime;

            if(legTimer < 0)
            {
                leg.OnLegAction();
                legTimer = legAbilityInterval;
            }
        }

        torsoTimer -= Time.deltaTime;
        if (torsoTimer <= 0)
        {
            torsoTimer = torsoAbilityInterval;
            body.OnDefensePerformed(true);
        }

        if(!IsFacingPlayer())
        {
            return;
        }

        bool playerIsClose = Vector3.Distance(PlayerControler.Instance.transform.position, transform.position) < playerDistanceToFlame;

        if (playerIsClose)
        {
            leftArm.OnAttackPressed(true);
        }
        else
        {
            leftArm.OnAttackReleased();
        }

        grappleTimer -= Time.deltaTime;
        if(grappleTimer <= 0 && playerIsClose)
        {
            grappleTimer = grappleInterval;
            rightArm.OnAttackPressed();
        }

        
    }

    private bool IsFacingPlayer()
    {
        var player = PlayerControler.Instance;
        var targetV = (player.transform.position - transform.position).normalized;
        targetV.y = 0;
        var towardsVector = body.transform.forward;
        towardsVector.y = 0;
        var dotProduct = Vector3.Dot(targetV, towardsVector);
        var angle = Mathf.Rad2Deg * dotProduct;

        return Mathf.Abs(angle) <= fancingAngle;
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
}