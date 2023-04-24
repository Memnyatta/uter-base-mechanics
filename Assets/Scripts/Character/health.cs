using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public float pubmaxHealth;
    [Header("Для просмотра")]
    public float pubcurHealth;
    
    ////////////////////
    
    public float maxHealth;
    public float curHealth;
    public void setPub() 
    {
        maxHealth = pubmaxHealth;
        //curHealth = pubcurHealth;
    }
    public void dealDamage(float damage, string source)  //My source is what i made it the fuck up
    {
        curHealth -= damage;
        if (curHealth <= 0) { death(); }
    }
    public void dealHeal(float heal, string source)  
    {
        curHealth += heal;
        if (curHealth > maxHealth) { curHealth = maxHealth; }
    }
    public void death() 
    {
    
    }
    // Start is called before the first frame update
    void Awake()
    {
        setPub();
        curHealth = maxHealth;

        dealHeal(1, "self");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pubcurHealth = curHealth;
    }
}
