using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretExpl : MonoBehaviour
{
    public GameObject explPart;
    private void OnEnable()
    {
        damageOnTrigger.onCol += spawnPart; 
       
    }

    private void OnDisable()
    {
        damageOnTrigger.onCol -= spawnPart;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void spawnPart(GameObject t) 
    {
        //Debug.Log("spawnPart");
        Instantiate(explPart, t.transform.position, t.transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
