using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MechaBodyPart : MechaPart
{
    [SerializeField] private Transform leftArmSocket;
    [SerializeField] private Transform rightArmSocket;
    [SerializeField] private Transform legSocket;

    [SerializeField] private float rotationSpeed = 180f;

    [SerializeField] private GameObject shield;

    public Transform GetLeftArmSocket() => leftArmSocket;

    public Transform GetRightArmSocket() => rightArmSocket;

    public Transform GetLegSocket() => legSocket;

    public void OnDefensePerformed()
    {
        var shieldInst = Instantiate(shield);
        shieldInst.transform.position = parent.transform.position;
    }

    public void OnDefenseCanceled()
    {
        
    }


    private void Update()
    {
        var target = MouseWorld.GetMousePosition();

        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 directionToTarget = target - transform.position;

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
        }
    }
}