using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int maxHealth { get; set; } //������������ ���-�� ������
    int curHealth { get; set; } //������� ���-�� ������

    public void takeDamage(int damage);
    public void takeHeal(int heal);
    public void death();
    public void makeInv(float time);
}
public interface ITurnOff
{
 
}

