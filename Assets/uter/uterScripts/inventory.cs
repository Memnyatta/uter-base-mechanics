using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    public List<int> givenIds; //Айди предметов, которые у нас есть

    public PauseMenu pauseMenu;
    public bool inventoryOpened; //Открыли ли мы инвентарь
    public Animator anim; //Аниматор инвентаря
    public GameObject InventoryBase;
    public delegate void standartKepka(); //Ивент при надевании стандартной кепки
    public static event standartKepka onStandartKepka;

    public delegate void froggerzKepka(); //Ивент при надевании фроггерз кепки
    public static event froggerzKepka onFroggerzKepka;

    public delegate void haggerzKepka(); //Ивент при надевании хаггерз кепки
    public static event haggerzKepka onHaggerzKepka;

    Item itemm;

    public List<Item> items;
    public List<int> ids; //Айди всех предметов, что есть
    //Выдает нужный предмет под айди
    public void giveItem(int id)
    {

        foreach (Item item in items)
        {
            if (item.id == id)
            {
                item.anim = item.icon.GetComponent<Animator>();
                item.anim.SetBool("isAble", true);
            }
        }
    }
    //Выдает все кепки (для тестов)
    public void giveAllitems() 
    {
    foreach (Item x in items) 
        {
            givenIds.Add(x.id);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown("k")) { giveAllitems(); }
        if (Input.GetButtonDown("inventory")) 
        { 
            inventoryOpened = !inventoryOpened;
            //activateInv();
        }

        if (inventoryOpened)
        {
            anim.SetBool("openInventory", true);
            Invoke("InvOpen", 0.2f);
        }
        else
        {
            anim.SetBool("openInventory", false);
            Invoke("InvClose", 0.2f);
        }

    }
    //Закрывает инвентарь
    void closing() 
    {
        inventoryOpened = false;
        InventoryBase.SetActive(false);
        pauseMenu.MouseDisable();
    }
    void opening() {  }
    private void OnEnable()
    {
        PauseMenu.onOpenedMenu += closing;
    }
    private void OnDisable()
    {
        PauseMenu.onOpenedMenu -= closing;
    }
    public void InvClose()
    {
        InventoryBase.SetActive(false);
        pauseMenu.MouseDisable();
        
    }

    public void InvOpen()
    {
        foreach (int x in givenIds) 
        {
            giveItem(x);
        }
        
        InventoryBase.SetActive(true);
        pauseMenu.MouseEnable();
    }

   /* void activateInv() 
    {
        Debug.Log("Открываем инвентарь");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }*/
    private void Start()
    {
      

        InvClose();
        givenIds.Add(1);
       // giveItem(1);
        //giveItem(0);
    }
    
    //Выбираем кепку Фроггерза
    public void pickFrogerz()
    {
        bool canSwitch = false;
        foreach (Item item in items)
        {

            if (item.id == 1 && item.anim.GetBool("isAble"))

            {
                canSwitch = true;
            }

        }
        if (canSwitch)
        {
            foreach (Item item in items)
            {

                if (item.id == 1)

                {
                    itemm = item;
                    Debug.Log("Выбираем фроггез");
                    item.model.SetActive(true);
                    if (onFroggerzKepka != null)
                        onFroggerzKepka();
                    canSwitch = false;
                }
                else { item.model.SetActive(false); }

            }
        }


    }
    //Выбираем кепку по стандарту
    public void pickStandart()
    {
        bool canSwitch = false;
        foreach (Item item in items)
        {

            if (item.id == 0 && item.anim.GetBool("isAble"))

            {
                canSwitch = true;
            }

        }
        if (canSwitch)
        {
            foreach (Item item in items)
            {

                if (item.id == 0 && item.anim.GetBool("isAble"))

                {
                    itemm = item;
                    Debug.Log("Выбираем стандартную");
                    item.model.SetActive(true);
                    if (onStandartKepka != null)
                        onStandartKepka();
                    canSwitch = false;
                }
                else { item.model.SetActive(false); }

            }
        }
        

    }
    public void pickHaxerz()
    {
        bool canSwitch = false;
        foreach (Item item in items)
        {

            if (item.id == 2 && item.anim.GetBool("isAble"))

            {
                canSwitch = true;
            }

        }
        if (canSwitch)
        {
            foreach (Item item in items)
            {

                if (item.id == 2 && item.anim.GetBool("isAble"))

                {
                    itemm = item;
                    Debug.Log("Выбираем стандартную");
                    item.model.SetActive(true);
                    if (onHaggerzKepka != null)
                        onHaggerzKepka();
                    canSwitch = false;
                }
                else { item.model.SetActive(false); }

            }
        }


    }
}
