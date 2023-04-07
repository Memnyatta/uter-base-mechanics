using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    private int MonitorHeight;
    private int MonitorWidth;
    private int MonitorSum;

    public List<GameObject> Arrows = new List<GameObject>();

    //UI
    public TMPro.TMP_Dropdown LanguageDropdown;
    public TMPro.TMP_Dropdown QualityDropdown;
    public TMPro.TMP_Dropdown ResolutionDropdown;
    public TMPro.TMP_Dropdown DisplayDropdown;
    public TMPro.TMP_Dropdown VSyncDropdown;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    //SOUNDSETTINGS
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;
    public float MasterVolume = 1f;
    public float MusicVolume = 0.5f;
    public float SFXVolume = 0.5f;

    void Awake()
    {
        Application.targetFrameRate = 144;

        MonitorHeight = Screen.height;
        MonitorWidth = Screen.width;

        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");

        Time.timeScale = 1f;
    }

    public void DisableAllArrows()
    {
        foreach (GameObject obj in Arrows)
        {
            obj.SetActive(false);
        }
    }

    bool HasSave()
    {
        List<string> pfs = new List<string>() { "MasterVolume", "MusicVolume", "SFXVolume", "QuaIndex", "ResIndex", "LanIndex", "DisIndex", "VSIndex" };
        foreach(string p in pfs)
        {
            if(!PlayerPrefs.HasKey(p))
            {
                return false;
            }
        }
        return true;
    }

    void Update()
    {
        ApplySettings();
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(MasterVolume);
    }

    public void Start()
    {
        LoadSettings();
        MonitorSum = MonitorHeight + MonitorWidth;

        if (!HasSave())
        {
            MasterVolume = 1f;
            MusicVolume = 0.5f;
            SFXVolume = 0.5f;

            switch (MonitorSum)
            {
                case 4000:
                    ResolutionDropdown.value = 0;
                    break;
                case 3000:
                    ResolutionDropdown.value = 1;
                    break;
                case 2000:
                    ResolutionDropdown.value = 2;
                    break;
                case 2340:
                    ResolutionDropdown.value = 3;
                    break;
                case 2304:
                    ResolutionDropdown.value = 4;
                    break;
                default:
                    ResolutionDropdown.value = 1;
                    break;
            }
        }
    }

    public void OnApplicationQuit()
    {
        SaveSettings();
        FMOD.Studio.Bus MasterBus;
        MasterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
        PlayerPrefs.SetInt("QuaIndex", QualityDropdown.value);
        PlayerPrefs.SetInt("ResIndex", ResolutionDropdown.value);
        PlayerPrefs.SetInt("LanIndex", LanguageDropdown.value);
        PlayerPrefs.SetInt("DisIndex", DisplayDropdown.value);
        PlayerPrefs.SetInt("VSIndex", VSyncDropdown.value);
    }

    public void LoadSettings()
    {
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        QualityDropdown.value = PlayerPrefs.GetInt("QuaIndex");
        ResolutionDropdown.value = PlayerPrefs.GetInt("ResIndex");
        LanguageDropdown.value = PlayerPrefs.GetInt("LanIndex");
        DisplayDropdown.value = PlayerPrefs.GetInt("DisIndex");
        VSyncDropdown.value = PlayerPrefs.GetInt("VSIndex");
    }

    public void ApplySettings()
    {
        switch (ResolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(2560, 1440, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 3:
                Screen.SetResolution(1440, 900, Screen.fullScreen);
                break;
            case 4:
                Screen.SetResolution(1280, 1024, Screen.fullScreen);
                break;
            default:
                Debug.Log("Дефолтный");
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
                break;
        }

        QualitySettings.SetQualityLevel(QualityDropdown.value);

        switch (DisplayDropdown.value)
        {
            case 0:
                Screen.fullScreen = true;
                break;
            case 1:
                Screen.fullScreen = false;
                break;

        }

        QualitySettings.vSyncCount = VSyncDropdown.value;
    }

    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
