using UnityEngine;
using UnityEngine.Rendering;

public class BlueAcidFlashBomb : ArcBullet
{
    [SerializeField] private BlueAcidPuddle acidPuddlePrefab;
    [SerializeField] private float dmgTickPeriod;
    [SerializeField] private float duration;
    [SerializeField] private LayerMask obstacleMask;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Obstacle")
        {
            BlueAcidPuddle puddle = Instantiate(acidPuddlePrefab);
            Ray ray = new Ray(transform.position, Vector3.down);

            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, obstacleMask);


            puddle.transform.position = raycastHit.point + new Vector3(0, 0.001f, 0);
            puddle.Setup(dmg, dmgTickPeriod, duration, owner);

            Destroy(gameObject);
        }
    }
}