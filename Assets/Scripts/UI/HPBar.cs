using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image[] hpBarImages;
    [SerializeField] private TextMeshProUGUI lifeAmountText;
    [SerializeField] private Health health;

    private void Start()
    {
        health.OnDamageHealed += UpdateHealthBar;
        health.OnDamageTaken += UpdateHealthBar;
        health.OnSetupEnded += Health_OnSetupEnded;

        UpdateHealthBar(health.GetMaxHealth(), health.GetMaxHealth());
    }

    private void Health_OnSetupEnded()
    {
        UpdateHealthBar(health.GetMaxHealth(), health.GetMaxHealth());
    }

    private void UpdateHealthBar(float currentHP, float maxHP)
    {
        if(maxHP == 0)
        {
            return; 
        }

        var hpThreshold = maxHP / hpBarImages.Length;

        var desiredIndex = Mathf.Ceil(currentHP / hpThreshold) - 1;

        for (int i = 0; i < hpBarImages.Length; i++)
        {
            if (i == desiredIndex)
            {
                hpBarImages[i].gameObject.SetActive(true);
            }
            else
            {
                hpBarImages[i].gameObject.SetActive(false);
            }
        }

        lifeAmountText.text = Mathf.FloorToInt(currentHP).ToString();
    }
}
