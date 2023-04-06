using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KepkaInv : MonoBehaviour
{
    public CurrentScene manager;
    public PauseMenu pMenu;

    private bool active = false;
    public Animator UIAnim;

    [Space]
    public GameObject Swift;
    public GameObject Standart;
    public GameObject Haxers;
    public GameObject Frogerz;
    public GameObject Vedro;
    public GameObject Hose;
    public GameObject Sem_1;
    public GameObject Sem_2;
    public GameObject Sem_3;
    public GameObject Sem_4;
    public GameObject Sem_5;
    public GameObject Clown_1;
    public GameObject Clown_2;
    public GameObject Clown_3;
    public GameObject Clown_4;
    public GameObject Lapka_1;
    public GameObject ochki;

    [Space]
    public ParticleSystem Boom;

    [Space]
    #region Images
    public Image ImgStandart;
    public Image ImgHaxers;
    public Image ImgFrogerz;
    public Image ImgVedro;
    public Image ImgHose;
    public Image ImgSem;
    public Image ImgClown;
    public Image ImgSwift;

    [Space]
    public Sprite SprtStandart;
    public Sprite SprtHaxers;
    public Sprite SprtFrogerz;
    public Sprite SprtVedro;
    public Sprite SprtHose;
    public Sprite SprtSem;
    public Sprite SprtClown;
    public Sprite SprtSwift;

    [Space]
    public Sprite SprtShadStandart;
    public Sprite SprtShadHaxers;
    public Sprite SprtShadFrogerz;
    public Sprite SprtShadVedro;
    public Sprite SprtShadHose;
    public Sprite SprtShadSem;
    public Sprite SprtShadClown;
    public Sprite SprtShadSwift;
    #endregion

    [Space]
    #region Buttons
    public Button ButStandart;
    public Button ButHaxers;
    public Button ButFrogerz;
    public Button ButVedro;
    public Button ButHose;
    public Button ButSem;
    public Button ButClown;
    public Button ButSwift;

    [Space]
    public EventTrigger EvStandart;
    public EventTrigger EvHaxers;
    public EventTrigger EvFrogerz;
    public EventTrigger EvVedro;
    public EventTrigger EvHose;
    public EventTrigger EvSem;
    public EventTrigger EvClown;
    public EventTrigger EvSwift;
    #endregion

    private void Start()
    {
        Swift.SetActive(false);
        Standart.SetActive(true);
        Haxers.SetActive(false);
        Frogerz.SetActive(false);
        Vedro.SetActive(false);
        Hose.SetActive(false);
        Sem_1.SetActive(false);
        Sem_2.SetActive(false);
        Sem_3.SetActive(false);
        Sem_4.SetActive(false);
        Sem_5.SetActive(false);
        Clown_1.SetActive(false);
        Clown_2.SetActive(false);
        Clown_3.SetActive(false);
        Clown_4.SetActive(false);
        Lapka_1.SetActive(true);
        ochki.SetActive(true);

        ButStandart.gameObject.SetActive(false);
        ButHaxers.gameObject.SetActive(false);
        ButFrogerz.gameObject.SetActive(false);
        ButVedro.gameObject.SetActive(false);
        ButHose.gameObject.SetActive(false);
        ButSem.gameObject.SetActive(false);
        ButClown.gameObject.SetActive(false);
        ButSwift.gameObject.SetActive(false);
    }

    void Update()
    {
        if (UIAnim.GetBool("isShown"))
        {
            pMenu.MouseEnable();
        }

        if (Input.GetKeyDown(KeyCode.R) && !pMenu.GameIsPaused && !active)
        {
            ButStandart.gameObject.SetActive(true);
            ButHaxers.gameObject.SetActive(true);
            ButFrogerz.gameObject.SetActive(true);
            ButVedro.gameObject.SetActive(true);
            ButHose.gameObject.SetActive(true);
            ButSem.gameObject.SetActive(true);
            ButClown.gameObject.SetActive(true);
            ButSwift.gameObject.SetActive(true);
            pMenu.MouseEnable();
            active = true;
            UIAnim.SetBool("isShown", true);

        }
        else if(Input.GetKeyDown(KeyCode.R) && !pMenu.GameIsPaused && active)
        {
            pMenu.MouseDisable();
            active = false;
            UIAnim.SetBool("isShown", false);
            Invoke("DisableButtons", 0.5f);
        }

        #region CheckOpenKepkas
        if (manager.kepkas.Length >= 7 && manager.kepkas[7])
        {
            ImgStandart.sprite = SprtStandart;
            ButStandart.enabled = true;
            EvStandart.enabled = true;
        }
        else
        {
            ImgStandart.sprite = SprtShadStandart;
            ButStandart.enabled = false;
            EvStandart.enabled = false;
        }
        if (manager.kepkas[1])
        {
            ImgHaxers.sprite = SprtHaxers;
            ButHaxers.enabled = true;
            EvHaxers.enabled = true;
        }
        else
        {
            ImgHaxers.sprite = SprtShadHaxers;
            ButHaxers.enabled = false;
            EvHaxers.enabled = false;
        }
        if (manager.kepkas[0])
        {
            ImgFrogerz.sprite = SprtFrogerz;
            ButFrogerz.enabled = true;
            EvFrogerz.enabled = true;
        }
        else
        {
            ImgFrogerz.sprite = SprtShadFrogerz;
            ButFrogerz.enabled = false;
            EvFrogerz.enabled = false;
        }
        if (manager.kepkas[3])
        {
            ImgVedro.sprite = SprtVedro;
            ButVedro.enabled = true;
            EvVedro.enabled = true;
        }
        else
        {
            ImgVedro.sprite = SprtShadVedro;
            ButVedro.enabled = false;
            EvVedro.enabled = false;
        }
        if (manager.kepkas[2])
        {
            ImgHose.sprite = SprtHose;
            ButHose.enabled = true;
            EvHose.enabled = true;
        }
        else
        {
            ImgHose.sprite = SprtShadHose;
            ButHose.enabled = false;
            EvHose.enabled = false;
        }
        if (manager.kepkas[5])
        {
            ImgSem.sprite = SprtSem;
            ButSem.enabled = true;
            EvSem.enabled = true;
        }
        else
        {
            ImgSem.sprite = SprtShadSem;
            ButSem.enabled = false;
            EvSem.enabled = false;
        }
        if (manager.kepkas[4])
        {
            ImgClown.sprite = SprtClown;
            ButClown.enabled = true;
            EvClown.enabled = true;
        }
        else
        {
            ImgClown.sprite = SprtShadClown;
            ButClown.enabled = false;
            EvClown.enabled = false;
        }
        if (manager.kepkas[6])
        {
            ImgSwift.sprite = SprtSwift;
            ButSwift.enabled = true;
            EvSwift.enabled = true;
        }
        else
        {
            ImgSwift.sprite = SprtShadSwift;
            ButSwift.enabled = false;
            EvSwift.enabled = false;
        }
        #endregion
    }

    #region InvAnimations
    public void InvEnter_1()
    {
        UIAnim.Play("InvOpens.Inv_1");
    }
    public void InvLeave_1()
    {
        UIAnim.Play("InvOpens.Inv_1 0");
    }

    public void InvEnter_2()
    {
        UIAnim.Play("InvOpens.Inv_2");
    }
    public void InvLeave_2()
    {
        UIAnim.Play("InvOpens.Inv_2 0");
    }

    public void InvEnter_3()
    {
        UIAnim.Play("InvOpens.Inv_3");
    }
    public void InvLeave_3()
    {
        UIAnim.Play("InvOpens.Inv_3 0");
    }

    public void InvEnter_4()
    {
        UIAnim.Play("InvOpens.Inv_4");
    }
    public void InvLeave_4()
    {
        UIAnim.Play("InvOpens.Inv_4 0");
    }

    public void InvEnter_5()
    {
        UIAnim.Play("InvOpens.Inv_5");
    }
    public void InvLeave_5()
    {
        UIAnim.Play("InvOpens.Inv_5 0");
    }

    public void InvEnter_6()
    {
        UIAnim.Play("InvOpens.Inv_6");
    }
    public void InvLeave_6()
    {
        UIAnim.Play("InvOpens.Inv_6 0");
    }

    public void InvEnter_7()
    {
        UIAnim.Play("InvOpens.Inv_7");
    }
    public void InvLeave_7()
    {
        UIAnim.Play("InvOpens.Inv_7 0");
    }

    public void InvEnter_8()
    {
        UIAnim.Play("InvOpens.Inv_8");
    }
    public void InvLeave_8()
    {
        UIAnim.Play("InvOpens.Inv_8 0");
    }
    #endregion

    public void ExitInv()
    {
        pMenu.MouseDisable();
        active = false;
        UIAnim.SetBool("isShown", false);
    }

    #region PickKepkas

    public void Pick_Nothing()
    {
        Swift.SetActive(false);
        Standart.SetActive(false);
        Haxers.SetActive(false);
        Frogerz.SetActive(false);
        Vedro.SetActive(false);
        Hose.SetActive(false);
        Sem_1.SetActive(false);
        Sem_2.SetActive(false);
        Sem_3.SetActive(false);
        Sem_4.SetActive(false);
        Sem_5.SetActive(false);
        Clown_1.SetActive(false);
        Clown_2.SetActive(false);
        Clown_3.SetActive(false);
        Clown_4.SetActive(false);
        Lapka_1.SetActive(true);
        ochki.SetActive(true);
    }

    public void Pick_Standart()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/KepkaChangeSFX");
        Boom.Play();
        Swift.SetActive(false);
        Standart.SetActive(true);
        Haxers.SetActive(false);
        Frogerz.SetActive(false);
        Vedro.SetActive(false);
        Hose.SetActive(false);
        Sem_1.SetActive(false);
        Sem_2.SetActive(false);
        Sem_3.SetActive(false);
        Sem_4.SetActive(false);
        Sem_5.SetActive(false);
        Clown_1.SetActive(false);
        Clown_2.SetActive(false);
        Clown_3.SetActive(false);
        Clown_4.SetActive(false);
        Lapka_1.SetActive(true);
        ochki.SetActive(true);
    }

    public void Pick_Frogerz()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/KepkaChangeSFX");
        Boom.Play();
        Swift.SetActive(false);
        Standart.SetActive(false);
        Haxers.SetActive(false);
        Frogerz.SetActive(true);
        Vedro.SetActive(false);
        Hose.SetActive(false);
        Sem_1.SetActive(false);
        Sem_2.SetActive(false);
        Sem_3.SetActive(false);
        Sem_4.SetActive(false);
        Sem_5.SetActive(false);
        Clown_1.SetActive(false);
        Clown_2.SetActive(false);
        Clown_3.SetActive(false);
        Clown_4.SetActive(false);
        Lapka_1.SetActive(true);
        ochki.SetActive(true);
    }

    public void Pick_Haxers()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/KepkaChangeSFX");
        Boom.Play();
        Swift.SetActive(false);
        Standart.SetActive(false);
        Haxers.SetActive(true);
        Frogerz.SetActive(false);
        Vedro.SetActive(false);
        Hose.SetActive(false);
        Sem_1.SetActive(false);
        Sem_2.SetActive(false);
        Sem_3.SetActive(false);
        Sem_4.SetActive(false);
        Sem_5.SetActive(false);
        Clown_1.SetActive(false);
        Clown_2.SetActive(false);
        Clown_3.SetActive(false);
        Clown_4.SetActive(false);
        Lapka_1.SetActive(true);
        ochki.SetActive(true);
    }

    public void Pick_swift()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/KepkaChangeSFX");
        Boom.Play();
        Swift.SetActive(true);
        Standart.SetActive(false);
        Haxers.SetActive(false);
        Frogerz.SetActive(false);
        Vedro.SetActive(false);
        Hose.SetActive(false);
        Sem_1.SetActive(false);
        Sem_2.SetActive(false);
        Sem_3.SetActive(false);
        Sem_4.SetActive(false);
        Sem_5.SetActive(false);
        Clown_1.SetActive(false);
        Clown_2.SetActive(false);
        Clown_3.SetActive(false);
        Clown_4.SetActive(false);
        Lapka_1.SetActive(true);
        ochki.SetActive(true);
    }

    public void Pick_Vedro()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/KepkaChangeSFX");
        Boom.Play();
        Swift.SetActive(false);
        Standart.SetActive(false);
        Haxers.SetActive(false);
        Frogerz.SetActive(false);
        Vedro.SetActive(true);
        Hose.SetActive(false);
        Sem_1.SetActive(false);
        Sem_2.SetActive(false);
        Sem_3.SetActive(false);
        Sem_4.SetActive(false);
        Sem_5.SetActive(false);
        Clown_1.SetActive(false);
        Clown_2.SetActive(false);
        Clown_3.SetActive(false);
        Clown_4.SetActive(false);
        Lapka_1.SetActive(true);
        ochki.SetActive(true);
    }

    public void Pick_Hose()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/KepkaChangeSFX");
        Boom.Play();
        Swift.SetActive(false);
        Standart.SetActive(false);
        Haxers.SetActive(false);
        Frogerz.SetActive(false);
        Vedro.SetActive(false);
        Hose.SetActive(true);
        Sem_1.SetActive(false);
        Sem_2.SetActive(false);
        Sem_3.SetActive(false);
        Sem_4.SetActive(false);
        Sem_5.SetActive(false);
        Clown_1.SetActive(false);
        Clown_2.SetActive(false);
        Clown_3.SetActive(false);
        Clown_4.SetActive(false);
        Lapka_1.SetActive(true);
        ochki.SetActive(true);
    }

    public void Pick_Sem()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/KepkaChangeSFX");
        Boom.Play();
        Swift.SetActive(false);
        Standart.SetActive(false);
        Haxers.SetActive(false);
        Frogerz.SetActive(false);
        Vedro.SetActive(false);
        Hose.SetActive(false);
        Sem_1.SetActive(true);
        Sem_2.SetActive(true);
        Sem_3.SetActive(true);
        Sem_4.SetActive(true);
        Sem_5.SetActive(true);
        Clown_1.SetActive(false);
        Clown_2.SetActive(false);
        Clown_3.SetActive(false);
        Clown_4.SetActive(false);
        Lapka_1.SetActive(true);
        ochki.SetActive(false);
    }

    public void Pick_clown()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/KepkaChangeSFX");
        Boom.Play();
        Swift.SetActive(false);
        Standart.SetActive(false);
        Haxers.SetActive(false);
        Frogerz.SetActive(false);
        Vedro.SetActive(false);
        Hose.SetActive(false);
        Sem_1.SetActive(false);
        Sem_2.SetActive(false);
        Sem_3.SetActive(false);
        Sem_4.SetActive(false);
        Sem_5.SetActive(false);
        Clown_1.SetActive(true);
        Clown_2.SetActive(true);
        Clown_3.SetActive(true);
        Clown_4.SetActive(true);
        Lapka_1.SetActive(false);
        ochki.SetActive(true);
    }
    #endregion

    void DisableButtons()
    {
        if (!active)
        {
            ButStandart.gameObject.SetActive(false);
            ButHaxers.gameObject.SetActive(false);
            ButFrogerz.gameObject.SetActive(false);
            ButVedro.gameObject.SetActive(false);
            ButHose.gameObject.SetActive(false);
            ButSem.gameObject.SetActive(false);
            ButClown.gameObject.SetActive(false);
            ButSwift.gameObject.SetActive(false);
        }
    }
}
