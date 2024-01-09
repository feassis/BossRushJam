using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChangeAnimation : MonoBehaviour
{
    [SerializeField] private float scaleChangingDuration = 0.5f;
    [SerializeField] private Vector3 initialScale;
    [SerializeField] private Vector3 finalScale;

    private float elapsedTime;

    private void Awake()
    {
        transform.localScale = initialScale;
    }

    private void Update()
    {
        if(elapsedTime > scaleChangingDuration)
        {
            return;
        }


        elapsedTime = Mathf.Clamp(elapsedTime + Time.deltaTime, 0, scaleChangingDuration);

        transform.localScale = initialScale + (finalScale - initialScale) * (elapsedTime/scaleChangingDuration);
    }

}
