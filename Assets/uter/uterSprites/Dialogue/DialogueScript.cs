using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueScript : MonoBehaviour
{
    public DialogueSO DialogueSO;

    private string ActorNameEN;
    private string ActorNameRU;
    private Sprite ActorIcon;

    public PauseMenu PauMen;

    public TMPro.TMP_Text ActorName;
    public TMPro.TMP_Text DialogueText;

    public Animator DialogueAnimator;

    [Header("Кнопка [E] + текст")]
    public TMPro.TMP_Text DialogueStartText;
    public string StartDialogueTextRU;
    public string StartDialogueTextEN;
    [HideInInspector]
    public bool inDialogueTrig = false;
    [HideInInspector]
    public bool DialogueActive = false;

    public GameObject MouseIcon;
    private bool CanSkip = false;

    public Image ActorImage;

    public SettingsMenu setting;

    public UnityEngine.Events.UnityEvent OnActivate;
    public UnityEngine.Events.UnityEvent OnDeactivate;

    private Dictionary<string, string> dictEN;
    private Dictionary<string, string> dictRU;

    //НОМЕР ДИАЛОГА
    //[HideInInspector]
    public int CurrentDia = 1;
    private string CurrentDiaStrEN;
    private string CurrentDiaStrRU;

    private string DiaCurEN;
    private string DiaCurRU;
    private string Dia1EN;
    private string Dia1RU;
    private string Dia2EN;
    private string Dia2RU;
    private string Dia3EN;
    private string Dia3RU;
    private string Dia4EN;
    private string Dia4RU;
    private string Dia5EN;
    private string Dia5RU;
    private string Dia6EN;
    private string Dia6RU;
    private string Dia7EN;
    private string Dia7RU;
    private string Dia8EN;
    private string Dia8RU;
    private string Dia9EN;
    private string Dia9RU;
    private string Dia10EN;
    private string Dia10RU;
    private string Dia11EN;
    private string Dia11RU;
    private string Dia12EN;
    private string Dia12RU;
    private string Dia13EN;
    private string Dia13RU;
    private string Dia14EN;
    private string Dia14RU;
    private string Dia15EN;
    private string Dia15RU;
    private string Dia16EN;
    private string Dia16RU;


    private void Start()
    {
        DialogueText.text = "";
        ActorName.text = "";

        DialogueStartText.color = new Color32(255, 255, 255, 0);

        MouseIcon.SetActive(false);

        DiaCurEN = Dia1EN;
        DiaCurRU = Dia1RU;

        #region DiaInfoEN
        Dia1EN = DialogueSO.Dia1EN;
        Dia2EN = DialogueSO.Dia2EN;
        Dia3EN = DialogueSO.Dia3EN;
        Dia4EN = DialogueSO.Dia4EN;
        Dia5EN = DialogueSO.Dia5EN;
        Dia6EN = DialogueSO.Dia6EN;
        Dia7EN = DialogueSO.Dia7EN;
        Dia8EN = DialogueSO.Dia8EN;
        Dia9EN = DialogueSO.Dia9EN;
        Dia10EN = DialogueSO.Dia10EN;
        Dia11EN = DialogueSO.Dia11EN;
        Dia12EN = DialogueSO.Dia12EN;
        Dia13EN = DialogueSO.Dia13EN;
        Dia14EN = DialogueSO.Dia14EN;
        Dia15EN = DialogueSO.Dia15EN;
        Dia16EN = DialogueSO.Dia16EN;
        #endregion

        #region DiaInfoRU
        Dia1RU = DialogueSO.Dia1RU;
        Dia2RU = DialogueSO.Dia2RU;
        Dia3RU = DialogueSO.Dia3RU;
        Dia4RU = DialogueSO.Dia4RU;
        Dia5RU = DialogueSO.Dia5RU;
        Dia6RU = DialogueSO.Dia6RU;
        Dia7RU = DialogueSO.Dia7RU;
        Dia8RU = DialogueSO.Dia8RU;
        Dia9RU = DialogueSO.Dia9RU;
        Dia10RU = DialogueSO.Dia10RU;
        Dia11RU = DialogueSO.Dia11RU;
        Dia12RU = DialogueSO.Dia12RU;
        Dia13RU = DialogueSO.Dia13RU;
        Dia14RU = DialogueSO.Dia14RU;
        Dia15RU = DialogueSO.Dia15RU;
        Dia16RU = DialogueSO.Dia16RU;
        #endregion

        #region ActorInfoEN/RU
        ActorNameEN = DialogueSO.ActorNameEN;
        ActorNameRU = DialogueSO.ActorNameRU;
        ActorImage.sprite = DialogueSO.ActorIcon;
        #endregion

        ActorImage.color = new Color32(255, 255, 255, 0);
    }

    public void UpdateData()
    {
        DialogueText.text = "";
        ActorName.text = "";

        DialogueStartText.DOColor(new Color32(255, 255, 255, 0), 0.5f);

        MouseIcon.SetActive(false);

        #region DiaInfoEN
        Dia1EN = DialogueSO.Dia1EN;
        Dia2EN = DialogueSO.Dia2EN;
        Dia3EN = DialogueSO.Dia3EN;
        Dia4EN = DialogueSO.Dia4EN;
        Dia5EN = DialogueSO.Dia5EN;
        Dia6EN = DialogueSO.Dia6EN;
        Dia7EN = DialogueSO.Dia7EN;
        Dia8EN = DialogueSO.Dia8EN;
        Dia9EN = DialogueSO.Dia9EN;
        Dia10EN = DialogueSO.Dia10EN;
        Dia11EN = DialogueSO.Dia11EN;
        Dia12EN = DialogueSO.Dia12EN;
        Dia13EN = DialogueSO.Dia13EN;
        Dia14EN = DialogueSO.Dia14EN;
        Dia15EN = DialogueSO.Dia15EN;
        Dia16EN = DialogueSO.Dia16EN;
        #endregion

        #region DiaInfoRU
        Dia1RU = DialogueSO.Dia1RU;
        Dia2RU = DialogueSO.Dia2RU;
        Dia3RU = DialogueSO.Dia3RU;
        Dia4RU = DialogueSO.Dia4RU;
        Dia5RU = DialogueSO.Dia5RU;
        Dia6RU = DialogueSO.Dia6RU;
        Dia7RU = DialogueSO.Dia7RU;
        Dia8RU = DialogueSO.Dia8RU;
        Dia9RU = DialogueSO.Dia9RU;
        Dia10RU = DialogueSO.Dia10RU;
        Dia11RU = DialogueSO.Dia11RU;
        Dia12RU = DialogueSO.Dia12RU;
        Dia13RU = DialogueSO.Dia13RU;
        Dia14RU = DialogueSO.Dia14RU;
        Dia15RU = DialogueSO.Dia15RU;
        Dia16RU = DialogueSO.Dia16RU;
        #endregion

        #region ActorInfoEN/RU
        ActorNameEN = DialogueSO.ActorNameEN;
        ActorNameRU = DialogueSO.ActorNameRU;
        ActorImage.sprite = DialogueSO.ActorIcon;
        #endregion

        DiaCurEN = Dia1EN;
        DiaCurRU = Dia1RU;

        ActorImage.color = new Color32(255, 255, 255, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DialogueStartText.DOColor(new Color32(255, 255, 255, 255), 0.5f);
            inDialogueTrig = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DialogueStartText.DOColor(new Color32(255, 255, 255, 0), 0.5f);
            inDialogueTrig = false;
            DialogueActive = false;
            CanSkip = false;
            Invoke("DialogueEnd", 0.4f);
            Invoke("MouseDisable", 0.4f);

            if (CurrentDia != 16)
            {
                CurrentDia = 1;
            }
        }
    }

    private void Update()
    {
        if (StartDialogueTextEN != "" || StartDialogueTextRU != "")
        {
            if (setting.LanIndex == 0)
            {
                DialogueStartText.text = StartDialogueTextEN;
            }
            else if (setting.LanIndex == 1)
            {
                DialogueStartText.text = StartDialogueTextRU;
            }
        }

        if (setting.LanIndex == 0 && (DialogueStartText.text == "" || DialogueStartText.text == "[E] Поговорить"))
        {
            DialogueStartText.text = "[E] Talk";
        }

        if (setting.LanIndex == 1 && (DialogueStartText.text == "" || DialogueStartText.text == "[E] Talk"))
        {
            DialogueStartText.text = "[E] Поговорить";
        }


        if (inDialogueTrig && Input.GetKeyDown(KeyCode.E) && !DialogueActive && !PauMen.GameIsPaused)
        {
            DialogueStartText.DOColor(new Color32(255, 255, 255, 0), 0.5f);
            DialogueAnimator.SetTrigger("Show");
            DialogueActive = true;
            Invoke("DialogueStart", 0.5f);
            OnActivate.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.E) && DialogueActive && !PauMen.GameIsPaused)
        {
            DialogueStartText.DOColor(new Color32(255, 255, 255, 255), 0.5f);
            DialogueAnimator.SetTrigger("Hide");
            Invoke("DialogueEnd", 0.4f);
        }

        dictEN = new Dictionary<string, string>();
        dictRU = new Dictionary<string, string>();

        #region DiaEN
        dictEN["Dia1EN"] = Dia1EN;
        dictEN["Dia2EN"] = Dia2EN;
        dictEN["Dia3EN"] = Dia3EN;
        dictEN["Dia4EN"] = Dia4EN;
        dictEN["Dia5EN"] = Dia5EN;
        dictEN["Dia6EN"] = Dia6EN;
        dictEN["Dia7EN"] = Dia7EN;
        dictEN["Dia8EN"] = Dia8EN;
        dictEN["Dia9EN"] = Dia9EN;
        dictEN["Dia10EN"] = Dia10EN;
        dictEN["Dia11EN"] = Dia11EN;
        dictEN["Dia12EN"] = Dia12EN;
        dictEN["Dia13EN"] = Dia13EN;
        dictEN["Dia14EN"] = Dia14EN;
        dictEN["Dia15EN"] = Dia15EN;
        dictEN["Dia16EN"] = Dia16EN;
        #endregion

        #region DiaRU
        dictRU["Dia1RU"] = Dia1RU;
        dictRU["Dia2RU"] = Dia2RU;
        dictRU["Dia3RU"] = Dia3RU;
        dictRU["Dia4RU"] = Dia4RU;
        dictRU["Dia5RU"] = Dia5RU;
        dictRU["Dia6RU"] = Dia6RU;
        dictRU["Dia7RU"] = Dia7RU;
        dictRU["Dia8RU"] = Dia8RU;
        dictRU["Dia9RU"] = Dia9RU;
        dictRU["Dia10RU"] = Dia10RU;
        dictRU["Dia11RU"] = Dia11RU;
        dictRU["Dia12RU"] = Dia12RU;
        dictRU["Dia13RU"] = Dia13RU;
        dictRU["Dia14RU"] = Dia14RU;
        dictRU["Dia15RU"] = Dia15RU;
        dictRU["Dia16RU"] = Dia16RU;
        #endregion

        dictEN["Dia1EN"] = dictEN["Dia" + CurrentDia + "EN"];
        dictRU["Dia1RU"] = dictRU["Dia" + CurrentDia + "RU"];

        DiaCurEN = dictEN["Dia1EN"];
        DiaCurRU = dictRU["Dia1RU"];

        if (CanSkip && Input.GetMouseButtonDown(0) && DialogueAnimator.GetCurrentAnimatorStateInfo(0).IsName("Focus") && !PauMen.GameIsPaused)
        {
            if (CurrentDia == 16)
            {
                DialogueEnd();
                DialogueStartText.DOColor(new Color32(255, 255, 255, 255), 0.5f);
            }
            else
            {
                MouseDisable();
                DialogueStart();
            }
        }

        if (!DialogueAnimator.GetCurrentAnimatorStateInfo(0).IsName("Focus"))
        {
            MouseIcon.SetActive(false);
        }
    }

    public void DialogueEnd()
    {
        ActorImage.color = new Color32(255, 255, 255, 0);
        DialogueActive = false;
        MouseIcon.SetActive(false);
        StopAllCoroutines();
        DialogueText.text = "";
        ActorName.text = "";
        if (DialogueAnimator.GetCurrentAnimatorStateInfo(0).IsName("Focus"))
        {
            if (CurrentDia != 1)
            {
                OnDeactivate.Invoke();
            }
            DialogueAnimator.SetTrigger("Hide");
        }
    }

    public void DialogueStart()
    {
        ActorImage.color = new Color32(255, 255, 255, 255);

        if (setting.LanIndex == 0) { ActorName.text = ActorNameEN; }
        else if (setting.LanIndex == 1) { ActorName.text = ActorNameRU; }

        if (DiaCurEN != "" || DiaCurRU != "")
        {
            if (setting.LanIndex == 0)
            {
                StopAllCoroutines();
                StartCoroutine(TypeSentenceEN(DiaCurEN));
            }

            else if (setting.LanIndex == 1)
            {
                StopAllCoroutines();
                StartCoroutine(TypeSentenceRU(DiaCurRU));
            }

            IEnumerator TypeSentenceEN(string DiaCurEN)
            {
                DialogueText.text = "";
                foreach (char letter in DiaCurEN.ToCharArray())
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/UI/TTS", GetComponent<Transform>().position);
                    DialogueText.text += letter;
                    yield return new WaitForSeconds(0.03f);
                }
            }

            IEnumerator TypeSentenceRU(string DiaCurRU)
            {
                DialogueText.text = "";
                foreach (char letter in DiaCurRU.ToCharArray())
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/UI/TTS", GetComponent<Transform>().position);
                    DialogueText.text += letter;
                    yield return new WaitForSeconds(0.03f);
                }
            }

            if (CurrentDia != 16)
            {
                CurrentDia += 1;
            }

            Invoke("MouseEnable", 1f);
        }
        else
        {
            DialogueEnd();
            CurrentDia = 16;
        }
    }

    void MouseEnable()
    {
        if (inDialogueTrig)
        {
            MouseIcon.SetActive(true);
        }
        CanSkip = true;
    }

    void MouseDisable()
    {
        MouseIcon.SetActive(false);
        CanSkip = false;
    }
}
