using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableRobotDel : MonoBehaviour, IThrowable
{
    public Vector3 throwOffset;
    [Header("Float-ы")]
    public float autoAimStrength;
    public float autoAimRadius;
    public float throwedDur;
    public float throwForce;
    public float dragDurSpin;
    public float spinClosingForce;
    public float rotateSpeed;
    public float dragOffset;
    public float yOffset;
    public float delayBeforeCanThrow;
    [Header("String-и")]
    public string spinBool;
    public string playerName;
    public string arrowOName;
    public List<string> tagsAfterThrowing;
    public List<string> plTags;
    public List<string> enemyTags;
    [Header("Для просмотра")]
    public damageOnCollision damOnCol;
    public Collider[] autoAimCols;
    public BoxCollider nonTriggerCol;
    public Rigidbody rb;
    public bool hasThrown;
    public bool canDealDam;
    public bool cnTw;
    public bool canSpin;
    public bool isSpinning;
    public Animator anim;
    public GameObject uter;
    public GameObject arrowObj;
    public Vector3 dirr;
    // Start is called before the first frame update
    void Awake()
    {
        uter = GameObject.Find(playerName);
        anim = uter.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        arrowObj = GameObject.Find(arrowOName);
        damOnCol = GetComponent<damageOnCollision>();
    }
    public IEnumerator cantThrow(float dur)
    {
        Debug.Log("Stopped spinnning");
        cnTw = false;
        yield return new WaitForSeconds(dur);
        cnTw = true;
        yield return null;
    }
    public IEnumerator goForw(float dur)
    {
        Debug.Log("Stopped spinnning");
        hasThrown = true;
        Vector3 nV3 = arrowObj.transform.position + arrowObj.transform.forward * throwForce + throwOffset;
        float nextTime = Time.time + dur;
        dirr = nV3 - new Vector3(transform.position.x, nV3.y, transform.position.z);
        yield return new WaitForSeconds(throwedDur);
        dirr = Vector3.zero;
        yield return null;
    }
    public void startSpin() 
    {
        anim.SetBool(spinBool, true);
        isSpinning = true;
    }
    public void stopSpin()
    {
       
        isSpinning = false;
        anim.SetBool(spinBool, false);
        
    }
    public void throwCorpse() 
    {
        hasThrown = true;

        Vector3 nV3 = arrowObj.transform.position + arrowObj.transform.forward * throwForce + throwOffset;
        
        rb.AddForce(nV3 - new Vector3(transform.position.x, nV3.y, transform.position.z));
    }
    private void OnTriggerEnter(Collider other)
    {    
         if (plTags.Contains(other.gameObject.tag))
        {
            canSpin = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (plTags.Contains(other.gameObject.tag))
        {
            canSpin = false;
        }
        

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            if (isSpinning && !hasThrown && cnTw)
            {
                //Debug.Log("Stopped spinnning");
                stopSpin();
                damOnCol.tags = tagsAfterThrowing;
                StartCoroutine(goForw(10));


            }
            else if (canSpin && !hasThrown && !isSpinning) 
            {
                //Debug.Log("2");
                StartCoroutine(cantThrow(delayBeforeCanThrow));
                startSpin();
            }
        }       
        if (anim.GetBool(spinBool) && isSpinning && !hasThrown) 
        {
            arrowMove();

            uter.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);

            Vector3 dragToPos = uter.transform.position + new Vector3(0, yOffset, 0) + uter.transform.forward * dragOffset;
            rb.AddForce((dragToPos - transform.position) * spinClosingForce, ForceMode.VelocityChange);
        }      
        if (dirr != Vector3.zero) 
        {
            autoAim();
            rb.AddForce(dirr* throwForce); 
        }
        Debug.DrawRay(transform.position, dirr * 999, Color.red);
    }
    public void autoAim()
    {
        autoAimCols = Physics.OverlapSphere(transform.position, autoAimRadius);
        GameObject clsSt = gameObject;
        float minDist = 999;
        foreach (Collider cl in autoAimCols) 
        {
        if (tagsAfterThrowing.Contains(cl.tag) && Vector3.Distance(cl.transform.position,transform.position) < minDist) 
            {
                clsSt = cl.gameObject;
                minDist = Vector3.Distance(cl.transform.position, transform.position);
            }
        }
        rb.AddForce((clsSt.transform.position - transform.position) * autoAimStrength);
    }
    public void arrowMove() 
    {
        arrowObj.transform.position = uter.transform.position;
        Vector3 arrowFrwrd;
        arrowFrwrd = uter.transform.position - new Vector3(transform.position.x, uter.transform.position.y, transform.position.z);
        arrowObj.transform.forward = arrowFrwrd;
    }

}
