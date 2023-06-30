using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*
 * (Writing in English so if everything breaks the text will remain)
 * Health for destructable objects
 * To setup: 
 * 1)Create a model 
 * 2)Attach script, add a "destructable" tag 
 * 3)Make a blank child
 * 4)Add pieces (as children) to said child (Make sure each piece has Rigidbody)
 * 5)Deactivate the child
 * 6)Drag the child to "allPieces"
 * 
 * */
public class destrHealth : health
{
    [Header("BlastForce - Сила разлета во все стороны")]
    [Header("DirectForce - Влияние положения игрока на траекторию")]
    public float blastForce;
    public float directForce;
    public float liveTime;
    public UnityEvent onDest;
    [Header("Для просмотра")]
    
    public MeshRenderer meshRend; //Визуальный меш объекта
    public Collider col; //Коллайдер объекта
    public GameObject allPieces; //От этого объекта зависят все невидимые куски
    public List<Rigidbody> pieces; //Список всех кусков

    public IEnumerator waitForDest(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        yield return null;
    }
    void Awake()
    {
        col = GetComponent<BoxCollider>();
        meshRend = GetComponent<MeshRenderer>();
        foreach (Transform t in allPieces.transform) 
        {
            pieces.Add(t.GetComponent<Rigidbody>());
            //Debug.Log(t.gameObject.name);
        }
        allPieces.SetActive(false);
        setPub();
        curHealth = maxHealth;
    }
    public override void death()
    {
        meshRend.enabled = false;
        col.enabled = false;
        allPieces.SetActive(true);
        foreach (Rigidbody r in pieces) 
        {
            Vector3 dir = (transform.position - lastAttacker.transform.position) * directForce + (r.transform.position - transform.position) * blastForce;
            r.AddForce(dir);
        }
        Debug.Log(gameObject.name + " destroyed by " + lastAttacker.name);
        waitForDest(liveTime);

        if (onDest != null) onDest.Invoke();
    }
    void FixedUpdate()
    {
        pubcurHealth = curHealth;
        pubisInvincible = isInvincible;
        //Debug.Log("fixedUpdate");
    }
}
