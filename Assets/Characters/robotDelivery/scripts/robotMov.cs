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
    public float damImpact;
    public List<string> damTags;
    public string uterName;
    public string explAnimTrigger;
    public Vector3 corpseOffset;
    public Vector3 minCorpseLaunch;
    public Vector3 maxCorpseLaunch;
    public float corpseLaunchForce;
    public float speed;
    public float pastUterMod;
    public float minDist;
    public GameObject expl;
    public GameObject corpse;
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
        anim.SetTrigger(explAnimTrigger);
        navAgent.enabled = false;
        
    }
    public void explosion() 
    {
        Instantiate(expl, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
    public virtual void setDest(Vector3 v) 
    {
        navAgent.SetDestination(v);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (damTags.Contains(other.gameObject.tag))
    //    {
    //        IDamageable dam = other.gameObject.GetComponent<IDamageable>();
    //        if (dam != null) 
    //        {
    //            dam.dealDamage(damImpact, gameObject);
    //        }
    //    }
    //}
    public void spawnCorpse() 
    {
        Rigidbody rb = Instantiate(corpse, transform.position + corpseOffset, Quaternion.identity).GetComponent<Rigidbody>();
        Vector3 randDir = new Vector3(Random.Range(minCorpseLaunch.x, maxCorpseLaunch.x), Random.Range(minCorpseLaunch.y, maxCorpseLaunch.y), Random.Range(minCorpseLaunch.z, maxCorpseLaunch.z));
        rb.AddForce(randDir * corpseLaunchForce);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, endPoint) < minDist || Vector3.Distance(transform.position, uter.transform.position) < minDist) 
        {
            endedWay();
        }
    }
}
