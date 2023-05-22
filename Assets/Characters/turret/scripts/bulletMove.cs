using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    



    [Header("--------")]

    //public GameObject r;
    public bool canMove;
    public float nextTime;
    public CharacterController charCont;
    private void dest()
    {
        
        
        
    }
    // Start is called before the first frame update
    void Awake()
    {
        charCont = GetComponent<CharacterController>();
        //r = transform.Find("bulletBlaze").Find("r").gameObject;
        
    }
    public IEnumerator startMove(float speed, float time, Vector3 dir)
    {

        nextTime = Time.time + time;
        while (canMove)
        {

            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            //transform.Rotate(dir);


            transform.rotation = rotation;
            

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
