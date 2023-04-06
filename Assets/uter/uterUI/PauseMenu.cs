using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public delegate void openedMenu(); //Ивент при надевании стандартной кепки
    public static event openedMenu onOpenedMenu;

    public bool GameIsPaused = false;
    public static bool Tasked = false;

    public GameObject pauseMenuUI;
    public GameObject OptionsMenuUI;

    public GameObject Sounds;

    FMOD.Studio.EventInstance snapshot;

    void Start()
    {
        snapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Game Is Paused");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                OptionsMenuUI.SetActive(false);
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    { 
        snapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        MouseDisable();
        Sounds.SetActive(true);
    }

    void Pause()
    {
        snapshot.start();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        if (onOpenedMenu != null)onOpenedMenu();
        MouseEnable();
        Sounds.SetActive(false);
    }

    public void LoadMenu()
    {
        snapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("MainMenu");
    }

    public void MouseEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void MouseDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
