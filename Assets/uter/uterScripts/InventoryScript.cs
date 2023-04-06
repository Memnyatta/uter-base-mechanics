using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    private Transform ItemPos;
    private float ItemPosNew;
    private float ItemPosTarget = 0.1f;
    public Animator UterAnim;
    public Animator PickUPUI;
    public Sprite ObjIcon;
    public InventoryManager InvMan;

    private bool CanPick;

    private float targetDissolveValue = 0f;
    private float currentDissolveValue = 1f;
    float currentVelocity;
    float currentVelocity2;
    private bool isPicked;

    Material DissolveMat;


    private void Awake()
    {
        DissolveMat = GetComponent<Renderer>().material;
        ItemPos = GetComponent<Transform>();
    }
    private void Start()
    {
        DissolveMat.SetFloat("_Opacity", 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUPUI.SetBool("isEnter", true);
            CanPick = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUPUI.SetBool("isEnter", false);
            CanPick = false;
        }
    }

    private void Update()
    {
        if (CanPick && Input.GetKeyDown(KeyCode.F) && !InvMan.InvFull && !InvMan.Paused)
        {
            UterAnim.SetTrigger("Pick");
            PickObject();
            isPicked = true;
        }
        else if (CanPick && Input.GetKeyDown(KeyCode.F) && InvMan.InvFull && !InvMan.Paused)
        {
            Debug.LogError("INV FULL");
        }

        if (isPicked)
        {
            currentDissolveValue = Mathf.SmoothDamp(currentDissolveValue, targetDissolveValue, ref currentVelocity, 50 * Time.deltaTime);
            ItemPosNew = Mathf.SmoothDamp(ItemPosNew, ItemPosTarget, ref currentVelocity2, 500 * Time.deltaTime);

            PickUPUI.SetBool("isEnter", false);
            CanPick = false;
        }

        if (currentDissolveValue <= targetDissolveValue)
        {
            isPicked = false;

            PickUPUI.SetBool("isEnter", false);
            CanPick = false;
        }

        DissolveMat.SetFloat("_Opacity", currentDissolveValue);
        ItemPos.position = ItemPos.position + new Vector3(0f, 0f + ItemPosNew, 0f);
    }

    public void PickObject()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/CollectItem", transform.position);
        currentDissolveValue = 1f;

        if (InvMan.ItemImage1.sprite == null)
        {
            InvMan.ItemImage1.sprite = ObjIcon;
        }
        else if (InvMan.ItemImage2.sprite == null)
        {
            InvMan.ItemImage2.sprite = ObjIcon;
        }
        else if (InvMan.ItemImage3.sprite == null)
        {
            InvMan.ItemImage3.sprite = ObjIcon;
        }
        else if (InvMan.ItemImage4.sprite == null)
        {
            InvMan.ItemImage4.sprite = ObjIcon;
        }
    }
}
