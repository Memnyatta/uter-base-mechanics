using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class enemyHealth : health
{
    [SerializeField] public UnityEvent onDeathEvent;
    public override void death()
    {
        onDeathEvent.Invoke();
    }
    public void dest() 
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        onUpdate();
    }
}
