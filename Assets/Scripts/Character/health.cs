using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour, IDamageable
{
    public float pubinvAftHit;
    public float pubmaxHealth;
    [Header("Для просмотра")]
    public float pubcurHealth;
    public bool pubisInvincible;
    
    ////////////////////
    public float invAftHit { get; set; }
    public bool isInvincible { get; set; }
    public float maxHealth { get; set; }
    public float curHealth { get; set; }
    public IEnumerator makeInvincible(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
        yield return null;
    }
    public void setPub() 
    {
        maxHealth = pubmaxHealth;
        invAftHit = pubinvAftHit;
        //curHealth = pubcurHealth;
    }
    public void dealDamage(float damage, string source)  //My source is what i made it the fuck up
    {
        if (isInvincible) return;
        curHealth -= damage;
        if (curHealth <= 0) { death(); curHealth = 0; }
        StartCoroutine(makeInvincible(invAftHit));
    }
    public void dealHeal(float heal, string source)  
    {
        curHealth += heal;
        if (curHealth > maxHealth) { curHealth = maxHealth; }
    }
    public virtual void death() 
    {
        Debug.Log(gameObject.name + " died");
    }
    // Start is called before the first frame update
    void Awake()
    {
        setPub();
        curHealth = maxHealth;
        
       // StartCoroutine(makeInvincible(10));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pubcurHealth = curHealth;
        pubisInvincible = isInvincible;
        //Debug.Log("fixedUpdate");
    }
}
