using UnityEngine;
using UnityEngine.Video;

public class SandHammer : MechaArmPart
{
    [SerializeField] private SphereDetection sphereDetection;
    [SerializeField] private float range = 3f;
    [SerializeField] private float stunDuration = 3f;
    [SerializeField] private float weaponCooldown = 2f;

    private float timer = 0f;

    private void Start()
    {
        sphereDetection.transform.localScale =  new Vector3(range, range, range);
        sphereDetection.Setup(mechaStats.gameObject);
    }

    protected override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;
    }

    public override void OnAttackPressed(bool isplayerTarget = false)
    {
        if(timer > 0)
        {
            return;
        }

        SpendManaAndAct(() =>
        {
            timer = weaponCooldown;
            foreach (var health in sphereDetection.GetEntitiesInRange())
            {
                health.TakeDamage(GetDamage());
                if(health.gameObject.TryGetComponent<MechaStats>(out MechaStats mechaStats))
                {
                    mechaStats.AddStatusEffectWithLifetime(StatusEffect.Rooted, stunDuration);
                }
            }
        });
    }

    public override void OnAttackReleased()
    {
        base.OnAttackReleased();
    }
}
