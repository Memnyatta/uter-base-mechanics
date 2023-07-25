using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageOnCollision : damageOnTrigger
{
    [Header("-damageOnCollision-")]
    public float bounceForce;
    public Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        colBox = GetComponent<BoxCollider>();
        canBeDest = false;
        StartCoroutine(destImmune());

    }
    private void OnCollisionStay(Collision other)
    {
        bool can = canBeDest && other.gameObject != gameObject && tags.Contains(other.gameObject.tag);
        if (can)
        {
            damaging(other.gameObject);
            collide(other.collider);
            Vector3 bounceDir = transform.position - other.gameObject.transform.position;
            rb.AddForce(bounceDir * bounceForce, ForceMode.Force);
        }
    }
}
