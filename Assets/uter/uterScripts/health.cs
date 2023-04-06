using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Uter's health script
public class health : MonoBehaviour, IDamageable
{
    

    public delegate void deathAction(); //Ивент смерти Утера
    public static event deathAction onDeath;

    public int pubMaxHealth; //ЧТобы изменять кол-во здоровья в инспекторе
    public int pubCurHealth; //Чтобы видеть, сколько у нас сейчас здоровья
   public int maxHealth { get; set; } = 3; //Максимальное кол-во жизней
    public int curHealth { get; set; } //Текущее кол-во жизней
    public float flashTime; //На сколько утер моргает после нанесения урона

    
    public float invDurationAfterDamage; //Сколько времени мы будем неуязвимы после нанесенного урона
    //float untilInvincible; //До какого момента мы будет неуязвимы
    //SkinnedMeshRenderer rendered; //Рендер частей утера
    public List<GameObject> renders; //Список рендеров Утера
    public bool pubIsInv;
    [Header("Изменять не надо")]
   // public bool pubIsInv;
    public bool isInvincible = false; //Чтобы делать Утера на секунду неуязвимым после получения урона
    public HPScript hpScript; //Скрипт для UI
    IEnumerator invincibleDelay(float time) 
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }
    //Моделька Утера мигает при получении урона
    IEnumerator blink() 
    {
        if (renders.Count > 0) 
        {
            updateFlashable(); //Обновляем референсы ко всем объектам, которые делаем прозрачными
                               //Три повторения одного и того же кода с включением/выключением рендера объектов
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

    //Делает Утера неуязвимым на некоторе время
    public void makeInv(float time) 
    {
        StartCoroutine(invincibleDelay(time));
    }

    //Наносится урон
    public void takeDamage(int damage) 
    {
  //      hpScript.MinusHP(); //Отнимаем одну единицу ХП в UI

    if (curHealth > damage && isInvincible == false) 
        {
            curHealth -= damage;
            //Debug.Log("Получили урон, осталось здоровья: " + curHealth);
            makeInv(invDurationAfterDamage);
            StartCoroutine("blink");
        }
        if (curHealth <= damage && isInvincible == false) { death(); }
       
    }

    //Наносится лечение
    public void takeHeal(int heal)
    {
     //  hpScript.PlusHP(); //Прибавляем одну единицу ХП в UI

        if (curHealth < maxHealth)
        {
            curHealth += heal;
            //Debug.Log("Получили лечение, осталось здоровья: " + curHealth);
        }
        
    }

    //Смерть Утера
    public void death() 
    {
        if (onDeath != null) { Debug.Log("Утер умер"); onDeath(); }
       
    }

    private void Start()
    {
        maxHealth = pubMaxHealth;
        curHealth = maxHealth;
        updateFlashable();

       
    }
    void updateFlashable() 
    {
        foreach (Transform form in transform) //Проверяем каждую часть тела Утера и добавляем её в список, чтобы потом её включить/выключить 
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
