using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableRobotDel : MonoBehaviour, IThrowable
{
    public Vector3 throwOffset;
    [Header("Float-ы")]
    public float throwForce;
    public float dragDurSpin;
    public float spinClosingForce;
    public float rotateSpeed;
    public float dragOffset;
    public float yOffset;
    [Header("String-и")]
    public string spinBool;
    public string playerName;
    public string arrowOName;
    public List<string> plTags;
    public List<string> enemyTags;
    [Header("Для просмотра")]
    public BoxCollider nonTriggerCol;
    public Rigidbody rb;
    public bool hasThrown;
    public bool canDealDam;
    public bool canSpin;
    public bool isSpinning;
    public Animator anim;
    public GameObject uter;
    public GameObject arrowObj;
    // Start is called before the first frame update
    void Awake()
    {
        uter = GameObject.Find(playerName);
        anim = uter.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        arrowObj = GameObject.Find(arrowOName);
    }
    public IEnumerator goForw(float dur)
    {
        Debug.Log("Stopped spinnning");
        hasThrown = true;
        Vector3 nV3 = arrowObj.transform.position + arrowObj.transform.forward * throwForce + throwOffset;
        float nextTime = Time.time + dur;
        Vector3 dirr = nV3 - new Vector3(transform.position.x, nV3.y, transform.position.z);
        bool c = true;
        while (c)
        {
            if (Time.time > nextTime) { c = false;  break; }
            rb.AddForce(dirr);
        }
        //yield return new WaitForSeconds();
        
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
        //nonTriggerCol.isTrigger = true;
        Vector3 nV3 = arrowObj.transform.position + arrowObj.transform.forward * throwForce + throwOffset;
        
        rb.AddForce(nV3 - new Vector3(transform.position.x, nV3.y, transform.position.z));
    }
    private void OnTriggerEnter(Collider other)
    {    
         if (plTags.Contains(other.gameObject.tag) && !anim.GetBool(spinBool))
        {
            canSpin = true;
        }
        else { canSpin = false; }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            if (isSpinning && !hasThrown)
            {
                //Debug.Log("Stopped spinnning");
                stopSpin();
                StartCoroutine(goForw(10));
                //throwCorpse();

            }
            else if (canSpin && !hasThrown && !isSpinning) 
            {
                //Debug.Log("2");
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
        
        
    }
    public void arrowMove() 
    {
        arrowObj.transform.position = uter.transform.position;
        Vector3 arrowFrwrd;
        arrowFrwrd = uter.transform.position - new Vector3(transform.position.x, uter.transform.position.y, transform.position.z);
        arrowObj.transform.forward = arrowFrwrd;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    
}
