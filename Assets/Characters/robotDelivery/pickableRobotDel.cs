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
    public List<string> plTags;
    public string spinBool;
    public string playerName;
    [Header("Для просмотра")]
    public Rigidbody rb;
    public bool isSpinning;
    public GameObject uter;
    public Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        uter = GameObject.Find(playerName);
        anim = uter.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    public void startSpin() 
    {
        anim.SetBool(spinBool, true);
        isSpinning = true;
    }
    public void stopSpin()
    {
        anim.SetBool(spinBool, false);
        isSpinning = false;
    }
    private void OnTriggerEnter(Collider other)
    {    
         if (plTags.Contains(other.gameObject.tag) && !anim.GetBool(spinBool))
        {
                startSpin();
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (anim.GetBool(spinBool)) 
        {
            uter.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);

            Vector3 dragToPos = uter.transform.position + new Vector3(0, yOffset, 0) + uter.transform.forward * dragOffset;
            rb.AddForce((dragToPos - transform.position) * spinClosingForce, ForceMode.VelocityChange);
        }
        
        
    }
}
