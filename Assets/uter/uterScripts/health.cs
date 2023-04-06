using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Uter's health script
public class health : MonoBehaviour, IDamageable
{
    

    public delegate void deathAction(); //����� ������ �����
    public static event deathAction onDeath;

    public int pubMaxHealth; //����� �������� ���-�� �������� � ����������
    public int pubCurHealth; //����� ������, ������� � ��� ������ ��������
   public int maxHealth { get; set; } = 3; //������������ ���-�� ������
    public int curHealth { get; set; } //������� ���-�� ������
    public float flashTime; //�� ������� ���� ������� ����� ��������� �����

    
    public float invDurationAfterDamage; //������� ������� �� ����� ��������� ����� ����������� �����
    //float untilInvincible; //�� ������ ������� �� ����� ���������
    //SkinnedMeshRenderer rendered; //������ ������ �����
    public List<GameObject> renders; //������ �������� �����
    public bool pubIsInv;
    [Header("�������� �� ����")]
   // public bool pubIsInv;
    public bool isInvincible = false; //����� ������ ����� �� ������� ���������� ����� ��������� �����
    public HPScript hpScript; //������ ��� UI
    IEnumerator invincibleDelay(float time) 
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }
    //�������� ����� ������ ��� ��������� �����
    IEnumerator blink() 
    {
        if (renders.Count > 0) 
        {
            updateFlashable(); //��������� ��������� �� ���� ��������, ������� ������ �����������
                               //��� ���������� ������ � ���� �� ���� � ����������/����������� ������� ��������
            foreach (GameObject objected in renders) { objected.SetActive(false); }
            yield return new WaitForSeconds(flashTime);
            foreach (GameObject objected in renders) { objected.SetActive(true); }
            yield return new WaitForSeconds(flashTime);


            foreach (GameObject objected in renders) { objected.SetActive(false); }
            yield return new WaitForSeconds(flashTime);
            foreach (GameObject objected in renders) { objected.SetActive(true); }
            yield return new WaitForSeconds(flashTime);


            foreach (GameObject objected in renders) { objected.SetActive(false); }
            yield return new WaitForSeconds(flashTime);
            foreach (GameObject objected in renders) { objected.SetActive(true); }
            yield return new WaitForSeconds(flashTime);
            yield break;
        }
        

    }

    //������ ����� ���������� �� �������� �����
    public void makeInv(float time) 
    {
        StartCoroutine(invincibleDelay(time));
    }

    //��������� ����
    public void takeDamage(int damage) 
    {
  //      hpScript.MinusHP(); //�������� ���� ������� �� � UI

    if (curHealth > damage && isInvincible == false) 
        {
            curHealth -= damage;
            //Debug.Log("�������� ����, �������� ��������: " + curHealth);
            makeInv(invDurationAfterDamage);
            StartCoroutine("blink");
        }
        if (curHealth <= damage && isInvincible == false) { death(); }
       
    }

    //��������� �������
    public void takeHeal(int heal)
    {
     //  hpScript.PlusHP(); //���������� ���� ������� �� � UI

        if (curHealth < maxHealth)
        {
            curHealth += heal;
            //Debug.Log("�������� �������, �������� ��������: " + curHealth);
        }
        
    }

    //������ �����
    public void death() 
    {
        if (onDeath != null) { Debug.Log("���� ����"); onDeath(); }
       
    }

    private void Start()
    {
        maxHealth = pubMaxHealth;
        curHealth = maxHealth;
        updateFlashable();

       
    }
    void updateFlashable() 
    {
        foreach (Transform form in transform) //��������� ������ ����� ���� ����� � ��������� � � ������, ����� ����� � ��������/��������� 
        {
            if (form.gameObject.GetComponent<SkinnedMeshRenderer>() != null && form.gameObject.activeSelf)
            {
                
                renders.Add(form.gameObject);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { takeDamage(1); }
        //if (Time.time > untilInvincible) { isInvincible = false; }
        //pubIsInv = isInvincible;
        pubCurHealth = curHealth;
        pubIsInv = isInvincible;
    }
}
