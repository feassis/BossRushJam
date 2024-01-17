using UnityEngine;

public class Mecha : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected MechaStats stat;

    public MechaStats GetMechaStats() { return stat; }
}
