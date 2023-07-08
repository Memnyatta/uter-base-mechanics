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
    public string explTrigger;
    public float speed;
    public string uterName;
    public float pastUterMod;
    public float minDist;
    [Header("Для просмотра")]
    public Vector3 endPoint;
    public Animator anim;
    public GameObject uter;
    public NavMeshAgent navAgent;
    // Start is called before the first frame update
    public Vector3 dest(GameObject utr) 
    {
        Vector3 end = utr.transform.position + (utr.transform.position - transform.position) * pastUterMod;
        return end;
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
        uter = GameObject.Find(uterName);
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;
        endPoint = dest(uter);
        setDest(endPoint);
    }
    public void endedWay() 
    {
        anim.SetTrigger(explTrigger);
    }
    public virtual void setDest(Vector3 v) 
    {
        navAgent.SetDestination(v);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, endPoint) < minDist) 
        {
            endedWay();
        }
    }
}
