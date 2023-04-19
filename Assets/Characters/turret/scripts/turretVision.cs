using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class turretVision : MonoBehaviour
{
    public LayerMask mask;
    public float maxDist;
    public float shootCooldown;
    [Range(0.0f, 100.0f)]
    public float turnSpeed;
    [Header("��� ���������")]
    public bool isVisible;
    public bool isShooting;
    public RaycastHit hit;
    public Vector3 dir;
    [Header("���������")]
    public GameObject headAim;
    public GameObject target;
    public GameObject uter;
    public MultiAimConstraint multAim;
    [Header("��� ������ ����������")]
    public string headAimName;
    public string targetName;
    public string uterName;

    // Start is called before the first frame update
    void Awake()
    {
        headAim = GameObject.Find(headAimName);
        target = GameObject.Find(targetName);
        uter = GameObject.Find(uterName);
        multAim = headAim.GetComponent<MultiAimConstraint>();
    }
    public void shoot() 
    {
        Debug.Log("shoots");
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
            else { isShooting = false; isVisible = false; }
            
           // Debug.Log(hit.point + " " + hit.collider.gameObject.name);
        }
        
        if (isVisible) 
        {
            if (!isShooting) StartCoroutine(periodiclyShoot(shootCooldown));
            target.transform.position = uter.transform.position;
        }
        
    }
}
