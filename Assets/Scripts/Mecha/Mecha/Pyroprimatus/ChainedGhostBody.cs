using UnityEngine;

public class ChainedGhostBody : MechaBodyPart
{
    [SerializeField] private ChainedGhost ghostPrefab;

    [SerializeField] private float damageModifier = 0.14f;
    [SerializeField] private float spawnDistanceFromEnemy = 5;
    
    public override void OnDefensePerformed(bool isPlayerTarget)
    {

        SpendManaAndAct(() => 
        {
            var ghost = Instantiate(ghostPrefab);
            ghost.Setup(isPlayerTarget, mechaStats.GetStatValue(Stat.INT) * damageModifier, mechaStats.gameObject);
            var targetVector = ghost.target.transform.position - transform.position;
            var lenght = targetVector.magnitude;
            ghost.transform.position = transform.position + targetVector.normalized * (lenght + spawnDistanceFromEnemy);
        });
    }
}
