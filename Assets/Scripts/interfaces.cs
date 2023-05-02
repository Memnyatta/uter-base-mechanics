using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    
    public float maxHealth { get; }
    float curHealth { get; }
    bool isInvincible { get; }
    public IEnumerator makeInvincible(float time);
    public float invAftHit { get; set; }
    public void dealDamage(float damage, string source);
    public void dealHeal(float heal, string source);
    public void death();
}