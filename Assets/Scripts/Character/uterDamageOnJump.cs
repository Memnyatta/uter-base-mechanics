using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class uterDamageOnJump : damageOnTrigger
{
    public bool hasDamaged;
    GameObject uter;
    public ThirdPersonController tpContr;
    [Header("Для просмотра")]
    public float minYVel;
    public float jumpHeight;
    // Start is called before the first frame update
    void Awake()
    {
        colBox = GetComponent<BoxCollider>();
        canBeDest = false;
        StartCoroutine(destImmune());
        uter = transform.parent.gameObject;
        tpContr = uter.GetComponent<ThirdPersonController>();
    }
    public override void damaging(GameObject obj)
    {
        curDam = null;
        curDam = obj.GetComponent<IDamageable>();
        // Debug.Log("Damage trigger: " + gameObject.name + " damaged " + obj.name);
        if (curDam == null) return;
        curDam.dealDamage(damage, gameObject);
        tpContr.velocity.y = jumpHeight;
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
