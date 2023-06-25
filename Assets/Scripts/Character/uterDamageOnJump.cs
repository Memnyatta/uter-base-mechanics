using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uterDamageOnJump : damageOnTrigger
{
    // Start is called before the first frame update
    void Awake()
    {
        colBox = GetComponent<BoxCollider>();
        canBeDest = false;
        StartCoroutine(destImmune());
    }

    private void OnTriggerStay(Collider other)
    {
        if (canBeDest && other.gameObject != gameObject)
        {
            if (tags.Contains(other.gameObject.tag) && canBeDest)
            {
                damaging(other.gameObject);
            }
            collide(other);
        }
    }
}
