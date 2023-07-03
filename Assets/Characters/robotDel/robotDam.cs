using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotDam : damageOnTrigger
{
    public Vector3 spawnOffset;
    
    public float throwForce;
    [Header("Префабы")]
    public GameObject corspe;
    public GameObject explos;
    // Start is called before the first frame update
    void Awake()
    {
        colBox = GetComponent<BoxCollider>();
        canBeDest = false;
        StartCoroutine(destImmune());

    }
    public void ragdollin() 
    {
        Rigidbody rb = Instantiate(corspe, transform.position + spawnOffset, Quaternion.identity).GetComponent<Rigidbody>();
        Instantiate(explos, transform.position + spawnOffset, Quaternion.identity);
        Vector3 rand = new Vector3(Random.Range(-8, 8), Random.Range(10, 18), Random.Range(-8, 8));
        rb.AddForce(rand * throwForce);
    }
    private void OnTriggerStay(Collider other)
    {
        bool can = canBeDest && other.gameObject != gameObject && tags.Contains(other.gameObject.tag);
        if (can)
        {
            damaging(other.gameObject);
            ragdollin();
            collide(other);
        }
    }
}
