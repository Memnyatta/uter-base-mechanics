using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * To modify explosion, find "explosion" prefab in "robotDelivery" folder and change DamWithPush script variables
 * The explosion itself triggers by an event in animation "prep" in the same folder
 */
public class robotDam : damWithpush
{
    public Vector3 spawnOffset;
    public string explTrigger;
    public float throwForce;
    [Header("Префабы")]
    public GameObject corspe;
    public GameObject explos;
    [Header("Для просмотра")]
    public NavMeshAgent navAgent;
    public Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        colBox = GetComponent<BoxCollider>();
        canBeDest = false;
        StartCoroutine(destImmune());

    }
    public void ragdollin() 
    {
        colBox.enabled = false;
        navAgent.enabled = false;

        anim.SetTrigger(explTrigger);
    }
    public void launchCorpse() 
    {
        //damaging player 

        //Launching corpse
        {
            Rigidbody rb = Instantiate(corspe, transform.position + spawnOffset, Quaternion.identity).GetComponent<Rigidbody>();
            Instantiate(explos, transform.position + spawnOffset, Quaternion.identity);
            Vector3 rand = new Vector3(Random.Range(-8, 8), Random.Range(10, 18), Random.Range(-8, 8));
            rb.AddForce(rand * throwForce);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        bool can = canBeDest && other.gameObject != gameObject && tags.Contains(other.gameObject.tag);
        if (can)
        {
            pushPlayer(other);
            damaging(other.gameObject);
            ragdollin();
            //collide(other);
        }
    }
}
