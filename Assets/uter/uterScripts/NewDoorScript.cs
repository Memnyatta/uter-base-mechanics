using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewDoorScript : MonoBehaviour
{
    


    public GameObject Door;
    public Vector3 RayPosition = new Vector3(0,0.13f,0);
    public float RayLengh = 1.08f;
    GameObject text;

    private void Update()
    {
        
        RaycastHit hit;
        Ray ray = new Ray(transform.position + RayPosition, transform.forward);

        if (Physics.Raycast(ray, out hit, RayLengh))
        {
            if (hit.transform.tag == "Door")
            {
                text = hit.transform.GetChild(0).gameObject;
                text.GetComponent<TMPro.TMP_Text>().DOColor(new Color32(255, 255, 255, 255), 0.3f);
            }
        }
        else
        {
            if (text != null)
            {
                text.GetComponent<TMPro.TMP_Text>().DOColor(new Color32(255, 255, 255, 0), 0.3f);
                text = null;
            }
        }

        Debug.DrawRay(transform.position + RayPosition, transform.forward * RayLengh, Color.green);
        if (Input.GetKey(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, RayLengh))
            {
                if (hit.transform.tag == "Door")
                {
                    Door = hit.transform.gameObject;
                    Door.GetComponent<Rigidbody>().AddForce(hit.normal);
                }
            }
        }
        else if (Door != null)
        {
            Door = null;
        }
    }
}
