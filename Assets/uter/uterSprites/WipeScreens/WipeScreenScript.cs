using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipeScreenScript : MonoBehaviour
{ //UI transitions between certain things. This covers the screen

    public List<string> functions; //Какие функции надо выполнить после перехода

    public delegate void wipeAction();
    public static event wipeAction onWipe;

    public float delay; //Скорость задержки между переходами

    //Объекты переходов
    GameObject downToUp;
    GameObject upToDown;
    GameObject rightToLeft;
    GameObject leftToRight;
    GameObject toCenter;

    //Аниматоры переходов
    Animator downToUpA;
    Animator upToDownA;
    Animator rightToLeftA;
    Animator leftToRightA;
    Animator toCenterA;

    
    public void evented() 
    {
        if (onWipe != null) onWipe();
    }
    private void Update()
    {
        if (Input.GetKeyDown("x")) { downToUpFunc(); }
        if (Input.GetKeyDown("c")) { upToDownFunc(); }
        if (Input.GetKeyDown("v")) { rightToLeftFunc(); }
        if (Input.GetKeyDown("b")) { leftToRightFunc(); }
        if (Input.GetKeyDown("n")) { toCenterunc(); }
    }
    public void downToUpFunc() 
    {
        downToUpA.SetBool("open", true);
        downToUpA.SetFloat("delay", delay);
    }
    public void upToDownFunc()
    {
        upToDownA.SetBool("open", true);
        upToDownA.SetFloat("delay", delay);
    }
    public void rightToLeftFunc()
    {
        rightToLeftA.SetBool("open", true);
        rightToLeftA.SetFloat("delay", delay);
    }
    public void leftToRightFunc()
    {
        leftToRightA.SetBool("open", true);
        leftToRightA.SetFloat("delay", delay);
    }
    public void toCenterunc()
    {
        toCenterA.SetBool("open", true);
        toCenterA.SetFloat("delay", delay);
    }
    private void Start()
    {
        
        startUp();
    }
    void startUp() 
    {
        downToUp = transform.Find("DownToUp").gameObject;
        upToDown = transform.Find("UpToDown").gameObject;
        rightToLeft = transform.Find("RightToLeft").gameObject;
        leftToRight = transform.Find("LeftToRight").gameObject;
        toCenter = transform.Find("ToCenter").gameObject;

        downToUpA = downToUp.GetComponent<Animator>();
        upToDownA = upToDown.GetComponent<Animator>();
        rightToLeftA = rightToLeft.GetComponent<Animator>();
        leftToRightA = leftToRight.GetComponent<Animator>();
        toCenterA = toCenter.GetComponent<Animator>();
    }
}
