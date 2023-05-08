using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageOnTrigger : MonoBehaviour
{
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
        Debug.Log("dealt damage");

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
