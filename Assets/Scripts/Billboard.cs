using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject Object;
    private Transform theCam;

    void Awake()
    {
        Object = GameObject.FindWithTag("MainCamera");
    }

    private void Start()
    {
        theCam = Object.GetComponent<Transform>();
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - theCam.transform.position);
    }
}
