using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallCutoutCheck : MonoBehaviour
{
    public Material CutOutMat;
    public LayerMask mask;
    public GameObject GameObj;
    public GameObject Uter;

    public float distanceCheck;

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(Uter.transform.position, transform.position);
        distanceCheck = distance;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);

        if (Physics.Raycast(ray, out hit, distance, mask))
        {
            GameObj = hit.collider.gameObject;
            CutOutMat = GameObj.GetComponent<Renderer>().material;
            if (Physics.Linecast(transform.position, Uter.transform.position, mask))
            {
                CutOutMat.DOFloat(0.14f, "_FalloffSize", 0.3f);
            }
        }
        else if (CutOutMat != null)
        {
            CutOutMat.DOFloat(1f, "_FalloffSize", 0.3f);
            Invoke("ClearMat", 0.3f);
        }
    }
    void ClearMat()
    {
        GameObj = null;
        CutOutMat = null;
    }
}
