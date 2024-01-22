using UnityEngine;

public class HomingBullet : Bullet
{
    [SerializeField] private float rotationSpeed = 30f;
    private GameObject target;
    public void SetHommingTarget(GameObject target)
    {
        this.target = target;
    }

    protected override void Update()
    {
        // Calculate the direction to the target
        Vector3 directionToTarget = target.transform.position - transform.position;

        // Ensure that the direction has a non-zero length
        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            // Flatten the direction to only consider the X-Z plane
            directionToTarget.y = 0f;

            // Create a rotation that looks towards the target in the X-Z plane
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        var desiredIntecrement = transform.forward * speed * Time.deltaTime;

        desiredIntecrement.y = 0;

        transform.position += desiredIntecrement;
    }
}
