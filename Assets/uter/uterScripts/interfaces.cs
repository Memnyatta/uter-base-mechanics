using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int maxHealth { get; set; } //Максимальное кол-во жизней
    int curHealth { get; set; } //Текущее кол-во жизней

    public void takeDamage(int damage);
    public void takeHeal(int heal);
    public void death();
    public void makeInv(float time);
}
public interface ITurnOff
{
 
}

