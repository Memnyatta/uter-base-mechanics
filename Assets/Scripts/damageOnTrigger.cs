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
        if (onCol != null)
        {
           //Debug.Log("Damage trigger: " + gameObject.name + " collided with " + other.gameObject.name);
            onCol(gameObject);
            //onCol = null;
        }

        if (delOnDam) Destroy(gameObject);
    }
    public void damaging(GameObject obj) 
    {
        curDam = null;
        curDam = obj.GetComponent<IDamageable>();
       // Debug.Log("Damage trigger: " + gameObject.name + " damaged " + obj.name);
        if (curDam == null) return;
        curDam.dealDamage(damage, gameObject.name);
    }
    public IEnumerator destImmune()
    {
        yield return new WaitForSeconds(destImmuneTime);
        canBeDest = true;
        yield return null;
    }


        private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject);
        if (canBeDest && other.gameObject != gameObject) 
        {
            //Debug.Log(other.gameObject.tag + other.gameObject.name);
            if (tags.Contains(other.gameObject.tag) && canBeDest)
            {
                
                damaging(other.gameObject);
            }
            collide(other);
        }
        
        

    }
}
