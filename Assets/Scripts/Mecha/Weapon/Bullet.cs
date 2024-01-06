using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float dmg;
    private float speed;
    private Vector3 movementDirection = Vector3.zero;

    public void Setup(float dmg, float speed, Vector3 directon)
    {
        this.dmg = dmg;
        this.speed = speed;
        this.movementDirection = directon;
    }

    private void Update()
    {
        transform.position += movementDirection.normalized * speed * Time.deltaTime;
    }
}
