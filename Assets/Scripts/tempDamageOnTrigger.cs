using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class tempDamageOnTrigger : damageOnTrigger
{

    
    public int usesBeforeStop;
    // Start is called before the first frame update
    void Start()
    {
        colBox = GetComponent<BoxCollider>();
        canBeDest = false;
        StartCoroutine(destImmune());
    }

    public override void collide(Collider other)
    {
        onColEvent.Invoke();
        if (delOnDam) { Destroy(gameObject); }
        if (usesBeforeStop <= 0) { Destroy(this); }
    }
    private void OnTriggerStay(Collider other)
    {
        onTriggerS(other);
    }
}
