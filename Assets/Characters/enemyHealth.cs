using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : health
{
    public override void death()
    {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}
