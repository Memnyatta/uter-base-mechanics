using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class KepkaInventory : MonoBehaviour
{
    public PauseMenu _pauMenu;
    public GameObject KepkaInv;
    [Header("Активные кепки")]
    public List<KepkaSO> Kepkas = new List<KepkaSO>();
    [Header("Кепки, которые будут активированы в начале сцены")]
    public List<KepkaSO> KepkaAdd = new List<KepkaSO>();
    [Header("Кепки на Утере")]
    public List<GameObject> KepkasUter = new List<GameObject>();
    public GameObject CurrentKepka;
    public GameObject KepkaFantomLeft;
    public GameObject KepkaFantomRight;
    public int KepkaNum;
    public ParticleSystem _explosion;
    [SerializeField]
    public string OdetayaKepka;

    bool canChangeKepka;

    MyNameIsUter controls = null;

    private void Awake()
    {
        controls = new MyNameIsUter();
        controls.UI.Enable();
        controls.UI.OpenKepkaInv.started += KepkaMenu;
        controls.UI.KepkaPlus.started += NextKepka;
        controls.UI.KepkaMinus.started += PreviousKepka;
        controls.UI.Submit.started += CloseKepkaMenuWithInput;

        Lockpick.OnHacking += HackCheck;

        _explosion.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        MyNameIsUter controls = new MyNameIsUter();
        controls.UI.Disable();
        controls.UI.OpenKepkaInv.started -= KepkaMenu;
        controls.UI.KepkaPlus.started -= NextKepka;
        controls.UI.KepkaMinus.started -= PreviousKepka;
        controls.UI.Submit.started -= CloseKepkaMenuWithInput;

        Lockpick.OnHacking -= HackCheck;
    }

    private void Start()
    {
        canChangeKepka = true;
        if (KepkaAdd.Count > 0)
        {
            foreach (var item in KepkaAdd)
            {
                Kepkas.Add(item);
            }
        }
        KepkaNum = 1;
        KepkaInv.SetActive(false);
        _pauMenu = GetComponent<PauseMenu>();
    }

    void HackCheck(bool isHack)
    {
        canChangeKepka = !isHack;
    }

    private void Update()
    {
        CurrentKepka.GetComponent<Image>().sprite = Kepkas[KepkaNum]._image;
        if (KepkaNum == 0)
        {
            KepkaFantomLeft.GetComponent<Image>().sprite = Kepkas[Kepkas.Count - 1]._image;
        }
        else
        {
            KepkaFantomLeft.GetComponent<Image>().sprite = Kepkas[KepkaNum - 1]._image;
        }

        if (KepkaNum == Kepkas.Count - 1)
        {
            KepkaFantomRight.GetComponent<Image>().sprite = Kepkas[0]._image;
        }
        else
        {
            KepkaFantomRight.GetComponent<Image>().sprite = Kepkas[KepkaNum + 1]._image;
        }
    }

    public void KepkaMenu(InputAction.CallbackContext context)
    {
        if (!canChangeKepka)
        {
            return;
        }

        if (KepkaInv.activeSelf)
        {
            CloseKepkaMenu();
        }
        else
        {
            OpenKepkaMenu();
        }
    }

    void OpenKepkaMenu()
    {
        if (!_pauMenu.GameIsPaused)
        {
            KepkaInv.SetActive(true);
            _pauMenu.Pause(false);
        }
    }

    void CloseKepkaMenuWithInput(InputAction.CallbackContext context)
    {
        if (context.started && KepkaInv.activeSelf)
        {
            CloseKepkaMenu();
        }
    }

    public void CloseKepkaMenu()
    {
        KepkaApply();
        _pauMenu.Resume();
        KepkaInv.SetActive(false);
    }

    public void NextKepka(InputAction.CallbackContext context)
    {
        if (context.started && KepkaInv.activeSelf)
        {
            if (KepkaNum == Kepkas.Count - 1)
            {
                KepkaNum = 0;
            }
            else
            {
                KepkaNum++;
            }
        }
    }

    public void PreviousKepka(InputAction.CallbackContext context)
    {
        if (context.started && KepkaInv.activeSelf)
        {
            if (KepkaNum == 0)
            {
                KepkaNum = Kepkas.Count - 1;
            }
            else
            {
                KepkaNum--;
            }
        }
    }

    public void KepkaApply()
    {
        foreach (GameObject kepka in KepkasUter)
        {
            if (kepka.name != Kepkas[KepkaNum].name)
            {
                kepka.SetActive(false);
            }
            else
            {
                if(!kepka.activeSelf)
                {
                    kepka.SetActive(true);
                    _explosion.gameObject.SetActive(true);
                    _explosion.Play();
                }
                OdetayaKepka = kepka.name;
            }
        }
    }
}