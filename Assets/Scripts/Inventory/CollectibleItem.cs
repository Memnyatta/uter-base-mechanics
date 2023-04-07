using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using I2.Loc;
using DG.Tweening;

public class CollectibleItem : MonoBehaviour
{
    public string _name;
    public Sprite _sprite;
    public GameObject TextPrefab;
    public string TextRU;
    public string TextEN;
    public bool canPick;
    ItemCollector _itemCollector;

    public CollectibleItem(string _name, Sprite _sprite)
    {
        this._name = _name;
        this._sprite = _sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _itemCollector = other.GetComponent<ItemCollector>();
            _itemCollector._item = this;
            if (LocalizationManager.CurrentLanguage == "English" && TextRU != "" && TextEN != "")
            {
                TextPrefab.GetComponent<TMP_Text>().text = TextEN;
            }
            else
            {
                TextPrefab.GetComponent<TMP_Text>().text = TextRU;
            }
            transform.GetChild(0).GetComponent<TextPrefabScript>().MakeTextVisible();
            canPick = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {      
        if (other.tag == "Player")
        {
            if (_itemCollector != null)
            {
                _itemCollector._item = null;
                _itemCollector = null;
            }
            transform.GetChild(0).GetComponent<TextPrefabScript>().MakeTextInvisible();
            canPick = false;
        }
    }

    public void PickUobject()
    {
        transform.GetChild(0).GetComponent<TextPrefabScript>().MakeTextInvisible();
        canPick = false;
        GetComponent<Renderer>().material.DOFloat(0, "_Opacity", 0.3f);
        Invoke("DestroyObject", 0.3f);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
