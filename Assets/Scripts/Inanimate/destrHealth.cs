using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class destrHealth : health
{
    [Header("BlastForce - ���� ������� �� ��� �������")]
    [Header("DirectForce - ������� ��������� ������ �� ����������")]
    public float blastForce;
    public float directForce;
    public float liveTime;
    public UnityEvent onDest;
    [Header("��� ���������")]
    
    public MeshRenderer meshRend; //���������� ��� �������
    public Collider col; //��������� �������
    public GameObject allPieces; //�� ����� ������� ������� ��� ��������� �����
    public List<Rigidbody> pieces; //������ ���� ������

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
