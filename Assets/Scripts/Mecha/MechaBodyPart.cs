using UnityEngine;

public class MechaBodyPart : MonoBehaviour
{
    [SerializeField] private Transform leftArmSocket;
    [SerializeField] private Transform rightArmSocket;
    [SerializeField] private Transform legSocket;

    public Transform GetLeftArmSocket() => leftArmSocket;

    public Transform GetRightArmSocket() => rightArmSocket;

    public Transform GetLegSocket() => legSocket;
}