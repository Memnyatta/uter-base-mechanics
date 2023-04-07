using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEditor;
using TMPro;
using I2.Loc;

public class PlayerInteraction : MonoBehaviour
{
    [Header("InteractableInfo")]
    public float sphereCastRadius = 0.5f;
    public int interactableLayerIndex;
    public GameObject lookObject;
    private PhysicsObject physicsObject;
    public LayerMask Mask;
    public float ThrowForce = 10;
    Animator UterAnim;

    [Header("Pickup")]
    [SerializeField] private Transform pickupParent;
    public GameObject currentlyPickedUpObject;
    private Rigidbody pickupRB;
    bool canPick;
    [Header("оформи референс на меню паузы")]
    public PauseMenu _pauMen;

    [Header("ObjectFollow")]
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 300f;
    [SerializeField] private float maxDistance = 10f;
    private float currentSpeed = 0f;
    private float currentDist = 0f;

    [Header("Rotation")]
    public float rotationSpeed = 100f;
    Quaternion lookRot;

    [Header("Text")]
    public GameObject _textObject;
    public Vector3 textPosition;
    public string TextEN = "PickUp";
    public string TextRU = "Поднять";

    [Header("Ray settings")]
    public Vector3 RayPosition = new Vector3(0, 0.13f, 0);
    float RayLengh;
    public float DistanceBeforeBreak;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (pickupParent != null) Gizmos.DrawSphere(pickupParent.position, 0.1f);
    }

    private void Awake()
    {
        MyNameIsUter controls = new MyNameIsUter();
        controls.Player.Enable();
        controls.Player.PickUp.started += InitiatePickup;
    }

    private void Start()
    {
        RayLengh = DistanceBeforeBreak;
        UterAnim = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position + RayPosition, transform.forward);
        Debug.DrawRay(transform.position + RayPosition, transform.forward * RayLengh, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, RayLengh, Mask))
        {
            lookObject = hit.collider.transform.root.gameObject;
            _textObject = lookObject.transform.GetChild(0).gameObject;
            if (LocalizationManager.CurrentLanguage == "English" && TextRU != "" && TextEN != "")
            {
                _textObject.GetComponent<TMP_Text>().text = TextEN;
            }
            else
            {
                _textObject.GetComponent<TMP_Text>().text = TextRU;
            }
            _textObject.GetComponent<TextPrefabScript>().MakeTextVisible();
        }
        else
        {
            if(lookObject != null)
            {
                _textObject.GetComponent<TextPrefabScript>().MakeTextInvisible();
            }
            lookObject = null;
        }

        if (currentlyPickedUpObject != null)
        {
            if (Vector3.Distance(new Vector3(currentlyPickedUpObject.transform.position.x, 0, currentlyPickedUpObject.transform.position.z), new Vector3(transform.position.x, 0, transform.position.z)) > DistanceBeforeBreak)
            {
                BreakConnection(true);
            }
        }
    }

    public void InitiatePickup(InputAction.CallbackContext context)
    {
        canPick = _pauMen.GameIsPaused;
        if (context.started && !canPick)
        {
            if (currentlyPickedUpObject == null)
            {
                if (lookObject != null)
                {
                    PickUpObject();
                }
            }
            else
            {
                BreakConnection(false);
            }
        }
    }


    private void FixedUpdate()
    {
        if (currentlyPickedUpObject != null)
        {
            currentDist = Vector3.Distance(pickupParent.position, pickupRB.position);
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDist / maxDistance);
            currentSpeed *= Time.fixedDeltaTime;
            Vector3 direction = pickupParent.position - pickupRB.position;
            pickupRB.velocity = direction.normalized * currentSpeed;
            //Rotation
            lookRot = Quaternion.LookRotation(transform.position - pickupRB.position);
            lookRot = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
            pickupRB.MoveRotation(lookRot);
        }

    }

    public void BreakConnection(bool forcedBreak)
    {
        GetComponent<ThirdPersonController>().canSprint = true;
        if (!forcedBreak)
        {
            currentlyPickedUpObject.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
        }
        currentlyPickedUpObject.layer = LayerMask.NameToLayer("Interactable");
        pickupRB.constraints = RigidbodyConstraints.None;
        currentlyPickedUpObject = null;
        physicsObject.pickedUp = false;
        currentDist = 0;
        if (UterAnim != null)
        {
            UterAnim.SetBool("isPicked", false);
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/DropSFX", transform.position);
    }

    public void PickUpObject()
    {
        GetComponent<ThirdPersonController>().canSprint = false;
        if (GetComponent<ThirdPersonController>().isSprinting)
        {
            GetComponent<ThirdPersonController>().SprintDisable();
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/PickupSFX", transform.position);
        if (UterAnim != null)
        {
            UterAnim.SetBool("isPicked", true);
        }
        physicsObject = lookObject.GetComponentInChildren<PhysicsObject>();
        currentlyPickedUpObject = lookObject;
        pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
        pickupRB.constraints = RigidbodyConstraints.FreezeRotation;
        physicsObject.playerInteractions = this;
        StartCoroutine(physicsObject.PickUp());
        currentlyPickedUpObject.layer = LayerMask.NameToLayer("Entity");
    }
}

