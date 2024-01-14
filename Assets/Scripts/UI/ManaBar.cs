using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Image fillBar;
    [SerializeField] private float lowerLimit;
    [SerializeField] private float upperLimit;
    [SerializeField] private Mana mana;

    private void Start()
    {
        mana.OnManaGain += Mana_OnManaGain;
        mana.OnManaLose += Mana_OnManaLose;
        SetUpFillValue(mana.GetCurrentMana() / mana.GetMaxMana());
    }

    private void Mana_OnManaLose(float currentMP, float maxMP)
    {
        SetUpFillValue(currentMP/ maxMP);
    }

    private void Mana_OnManaGain(float currentMP, float maxMP)
    {
        SetUpFillValue(currentMP / maxMP);
    }

    private void SetUpFillValue(float percentage)
    {
        fillBar.fillAmount = lowerLimit + (upperLimit - lowerLimit) * percentage;
    }
}
