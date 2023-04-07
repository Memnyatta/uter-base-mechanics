using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using FMOD;
using System.Diagnostics;
using I2.Loc;
using TMPro;

public class Lockpick : MonoBehaviour
{
    public GameObject LockPickUI;
    public bool canStartHack;
    [SerializeField]
    bool isHacking;
    [SerializeField]
    ThirdPersonController uterController;
    MyNameIsUter _controls;
    [Header("TextPrefab")]
    [SerializeField]
    TextPrefabScript textPrefab;
    [SerializeField]
    string TextRU;
    [SerializeField]
    string TextEN;
    [Header("UI")]
    public GameObject yellowAnchor;
    public GameObject grayAnchor;
    public float rotationSpeed = 5;
    public float rotationDirectionYellow;
    public float rotationDirectionGray;

    [Header("Key info")]
    [SerializeField] float YellowKeyZ, GrayKeyZ;

    [Header("Game")]
    public int RightPositionYellowMin;
    public int RightPositionYellowMax;
    public int RightPositionGrayMin;
    public int RightPositionGrayMax;

    public bool hackIsCompleted;
    public bool spin;

    [SerializeField]
    int currentRound;

    public UnityEvent m_OnComplete;

    [Header("FMOD")]
    public GameObject SFXYellow;
    public GameObject SFXGray;

    bool alreadyPlayed = false;

    public delegate void StartToHack(bool isHacking);
    public static event StartToHack OnHacking;

    private void Awake()
    {
        _controls = new MyNameIsUter();
        _controls.Enable();
        _controls.Player.Interact.started += InitialiseHack;
        LocalizationManager.OnLocalizeEvent += OnChangeLanguage;
    }

    private void Start()
    {
        SFXYellow.SetActive(false);
        SFXGray.SetActive(false);
        currentRound= 0;
        LockPickUI.SetActive(false);
    }

    private void Update()
    {
        if (isHacking)
        {
            if ((RightPositionYellowMin < YellowKeyZ) && (YellowKeyZ < RightPositionYellowMax))
            {
                if (!alreadyPlayed)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/LockPicking_1", transform.position);
                    alreadyPlayed = true;
                }
            }
            else
            {
                alreadyPlayed = false;
            }
        }

        if ((_controls.Player.HackAD.IsPressed() || _controls.Player.HackQE.IsPressed()) && isHacking)
        {
            rotationDirectionGray = _controls.Player.HackAD.ReadValue<float>();
            rotationDirectionYellow = _controls.Player.HackQE.ReadValue<float>();
        }
        else
        {
            SFXGray.SetActive(false);
            SFXYellow.SetActive(false);
            rotationDirectionGray = 0;
            rotationDirectionYellow = 0;
        }

        YellowKeyZ = (int)yellowAnchor.transform.localEulerAngles.z;
        GrayKeyZ = (int)grayAnchor.transform.localEulerAngles.z;
    }


    void OnChangeLanguage()
    {
        if (LocalizationManager.CurrentLanguage == "Russian")
        {
            textPrefab.GetComponent<TMP_Text>().text = TextRU;
        }
        else
        {
            textPrefab.GetComponent<TMP_Text>().text = TextEN;
        }
    }

    IEnumerator yellowAnchorRotate()
    {
        while (spin)
        {
            if (yellowAnchor.transform.localEulerAngles.z < 360 || yellowAnchor.transform.localEulerAngles.z > -360)
            {
                SFXYellow.SetActive(true);
                yellowAnchor.transform.localEulerAngles += new Vector3(0, 0, -rotationDirectionYellow * rotationSpeed * Time.deltaTime);
            }
            else
            {
                yellowAnchor.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            if (grayAnchor.transform.localEulerAngles.z < 360 || grayAnchor.transform.localEulerAngles.z > -360)
            {
                SFXGray.SetActive(true);
                grayAnchor.transform.localEulerAngles += new Vector3(0, 0, rotationDirectionGray * rotationSpeed * Time.deltaTime);
            }
            else
            {
                grayAnchor.transform.localEulerAngles = new Vector3(0, 0, 0);
            }

            if (CheckIfRight())
            {
                if (currentRound == 2)
                {
                    EndHack(true);
                }
                else
                {
                    NextHack();
                }
            }

            yield return null;
        }
    }

    [Button("GenerateNum")]
    void NextHack()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/LockPicking_2", transform.position);

        currentRound++;

        RightPositionYellowMin = Random.Range(0, 360);
        if (RightPositionYellowMax > 350)
        {
            RightPositionYellowMax = RightPositionYellowMin - 10;
        }
        else if (RightPositionYellowMax < 10)
        {
            RightPositionYellowMax = RightPositionYellowMin + 10;
        }
        else
        {
            RightPositionYellowMax = RightPositionYellowMin + 10;
        }

        RightPositionGrayMin = Random.Range(0, 360);
        if (RightPositionGrayMax > 350)
        {
            RightPositionGrayMax = RightPositionGrayMin - 10;
        }
        else if (RightPositionGrayMax < 10)
        {
            RightPositionGrayMax = RightPositionGrayMin + 10;
        }
        else
        {
            RightPositionGrayMax = RightPositionGrayMin + 10;
        }

        if (RightPositionGrayMax < RightPositionGrayMin)
        {
            NextHack();
        }
    }

    public bool CheckIfRight()
    {
        if (((RightPositionYellowMin < YellowKeyZ) && (YellowKeyZ < RightPositionYellowMax))
            && ((RightPositionGrayMin < GrayKeyZ) && (GrayKeyZ < RightPositionGrayMax)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void InitialiseHack(InputAction.CallbackContext context)
    {
        if (Time.deltaTime == 0)
        {
            return;
        }
        if(context.started && canStartHack)
        {
            if (!isHacking && !hackIsCompleted)
            {
                StartHack();
            }
            else if (isHacking && !hackIsCompleted)
            {
                EndHack();
            }
        }
    }

    void StartHack()
    {
        OnHacking(true);
        spin = true;
        StartCoroutine(yellowAnchorRotate());
        uterController.canMove= false;
        isHacking = true;
        LockPickUI.SetActive(true);
        textPrefab.MakeTextInvisible();
    }

    void EndHack(bool completeHack = false)
    {
        OnHacking(false);
        if (completeHack)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/LockPicking_3", transform.position);
            m_OnComplete.Invoke();
            hackIsCompleted = true;
            textPrefab.MakeTextInvisible();
            currentRound = 0;
        }
        StopCoroutine(yellowAnchorRotate());
        spin = false;
        grayAnchor.transform.localEulerAngles = new Vector3(0, 0, 0);
        yellowAnchor.transform.localEulerAngles = new Vector3(0, 0, 0);
        uterController.canMove = true;
        isHacking = false;
        LockPickUI.SetActive(false);
        if(canStartHack)
        {
            textPrefab.MakeTextVisible();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canStartHack= true;
            textPrefab.MakeTextVisible();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canStartHack = false;
            textPrefab.MakeTextInvisible();
        }
    }
}
