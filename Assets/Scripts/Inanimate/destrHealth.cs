using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destrHealth : health
{
    public float blastForce;
    public float disappearTime;
    [Header("Для просмотра")]
    public MeshRenderer meshRend;
    public Collider col;
    public GameObject allPieces;
    public List<Rigidbody> pieces;

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
            r.AddForce(Vector3.up * blastForce);
        }
        Debug.Log(gameObject.name + " destroyed");
        waitForDest(disappearTime);
    }
    void FixedUpdate()
    {
        pubcurHealth = curHealth;
        pubisInvincible = isInvincible;
        //Debug.Log("fixedUpdate");
    }
}
