using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCont : MonoBehaviour
{
    public float s;
    public  int c;
    public  float tBB;

    public CharacterController charCont;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        charCont = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        StartCoroutine(startMove(s,Vector3.forward,c,tBB));


    }
    IEnumerator startMove(float speed,Vector3 dir, int cycles,float timeBetweenBoost) 
    {
        
        for (int i = 0; i < cycles; i++) 
        {
            Debug.Log("moved");
            yield return new WaitForSeconds(timeBetweenBoost);
            charCont.Move(dir * speed);
        }
        yield return null;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
