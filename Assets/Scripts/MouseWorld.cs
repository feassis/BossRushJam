using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseWorld : MonoBehaviour
{
    [SerializeField] private LayerMask floorMask;

    private static MouseWorld instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(GetMouseScreenPosition());

        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.floorMask);

        instance.transform.position = raycastHit.point;

        return raycastHit.point;
    }

    private static Vector3 GetMouseScreenPosition()
    {
        return Mouse.current.position.ReadValue();
    }

    public static Vector3 GetPositionOnlyVisible()
    {
        Ray ray = Camera.main.ScreenPointToRay(GetMouseScreenPosition());

        RaycastHit[] raycastArray = Physics.RaycastAll(ray, float.MaxValue, instance.floorMask);

        System.Array.Sort(raycastArray,
            (RaycastHit a, RaycastHit b) => Mathf.RoundToInt(a.distance - b.distance));
        foreach (var hit in raycastArray)
        {
            if(hit.transform.TryGetComponent<Renderer>(out Renderer renderer))
            {
                if (renderer.enabled)
                {
                    return hit.point;
                }
            }

        }

        return Vector3.zero;
    }
}
