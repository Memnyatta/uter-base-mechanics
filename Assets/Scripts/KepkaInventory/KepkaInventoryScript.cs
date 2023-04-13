using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using NaughtyAttributes;

public class KepkaInventoryScript : MonoBehaviour
{
    [SerializeField]
    PauseMenu pauMen;
    [SerializeField]
    ScrollRect scrollRect;
    [SerializeField]
    RectTransform contentPanel;
    [SerializeField]
    float speed;
    [SerializeField]
    List<RectTransform> targets = new List<RectTransform>();
    [SerializeField]
    int currentKepka = 0;
    [SerializeField]
    GameObject KepkaInvContainer;
    [SerializeField, Header("Модельки кепок на утере")]
    List<GameObject> KepkaModels = new List<GameObject>();
    [SerializeField]
    ParticleSystem explosionVFX;

    Vector2 newPos;
    bool canMove;
    [SerializeField]
    bool InvIsOpen = false;
    int oldKepkaNum;
    MyNameIsUter controls = null;

    private void OnEnable()
    {
        KepkaInvContainer.transform.localScale = Vector3.zero;
        InvIsOpen = false;
        currentKepka = 1;
        controls = new MyNameIsUter();
        controls.UI.Enable();
        controls.UI.KepkaPlus.started += GoRight;
        controls.UI.KepkaMinus.started += GoLeft;
        controls.UI.OpenKepkaInv.started += InitializeKepkaInv;
    }

    private void OnDisable()
    {   
        controls.UI.Disable();
        controls.UI.KepkaPlus.started -= GoRight;
        controls.UI.KepkaMinus.started -= GoLeft;
        controls.UI.OpenKepkaInv.started -= InitializeKepkaInv;
        controls = null;
    }

    void InitializeKepkaInv(InputAction.CallbackContext context)
    {
        if (pauMen.GameIsPaused)
        {
            return;
        }

        if (!InvIsOpen)
        {
            oldKepkaNum = currentKepka;
            StopAllCoroutines();
            StartCoroutine(OpenInventory());
            pauMen.Pause(false);
            Debug.Log("Открываем инвентарь");
            InvIsOpen = true;
            ChooseKepka(targets[currentKepka]);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(CloseInventory());
            pauMen.Resume();
            Debug.Log("Закрываем инвентарь");
            InvIsOpen = false;
            if (currentKepka != oldKepkaNum)
            {
                explosionVFX.Play();
            }
        }
    }

    IEnumerator OpenInventory()
    {
        while(KepkaInvContainer.transform.localScale != new Vector3(1, 1, 1))
        {
            KepkaInvContainer.transform.localScale = Vector3.Lerp(KepkaInvContainer.transform.localScale, new Vector3(1, 1, 1), speed);
            Debug.Log("Open");
            yield return null;
        }
    }

    IEnumerator CloseInventory()
    {
        foreach (GameObject kepka in KepkaModels)
        {
            if (kepka.name == targets[currentKepka].name)
            {
                kepka.SetActive(true);
            }
            else
            {
                kepka.SetActive(false);
            }
        }
        while (KepkaInvContainer.transform.localScale != Vector3.zero)
        {
            KepkaInvContainer.transform.localScale = Vector3.Lerp(KepkaInvContainer.transform.localScale, Vector3.zero, speed);
            Debug.Log("Close");
            yield return null;
        }
    }

    void GoRight(InputAction.CallbackContext context)
    {
        if (!InvIsOpen || pauMen.GameIsPaused)
        {
            return;
        }

        if(currentKepka != targets.Count - 1)
        {
            currentKepka += 1;
        }
        else
        {
            currentKepka = 0;
        }
        ChooseKepka(targets[currentKepka]);
    }

    void GoLeft(InputAction.CallbackContext context)
    {
        if (!InvIsOpen || pauMen.GameIsPaused)
        {
            return;
        }

        if (currentKepka != 0)
        {
            currentKepka -= 1;
        }
        else
        {
            currentKepka = targets.Count - 1;
        }
        ChooseKepka(targets[currentKepka]);
    }

    private void Start()
    {
        canMove = false;
        for (int i = 1; i < contentPanel.transform.childCount - 1; i++)
        {
            if (contentPanel.transform.GetChild(i).gameObject.activeSelf)
            {
                targets.Add(contentPanel.transform.GetChild(i).GetComponent<RectTransform>());
            }
        }

        foreach (RectTransform image in targets)
        {
            image.GetChild(0).GetComponent<Image>().color = new Color32(140, 140, 140, 255);
        }
        targets[currentKepka].GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    [Button]
    public void UpdateKepkaList()
    {
        targets.Clear();
        currentKepka = 0;
        for (int i = 1; i < contentPanel.transform.childCount - 1; i++)
        {
            if (contentPanel.transform.GetChild(i).gameObject.activeSelf)
            {
                targets.Add(contentPanel.transform.GetChild(i).GetComponent<RectTransform>());
            }
        }
        foreach (RectTransform image in targets)
        {
            image.GetChild(0).GetComponent<Image>().color = new Color32(140, 140, 140, 255);
        }
        targets[currentKepka].GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void ChooseKepka(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        newPos = (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
        - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

        canMove = true;
        Invoke("DisableMove", speed);

        foreach(RectTransform image in targets)
        {
            image.GetChild(0).GetComponent<Image>().color = new Color32(140,140,140,255);
        }
        targets[currentKepka].GetChild(0).GetComponent<Image>().color = new Color32(255,255,255,255);
    }

    void DisableMove()
    {
        canMove = false;
    }

    private void Update()
    {
        if(canMove)
        {
            contentPanel.anchoredPosition = Vector2.Lerp(contentPanel.anchoredPosition, newPos, speed);
        }
    }
}
