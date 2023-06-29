using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activation : MonoBehaviour
{
    public GameObject obj;
    
    public void activate() { obj.SetActive(true); }

    public void deactivate() { obj.SetActive(false); }
}
