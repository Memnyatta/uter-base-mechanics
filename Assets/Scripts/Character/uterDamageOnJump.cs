using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class uterDamageOnJump : damageOnTrigger
{
    public bool hasDamaged;
    GameObject uter;
    public ThirdPersonController tpContr;
    [Header("��� ���������")]
    public float minYVel;
    // Start is called before the first frame update
    void Awake()
    {
        colBox = GetComponent<BoxCollider>();
        canBeDest = false;
        StartCoroutine(destImmune());
        uter = transform.parent.gameObject;
        tpContr = uter.GetComponent<ThirdPersonController>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        bool can = canBeDest && other.gameObject != gameObject && !hasDamaged && tags.Contains(other.gameObject.tag) && !tpContr.isGrounded && tpContr.velocity.y < -1 * Mathf.Abs(minYVel);
        if (can)
        {
                damaging(other.gameObject);
                collide(other);
                hasDamaged = true;  
        }
    }
    private void FixedUpdate()
    {
        if (tpContr.isGrounded) { hasDamaged = false; }
    }
}
