using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Mana mana;

    [SerializeField] private Image hpBar1;
    [SerializeField] private Image hpBar2;
    [SerializeField] private Image manaBar1;
    [SerializeField] private Image manaBar2;

    private void Awake()
    {
        mana.OnManaGain += Mana_OnManaGain;
        mana.OnManaLose += Mana_OnManaLose;

        health.OnDamageTaken += Health_OnDamageTaken;
        health.OnDamageHealed += Health_OnDamageHealed;

    }

    private void Health_OnDamageHealed(float currentHP, float maxHP)
    {
        hpBar1.fillAmount = currentHP / maxHP;
        hpBar2.fillAmount = currentHP / maxHP;
    }

    private void Health_OnDamageTaken(float currentHP, float maxHP)
    {
        hpBar1.fillAmount = currentHP / maxHP;
        hpBar2.fillAmount = currentHP / maxHP;
    }

    private void Mana_OnManaLose(float currentMana, float maxMana)
    {
        manaBar1.fillAmount = currentMana / maxMana;
        manaBar2.fillAmount = currentMana / maxMana;
    }

    private void Mana_OnManaGain(float currentMana, float maxMana)
    {
        manaBar1.fillAmount = currentMana / maxMana;
        manaBar2.fillAmount = currentMana / maxMana;
    }
}
