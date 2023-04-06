using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public int id; //���� ��������
    public string name; //��� ��������
    public GameObject model; //3� ������
    public GameObject icon; //������ � ���������
   // public bool isAble; //������� �� �� ��� �����
    public Animator anim;

}
