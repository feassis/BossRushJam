using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaManager : MonoBehaviour
{
    public static MechaManager Instance;

    private List<Mecha> enemyMecha = new List<Mecha>();
    private List<Mecha> playerMecha = new List<Mecha>();
    
    public List<Mecha> PlayerMecha { get {  return playerMecha; } }
    public List<Mecha> EnemyMecha {  get { return enemyMecha; } }

    public List<Mecha> GetAllMechas()
    {
        var allMechaList = new List<Mecha>();
        allMechaList.AddRange(enemyMecha);
        allMechaList.AddRange(playerMecha);

        return allMechaList;
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void AddMecha(Mecha mecha, bool isPlayer)
    {
        if (isPlayer)
        {
            playerMecha.Add(mecha);
        }
        else
        {
            enemyMecha.Add(mecha);
        }
    }
}
