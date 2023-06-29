using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageOnTrigger : MonoBehaviour
{
    public delegate void collideAct(GameObject t);
    public static event collideAct onCol;

    public float damage;
    public List<string> tags;
    public bool delOnDam;
    public float destImmuneTime;
    [Header("Для просмотра")]
    public bool canBeDest;
    public IDamageable curDam;
    public BoxCollider colBox;
    // Start is called before the first frame update
    void Awake()
    {
        colBox = GetComponent<BoxCollider>();
        canBeDest = false;
        StartCoroutine(destImmune());
        
    }
    public void collide(Collider other) 
    {
        if (onCol != null) {onCol(gameObject);}
        if (delOnDam) { Destroy(gameObject); }
    }
    public virtual void damaging(GameObject obj) 
    {
        curDam = null;
        curDam = obj.GetComponent<IDamageable>();
       // Debug.Log("Damage trigger: " + gameObject.name + " damaged " + obj.name);
        if (curDam == null) return;
        curDam.dealDamage(damage, gameObject);
    }
    public IEnumerator destImmune()
    {
        yield return new WaitForSeconds(destImmuneTime);
        canBeDest = true;
        yield return null;
    }


 private void OnTriggerStay(Collider other)
    {
        bool can = canBeDest && other.gameObject != gameObject && tags.Contains(other.gameObject.tag);
        if (can)
        {
            damaging(other.gameObject);
            collide(other);     
        }  
    }
}
