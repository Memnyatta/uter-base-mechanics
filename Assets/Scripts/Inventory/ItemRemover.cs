using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemRemover : MonoBehaviour
{
    public GameObject TextPrefab;
    public string TextEN;
    public string TextRU;
    [SerializeField]
    ItemCollector _collector;
    bool canRemove;

    private void Awake()
    {
        canRemove= false;
        MyNameIsUter _controls= new MyNameIsUter();
        _controls.Player.Enable();
        _controls.Player.PickUp.started += RemoveItem;
    }

    void RemoveItem(InputAction.CallbackContext context)
    {
        if (canRemove)
        {
            Remove();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canRemove= true;
            _collector = other.GetComponent<ItemCollector>();
            if (LocalizationManager.CurrentLanguage == "English" && TextRU != "" && TextEN != "")
            {
                TextPrefab.GetComponent<TMP_Text>().text = TextEN;
            }
            else
            {
                TextPrefab.GetComponent<TMP_Text>().text = TextRU;
            }
            transform.GetChild(0).GetComponent<TextPrefabScript>().MakeTextVisible();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canRemove= false;
            _collector = null;
            transform.GetChild(0).GetComponent<TextPrefabScript>().MakeTextInvisible();
        }
    }

    void Remove()
    {
        if (_collector._inventory.items.Count > 0)
        {
            _collector._inventory.RemoveItem();
        }
    }
}
