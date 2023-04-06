using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPScript : MonoBehaviour
{
    
    public int HP = 3;

    [Space]
    public GameObject HP1;
    public GameObject HP2;
    public GameObject HP3;


    public void PlusHP()
    {
        HP += 1;
    }

    public void MinusHP()
    {
        HP -= 1;
    }

    void Update()
    {
        if (HP == 3)
        {
            HP1.SetActive(true);
            HP2.SetActive(true);
            HP3.SetActive(true);
        }
        else if (HP == 2)
        {
            HP1.SetActive(true);
            HP2.SetActive(true);
            HP3.SetActive(false);
        }
        else if (HP == 1)
        {
            HP1.SetActive(true);
            HP2.SetActive(false);
            HP3.SetActive(false);
        }
        else if (HP == 0)
        {
            HP1.SetActive(false);
            HP2.SetActive(false);
            HP3.SetActive(false);
        }
    }
}
