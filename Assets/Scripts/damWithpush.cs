using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Damaging and pushing whatever we need at the same time
public class damWithpush : damageOnTrigger
{
    public float pushStr;
    // Start is called before the first frame update
    void Awake()
    {
        colBox = GetComponent<BoxCollider>();
        canBeDest = false;
        StartCoroutine(destImmune());

    }
    public virtual void pushPlayer(Collider other) 
    {
        if (other.gameObject.GetComponent<ThirdPersonController>() != null)
        {
            ThirdPersonController tpContr = other.gameObject.GetComponent<ThirdPersonController>();
            tpContr.velocity += (other.transform.position - transform.position) * pushStr;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        bool can = canBeDest && other.gameObject != gameObject && tags.Contains(other.gameObject.tag);
        if (can)
        {
            pushPlayer(other);
            damaging(other.gameObject);
            collide(other);
        }
    }
}
