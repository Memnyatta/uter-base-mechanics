using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class turretVision : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float turnSpeed;
    [Header("Референсы")]
    public GameObject target;
    public MultiAimConstraint multAim;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
