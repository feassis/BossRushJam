using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    private float maxMana;
    private float currentMana;
    private float manaRes;

    Coroutine manaResRotine;

    public void Setup(float mp, float mpRes)
    {
        maxMana = mp;
        currentMana = maxMana;
        manaRes = mpRes;

        if(manaResRotine != null)
        {
            StopCoroutine(manaResRotine);
        }
        
        manaResRotine = StartCoroutine(ManaRegenerationRotine());
    }

    public float GetCurrentMana() => currentMana;

    public void AddMana(float mp)
    {
        currentMana =  Mathf.Clamp(currentMana + mp, 0, maxMana);
    }

    public void SpendMana(float mp)
    {
        currentMana = Mathf.Clamp(currentMana - mp, 0, maxMana);
    }

    private IEnumerator ManaRegenerationRotine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            AddMana(manaRes);
        }
    }
}
