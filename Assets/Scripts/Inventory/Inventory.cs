using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour 
{
    [SerializeField]
    public List<CollectibleItem> items = new List<CollectibleItem>();
    [SerializeField]
    private List<Image> _slots = new List<Image>();

    private void Start()
    {
        foreach (var slots in _slots)
        {
            slots.sprite = null;
            slots.color = new Color32(255, 255, 255, 0);
        }
    }

    public void AddItem(CollectibleItem item)
    {
        items.Add(item);
        foreach (var slot in _slots)
        {
            if (slot.sprite == null)
            {
                slot.sprite = item._sprite;
                slot.color = new Color32(255, 255, 255, 255);
                break;
            }
            else
            {
                continue;
            }
        }
    }

    public void RemoveItem()
    {
        items.RemoveAt(items.Count - 1);
        for(int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].sprite == null)
            {
                _slots[i-1].sprite = null;
                _slots[i-1].color = Color.clear;
                break;
            }
        }
    }

    public List<CollectibleItem> GetItems()
    {
        Debug.Log(items.ToString());
        return items;
    }
}
