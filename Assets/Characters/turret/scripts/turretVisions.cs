using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.Animations.Rigging;

public class turretVisions : MonoBehaviour
{
    [Header("Изменяемые")]
    public float maxDist;
    public LayerMask mask;

    [Header("Для просмотра")]

    public bool isVisible;

    [Header("Сохраненные объекты")]
    public GameObject headAim;
    public GameObject visor;
    public GameObject uter;
    public GameObject target;
    public GameObject Rig1; 
    public Event onSeen;
    public Event onGone;

    [Header("Components")]
    public Rig rg;
    public MultiAimConstraint multyAim;

    [Header("Поиск референсов")]
    public string uterName;
    public string headAimName;
    public string targetName;
    public string visorName;
    public string rigName;

    public void onAwake() 
    {
        uter = GameObject.Find(uterName);
        visor = GameObject.Find(visorName).gameObject;
        target = GameObject.Find(targetName);
        Rig1 = GameObject.Find(rigName).gameObject;
        rg = Rig1.GetComponent<Rig>();
        headAim = GameObject.Find(headAimName).gameObject;
        multyAim = headAim.GetComponent<MultiAimConstraint>();
    }
    void targetRay() 
    {
        target.transform.position = uter.transform.position;
        RaycastHit hit;
        Vector3 dir = target.transform.position - visor.transform.position;
        Physics.Raycast(visor.transform.position, dir*maxDist, out hit, maxDist, mask);


        Debug.DrawRay(visor.transform.position, dir * maxDist, Color.red);
        Debug.Log(hit.transform.gameObject.name + " " + hit.transform.gameObject.layer);
        if (hit.transform.gameObject.name == "Uter") 
        {
            isVisible = true;
            rg.weight = 100;
        }
        else 
        {
            isVisible = false;
            rg.weight = 0;
        }
        //Debug.DrawLine()
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        targetRay();

    }

    // Start is called before the first frame update
    void Awake()
    {onAwake();
        
    }
}
