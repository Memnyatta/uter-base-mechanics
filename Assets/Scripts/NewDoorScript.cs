using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using I2.Loc;
using TMPro;

public class NewDoorScript : MonoBehaviour
{
    public Vector3 RayPosition = new Vector3(0, 0.13f, 0);
    public float RayLengh = 1.08f;
    [SerializeField]
    DoorScript doorScript;

    private void Awake()
    {
        MyNameIsUter _controls = new MyNameIsUter();
        _controls.Player.Enable();
        _controls.Player.Interact.started += DoorForward;
    }

    void DoorForward(InputAction.CallbackContext context)
    {
        if(doorScript != null)
        {
            doorScript.MoveDoor();
        }
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + RayPosition, transform.forward);

        if (Physics.Raycast(ray, out hit, RayLengh))
        {
            if (hit.transform.tag == "Door")
            {
                doorScript = hit.transform.GetComponent<DoorScript>();
                doorScript.textScript.MakeTextVisible();
            }
        }
        else if (doorScript != null)
        {
            doorScript.textScript.MakeTextInvisible();
            doorScript = null;
        }

        Debug.DrawRay(transform.position + RayPosition, transform.forward * RayLengh, Color.green);
    }
}
