using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BlueAcidFlashBomb : ArcBullet
{
    [SerializeField] private BlueAcidPuddle acidPuddlePrefab;
    [SerializeField] private float dmgTickPeriod;
    [SerializeField] private float duration;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private AudioSource audioSourceHited;
    [SerializeField] private MeshRenderer mesh;

    bool hitedSomething = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Obstacle")
        {
            BlueAcidPuddle puddle = Instantiate(acidPuddlePrefab);
            Ray ray = new Ray(transform.position, Vector3.down);

            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, obstacleMask);


            puddle.transform.position = raycastHit.point + new Vector3(0, 0.001f, 0);
            puddle.Setup(dmg, dmgTickPeriod, duration, owner);

            StartCoroutine(HitedSomething());
        }
    }

    private IEnumerator HitedSomething()
    {
        hitedSomething = true;
        audioSourceHited.Play();
        mesh.enabled = false;

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}