using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public int id; //Айди предмета
    public string name; //Имя предмета
    public GameObject model; //3д Модель
    public GameObject icon; //Иконка в инвентаре
   // public bool isAble; //Открыли ли мы эту шапку
    public Animator anim;

}
