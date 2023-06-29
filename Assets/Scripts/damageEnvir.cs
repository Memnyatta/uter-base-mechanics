using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageEnvir : damageOnTrigger
{
    public GameObject uter;
    [Header("Для просмотра")]
    public string uterName;
    
    public float pushStr;
    void Awake()
    {
        uter = GameObject.Find(uterName);
        colBox = GetComponent<BoxCollider>();
        canBeDest = false;
        StartCoroutine(destImmune());

    }

    private void OnTriggerStay(Collider other)
    {
        bool can = canBeDest && other.gameObject != gameObject && tags.Contains(other.gameObject.tag);
        if (can)
        {
            if (other.gameObject.GetComponent<ThirdPersonController>() != null) 
            {
                ThirdPersonController tpContr = other.gameObject.GetComponent<ThirdPersonController>();
                tpContr.velocity = (other.transform.position - transform.position) *pushStr;
            }
            damaging(other.gameObject);
            collide(other);
        }
    }
}
