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
    [Header("Для просмотра")]
    public IDamageable curDam;
    public BoxCollider colBox;
    // Start is called before the first frame update
    void Awake()
    {
        colBox = GetComponent<BoxCollider>(); 
    }
    public void damaging(GameObject obj) 
    {
        curDam = null;
        curDam = obj.GetComponent<IDamageable>();

        if (curDam == null) return;
        curDam.dealDamage(damage, gameObject.name);
        

        if (onCol != null) 
        {
            Debug.Log("onCol");
            onCol(gameObject);
            //onCol = null;
        }
           
        if (delOnDam) Destroy(gameObject);
         


    }
    private void OnTriggerStay(Collider other)
    {
        if (tags.Contains(other.gameObject.tag))
        {
            damaging(other.gameObject);
            Debug.Log("Bullet collides " + other.gameObject.name);
        }
        
    }
}
