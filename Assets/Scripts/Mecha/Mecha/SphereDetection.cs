using System.Collections.Generic;
using UnityEngine;

public class SphereDetection : MonoBehaviour
{
    private List<Health> entitiesInRange = new List<Health>();

    private GameObject owner;

    public List<Health> GetEntitiesInRange() => entitiesInRange;

    public void Setup(GameObject owner)
    {
        this.owner = owner; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == owner)
        {
            return;
        }

        if(other.gameObject.TryGetComponent<Health>(out Health health))
        {
            entitiesInRange.Add(health);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            entitiesInRange.Remove(health);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // Draw a wire sphere at the object's position with the specified radius
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2);
    }
}