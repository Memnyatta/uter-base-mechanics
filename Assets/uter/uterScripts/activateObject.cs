using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateObject : MonoBehaviour
{
    
     
    public GameObject needed;


    GameObject cur;
    GameObject next;
    // Start is called before the first frame update
    void Awake()
    {

    }
    public GameObject findObject(List<string> path) 
    {
        
        for (int i = 0; i < path.Count - 1; i++)
        {
            cur = transform.Find(path[i]).gameObject;
            next = cur.transform.Find(path[i + 1]).gameObject;
            
        }
        return next;
    }
    public void activate(string path) 
    {
        List<string> ppath = new List<string>(path.Split(','));
        findObject(ppath).SetActive(true);
        

    }
    public void deActivate(string path)
    {
        List<string> ppath = new List<string>(path.Split(','));
        findObject(ppath).SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
