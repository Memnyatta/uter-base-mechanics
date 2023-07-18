using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableRobotDel : MonoBehaviour, IThrowable
{
    
    public float dragDurSpin;
    public float spinClosingForce;
    public float rotateSpeed;
    public float dragOffset;
    public float yOffset;
    public string spinBool;
    public string playerName;
    public string arrowOName;
    public List<string> plTags;
    public List<string> enemyTags;
    [Header("Для просмотра")]
    public Rigidbody rb;
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
        if (isSpinning && Input.GetButtonUp("Fire1"))
        {
            stopSpin();
        }
        if (canSpin && Input.GetButtonUp("Fire1")) 
        {
            startSpin();
        }
        
        if (anim.GetBool(spinBool) && isSpinning) 
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
