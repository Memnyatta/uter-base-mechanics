using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    [Header("--------")]
    public bool canMove;
    public float nextTime;
    public CharacterController charCont;
    Vector3 direction;
    float speed;

    void Awake()
    {
        charCont = GetComponent<CharacterController>();      
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void DirSpeed(Vector3 dir, float sp)
    {
        direction = dir;
        speed = sp;
    }

    private void Update()
    {
        if (canMove)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
            charCont.Move(direction * speed * Time.deltaTime);
        }
    }
}
