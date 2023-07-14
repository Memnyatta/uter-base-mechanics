using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickableRobotDel : MonoBehaviour, IThrowable
{
    public float dragDurSpin;
    public float spinClosingForce;

    public List<string> plTags;
    public string spinBool;
    public string playerName;
    [Header("Для просмотра")]
    public bool isSpinning;
    public GameObject uter;
    public Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        uter = GameObject.Find(playerName);
        anim = uter.GetComponent<Animator>();
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
        while(anim.GetBool(spinBool) && isSpinning) 
        {
        
        }
    }
}
