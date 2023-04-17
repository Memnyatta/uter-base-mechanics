using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    public float testS;
    public float testT;
    public Vector3 testD;
    public CharacterController charCont;
    public float nextTime;
    // Start is called before the first frame update
    void Awake()
    {
        charCont = GetComponent<CharacterController>();
        StartCoroutine(startMove(testS, testT, testD));
    }
    public IEnumerator startMove(float speed,float time,Vector3 dir) 
    {
        Debug.Log("startMove");
        nextTime = Time.time + time;
        
        for (int i = 0; i < time; i++) { charCont.Move(dir * speed * Time.deltaTime); }
        
        
        
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
