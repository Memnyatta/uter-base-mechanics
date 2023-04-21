using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class turretVision : MonoBehaviour
{
    public LayerMask mask;
    public float maxDist;
    public float shootCooldown;
    public float bulletDur;
    public float bulletSpeed;
    public float inactiveSpeed;
    public float minDistInactive;
    [Range(0.0f, 100.0f)]
    public float turnSpeed;
    [Header("Для просмотра")]
    public GameObject curInactive;
    public bool isVisible;
    public bool isShooting;
    public RaycastHit hit;
    public Vector3 dir;
    [Header("Референсы")]
    public List<GameObject> inactives;
    
    public GameObject bullet;
    public GameObject fireHole;
    public GameObject head;
    public GameObject headAim;
    public GameObject target;
    public GameObject uter;
    public MultiAimConstraint multAim;
    [Header("Для поиска референсов")]
    public string fireHoleName;
    public string headAimName;
    public string headName;
    public string targetName;
    public string uterName;

    // Start is called before the first frame update
    void Awake()
    {
        head = GameObject.Find(headName);
        headAim = GameObject.Find(headAimName);
        target = GameObject.Find(targetName);
        uter = GameObject.Find(uterName);
        multAim = headAim.GetComponent<MultiAimConstraint>();
        fireHole = GameObject.Find(fireHoleName);
    }
    public void shoot() 
    {
        Debug.Log("shoots");
        GameObject b = Instantiate(bullet, fireHole.transform.position, head.transform.rotation);
        StartCoroutine(b.GetComponent<bulletMove>().startMove(bulletSpeed, bulletDur, dir));
    }
    public IEnumerator periodiclyShoot(float cooldown)
    {
        isShooting = true;
        while (isVisible) 
        {
            yield return new WaitForSeconds(cooldown);
            shoot();
            isShooting = false;
            yield return new WaitForSeconds(cooldown);
        }
        
        yield return null;
    }
    public void idle() 
    {
        
        isShooting = false;
        isVisible = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        dir = uter.transform.position - headAim.transform.position;
        Physics.Raycast(headAim.transform.position, dir, out hit, maxDist, mask);
        Debug.DrawLine(headAim.transform.position, uter.transform.position,Color.red);
        if (hit.collider != null) 
        {
            if (hit.collider.gameObject.name == uterName)
            {
                isVisible = true;
            }
            else 
            {
                idle();
            }
            
           // Debug.Log(hit.point + " " + hit.collider.gameObject.name);
        }
        else 
        {
            idle();
        }
        
        if (isVisible) 
        {
            if (!isShooting) StartCoroutine(periodiclyShoot(shootCooldown));
            target.transform.position = uter.transform.position;
        }
       foreach (GameObject obj in inactives) 
        {
            Debug.Log(obj.name + " dist " + Vector3.Distance(obj.transform.position, target.transform.position));
            if (Vector3.Distance(obj.transform.position, curInactive.transform.position) < minDistInactive) 
            {
                
                List<GameObject> rest = inactives;
                rest.Remove(curInactive);
                curInactive = rest[0];
            }
        } 
    }
}
