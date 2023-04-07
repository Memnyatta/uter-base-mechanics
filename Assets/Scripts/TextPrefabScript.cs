using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TextPrefabScript : MonoBehaviour
{
    public List<Sprite> _sprites = new List<Sprite>();
    public SpriteRenderer _buttonIcon;

    private void OnDestroy()
    {
        DOTween.CompleteAll();
    }

    private void Start()
    {
        OnIconChange("Keyboard:/Keyboard");
        GetComponent<TMP_Text>().color = Color.clear;
        _buttonIcon.color = Color.clear;
    }

    private void OnEnable()
    {
        DeviceChecker.IconChange += OnIconChange;
    }
    private void OnDisable()
    {
        DeviceChecker.IconChange -= OnIconChange;
    }

    public void MakeTextInvisible()
    {
        GetComponent<TMP_Text>().DOColor(new Color32(255, 255, 255, 0), 0.3f);
        _buttonIcon.DOColor(new Color32(255, 255, 255, 0), 0.3f);
    }

    public void MakeTextVisible()
    {
        GetComponent<TMP_Text>().DOColor(new Color32(255, 255, 255, 255), 0.3f);
        _buttonIcon.DOColor(new Color32(255, 255, 255, 255), 0.3f);
    }

    void OnIconChange(string deviceName)
    {
        switch (deviceName)
        {
            case "Keyboard:/Keyboard":
                _buttonIcon.sprite = _sprites[0];
                break;
            case "DualSenseGamepadHID:/DualSenseGamepadHID":
                _buttonIcon.sprite = _sprites[1];
                break;
        }
    }
}
