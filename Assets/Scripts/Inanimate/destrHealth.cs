using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destrHealth : health
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void death()
    {
        Debug.Log(gameObject.name + " destroyed");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
