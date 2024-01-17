using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI;

public class MechaBodyPart : MechaPart
{
    [SerializeField] private Transform leftArmSocket;
    [SerializeField] private Transform rightArmSocket;
    [SerializeField] private Transform legSocket;

    [SerializeField] private float rotationSpeed = 180f;

    public Transform GetLeftArmSocket() => leftArmSocket;

    public Transform GetRightArmSocket() => rightArmSocket;

    public Transform GetLegSocket() => legSocket;

    private Vector3 target;

    public virtual void OnDefensePerformed()
    {
      
    }

    public virtual void OnDefenseCanceled()
    {
        
    }

    public virtual void SetLookUpTarget(Vector3 target)
    {
        this.target = target;
    }

    protected virtual void Update()
    {
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
