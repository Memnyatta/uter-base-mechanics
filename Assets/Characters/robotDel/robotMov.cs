using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class robotMov : MonoBehaviour
{
    public float speed;
    public string uterName;
    [Header("Для просмотра")]
    public GameObject uter;
    public NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Awake()
    {
        uter = GameObject.Find(uterName);
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;

        setDest(uter.transform.position);
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
