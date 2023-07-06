using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * In order for it to be possible for a robot to miss uter,
 * i get a static position past Uter, "pastUterMod" determines how far this
 * imaginary target is
 */
public class robotMov : MonoBehaviour
{
    public float speed;
    public string uterName;
    public float pastUterMod;
    [Header("��� ���������")]
    public GameObject uter;
    public NavMeshAgent navAgent;
    // Start is called before the first frame update
    public Vector3 setDest(GameObject utr) 
    {
        Vector3 end = utr.transform.position + (utr.transform.position - transform.position) * pastUterMod;
        return end;
    }
    void Awake()
    {
        uter = GameObject.Find(uterName);
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;

        setDest(setDest(uter));
    }
    public virtual void setDest(Vector3 v) 
    {
        navAgent.SetDestination(v);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
