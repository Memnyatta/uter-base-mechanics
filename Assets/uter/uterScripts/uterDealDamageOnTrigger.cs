using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyCharacterMovement;
public class uterDealDamageOnTrigger : dealDamageOnCollision
{
    [Header("Не наследуемые переменные ")]
    public float invDur;
     IDamageable canDamage;
    
    
    public float upForce;
    public float angleForce; //Насколько сильно будем отталкиваться конкретно от объекта, а не просто вверх
    public bool hasDamaged;
  //  Vector3 oldPos;
    

    private void OnEnable()
    {
       // oldPos = transform.position;
        resett();
      //  Debug.Log("active");
    }
    private void OnDisable()
    {
        resett();
        //Debug.Log("inactive");
    }
    public void resett() 
    {
        canDamage = null;
        hasDamaged = false;
    }
    // Start is called before the first frame update
    void Awake()
    {
        onAwake();
        
       

        damagin = uter.GetComponent<IDamageable>();
    }
    public virtual void dealDamage(Collider other) 
    {
        damagin.makeInv(invDur);

        //rbUter.isKinematic = false;
        //rbUter.AddForce(Vector3.up*upForce + (other.gameObject.transform.position - uter.transform.position) * angleForce,ForceMode.Force);
        //cmUter.AddForce(Vector3.up * upForce + (other.gameObject.transform.position - uter.transform.position) * angleForce);
        hasDamaged = true;
        canDamage.takeDamage(damage);
        gameObject.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        
        
        if (other.transform.parent != null)
        {
            if (other.transform.parent.gameObject.GetComponent<IDamageable>() != null && other.gameObject.tag != playerTag && other.transform.parent.gameObject.tag != playerTag && !hasDamaged /*&& transform.position.y <= oldPos.y*/)
            { 
            canDamage = other.transform.gameObject.GetComponent<IDamageable>();
            dealDamage(other);
              //  Debug.Log("ol - " + oldPos.y + " new - " + transform.position.y);
            }
        }
        
        if (other.gameObject.GetComponent<IDamageable>() != null && other.gameObject.tag != playerTag && !hasDamaged /*&& transform.position.y <= oldPos.y*/) 
        {
            canDamage = other.gameObject.GetComponent<IDamageable>();
            dealDamage(other);
           // Debug.Log("ol - " + oldPos.y + " new - " + transform.position.y);
        }
       
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
