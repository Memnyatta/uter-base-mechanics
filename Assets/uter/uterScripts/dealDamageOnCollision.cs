using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyCharacterMovement;
public class dealDamageOnCollision : MonoBehaviour
{
    public string animTriggerName;
    


    
    public float delayBeforeDeleating; //��������� ����� ����� �������� ������ �� ����� ��������� ��� ��������
     bool canBeDeleted = false; //����� �� �� ������� ������
    public float forceAfterCol;

    public int damage;
    public bool isTemp; //��������� �� ������ �� ��������� �������
    public bool deletOutside; //��������� �� ������ ���� �� ���� �� �������
    public float destroyTime; //����� ������� ������ ��������
    
    [Header("�������� ��� ������ �������� ")]
    public string uterName;
    
    public string playerTag;
    

    [Header("�������� �� ���� ")]
   // public CharacterMovement cmUter;
    public IDamageable damagin;
    Animator anim;
    
    public float momentDel; //����� ������ ������� �� ����� ������� ������ ��� ��������
    
    public GameObject uter;
    Camera cam;
    public float momentWhenDestroyed;
    
    // Start is called before the first frame update
    void Awake()
    {
        onAwake();
    }
    public void dealDam() 
    {
        Debug.Log("������� ���� ����� " + name);
        //cmUter.AddForce((uter.transform.position - transform.position) * forceAfterCol);
        
        Debug.Log("damagin " + damagin);
        damagin.takeDamage(damage);
        if (anim != null) anim.SetTrigger(animTriggerName);
    }
    public void onAwake() 
    {
        momentDel = Time.time + delayBeforeDeleating;
        
        
        momentWhenDestroyed = Time.time + destroyTime;
        uter = GameObject.Find(uterName);
        
        damagin = uter.GetComponent<IDamageable>();
        // GameObject.FindGameObjectWithTag("Player").GetComponent<IDamageable>().takeDamage(1);
        if (GetComponent<Animator>() != null) anim = GetComponent<Animator>();
       // cmUter = uter.GetComponent<CharacterMovement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == playerTag /*|| collision.gameObject.transform.parent.tag == playerTag*/)
        {
            dealDam();
        }
        else 
        { 
            if (isTemp && canBeDeleted) { dest(); }
        }
    }
    private void OnTriggerEnter(Collider other)
{ 
        if (other.gameObject.tag == playerTag )
        {
            dealDam();
        }
        else 
        { 
            if (isTemp && canBeDeleted) 
            {
                dest(); 
            } 
        }
    }
    public void dest()
    {
       // Debug.Log(gameObject.name + " ���������");
       Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
      
        if (momentDel <= Time.time) { canBeDeleted = true; }
      
        if (cam != null) 
        {
            Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);
            bool screen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (!screen && deletOutside) { dest(); }
        }



        if ( Time.time > momentWhenDestroyed && isTemp)
        { 
            dest(); 
        }   
    }
}
