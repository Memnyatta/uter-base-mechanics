using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    //public float testS;
    //public float testT;
    //public Vector3 testD;

    

    [Header("--------")]

    
    public bool canMove;
    public float nextTime;
    public CharacterController charCont;
    private void dest()
    {
        
        
        //Destroy(gameObject,1);
    }
    // Start is called before the first frame update
    void Awake()
    {
        charCont = GetComponent<CharacterController>();
        //StartCoroutine(startMove(testS, testT, testD));
    }
    public IEnumerator startMove(float speed, float time, Vector3 dir)
    {

        nextTime = Time.time + time;
        while (canMove)
        {

            //Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            //transform.Rotate(dir);

            
            transform.LookAt(transform.position + dir);
            

            charCont.Move(dir * speed * Time.deltaTime);
            yield return new WaitForSeconds(0.005f);
            if (Time.time > nextTime) { canMove = false; }
        }
        dest();
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
