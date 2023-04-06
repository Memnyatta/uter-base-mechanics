using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SettingsMenu : MonoBehaviour
{
    public GameObject Settings;

    public TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public Volume volume;
    public ColorAdjustments postExposure;

    private int MonitorHeight;
    private int MonitorWidth;
    private int MonitorSum;

    //APPLIED INT/FLOAT
    private int resolutionIndex;
    private int qualityIndex;
    private int DisplayIndex;
    private int VSyncIndex;
    private int LanguageIndex;
    private float BrightnessIndex;

    //SAVED INT
    public int ResIndex;
    public int QuaIndex;
    public int DisIndex;
    public int VSIndex;
    public int LanIndex;
    public float BriIndex;

    //UI
    public TMPro.TMP_Dropdown LanguageDropdown;
    public TMPro.TMP_Dropdown QualityDropdown;
    public TMPro.TMP_Dropdown ResolutionDropdown;
    public TMPro.TMP_Dropdown DisplayDropdown;
    public TMPro.TMP_Dropdown VSyncDropdown;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Slider BrightnessSlider;

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


    void Update()
    {
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(MasterVolume);
    }

    void FixedUpdate()
    {
        postExposure.postExposure.value = BriIndex;
    }

    //SOUND SETTINGS

    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
    }

    //OTHER SETTINGS
    public void Start()
    {
        MonitorSum = MonitorHeight + MonitorWidth;

        volume.profile.TryGet<ColorAdjustments>(out postExposure);

        string path = Application.persistentDataPath + "/SettingsSave.fun";
        if (File.Exists(path))
        {

        }
        else
        {
            MasterVolume = 1f;
            MusicVolume = 0.5f;
            SFXVolume = 0.5f;

            switch (MonitorSum)
            {
                case 4000:
                    ResIndex = 0;
                    break;
                case 3000:
                    ResIndex = 1;
                    break;
                case 2000:
                    ResIndex = 2;
                    break;
                case 2340:
                    ResIndex = 3;
                    break;
                case 2304:
                    ResIndex = 4;
                    break;
                default:
                    ResIndex = 1;
                    break;
            }
            Invoke("ApplySettings", 0f);
        }
                   
        LoadPlayer();
        LoadSettings();
        Invoke("ApplySettings", 0f);
    }

    public void SetResolution(int resolutionIndex)
    {
        ResIndex = resolutionIndex;
    }

    public void SetQuality(int qualityIndex)
    {
        QuaIndex = qualityIndex;
    }

    public void SetDisplay(int DisplayIndex)
    {
        DisIndex = DisplayIndex;
    }

    public void VSync(int VSyncIndex = 0)
    {
        VSIndex = VSyncIndex;
    }

    public void Language(int LanguageIndex)
    {
        LanIndex = LanguageIndex;
        //LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[LanIndex];
    }

    public void Brightness(float BrightnessIndex)
    {
        BriIndex = BrightnessIndex;
        Screen.brightness = BriIndex;
    }

    public void SavePlayer()
    {
        Debug.Log("Сохраняем в SettingsMenu");
        SaveSystem_settings.SavePlayer(this);
    }

    public void OnApplicationQuit()
    {
        SaveSystem_settings.SavePlayer(this);
        FMOD.Studio.Bus MasterBus;
        MasterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void LoadPlayer()
    {
        Debug.Log("Загружаем в SettingsMenu");
        SettingsData data = SaveSystem_settings.LoadLevel();

        MasterVolume = data.MasterVolume;
        MusicVolume = data.MusicVolume;
        SFXVolume = data.SFXVolume;
        ResIndex = data.resolution;
        QuaIndex = data.graphics;
        DisIndex = data.display;
        VSIndex = data.vsync;
        LanIndex = data.language;
        BriIndex = data.brightness;
    }

    public void LoadSettings()
    {
       // Debug.Log("Загружаем ещё рах ");
        MasterSlider.value = MasterVolume;
        MusicSlider.value = MusicVolume;
        SFXSlider.value = SFXVolume;
        QualityDropdown.value = QuaIndex;
        ResolutionDropdown.value = ResIndex;
        LanguageDropdown.value = LanIndex;
        DisplayDropdown.value = DisIndex;
        VSyncDropdown.value = VSIndex;
        BrightnessSlider.value = BriIndex;
    }

    public void ApplySettings()
    {
        switch (ResIndex)
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

        QualitySettings.SetQualityLevel(QuaIndex);
        
        switch (DisIndex)
        {
            case 0:
                Screen.fullScreen = true;
                break;
            case 1:
                Screen.fullScreen = false;
                break;

        }

        QualitySettings.vSyncCount = VSIndex;

        //LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[LanIndex];
    }
}
