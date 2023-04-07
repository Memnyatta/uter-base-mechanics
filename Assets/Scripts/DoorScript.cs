using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using TMPro;
using I2.Loc;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    Quaternion InitialPosition;
    [SerializeField]
    Quaternion EndPosition;
    bool doorIsOpen = false;
    [SerializeField]
    float velocity = 1;

    public TextPrefabScript textScript;

    [SerializeField]
    string TextRU;
    [SerializeField]
    string TextEN;

    public float trany;


    private void Awake()
    {
        LocalizationManager.OnLocalizeEvent += OnChangeLanguage;
    }

    private void Start()
    {
        InitialPosition = transform.rotation;
        doorIsOpen = false;
        if (LocalizationManager.CurrentLanguage == "Russian")
        {
            textScript.GetComponent<TMP_Text>().text = TextRU;
        }
        else
        {
            textScript.GetComponent<TMP_Text>().text = TextEN;
        }
    }

    void OnChangeLanguage()
    {
        if (LocalizationManager.CurrentLanguage == "Russian")
        {
            textScript.GetComponent<TMP_Text>().text= TextRU;
        }
        else
        {
            textScript.GetComponent<TMP_Text>().text = TextEN;
        }
    }

    [Button]
    void SavePosition()
    {
        EndPosition = transform.rotation;
    }

    private void Update()
    {
        trany = Math.Abs(transform.rotation.y);

        if (transform.rotation == EndPosition)
        {
            StopAllCoroutines();
            transform.rotation = EndPosition;
        }
        if (transform.rotation == InitialPosition)
        {
            StopAllCoroutines();
            transform.rotation = InitialPosition;
        }
    }

    public void MoveDoor()
    {
        if(!doorIsOpen)
        {
            StopAllCoroutines();
            StartCoroutine(OpenDoor());
            doorIsOpen = true;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(CloseDoor());
            doorIsOpen = false;
        }
    }

    IEnumerator OpenDoor()
    {
        while (true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, EndPosition, Time.deltaTime * velocity);
            //Debug.Log("Открываем дверь");
            yield return null;
        }
    }

    IEnumerator CloseDoor()
    {
        while (true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, InitialPosition, Time.deltaTime * velocity);
            //Debug.Log("Закрываем дверь");
            yield return null;
        }
    }
}

