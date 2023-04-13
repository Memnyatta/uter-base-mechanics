using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using UnityEditor.ShaderGraph;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public static bool Tasked = false;

    public Button ContinueButton;
    public GameObject pauseMenuUI;
    public GameObject OptionsMenuUI;

    public List<GameObject> arrows = new List<GameObject>();

    FMOD.Studio.EventInstance snapshot;

    MyNameIsUter controls = null;

    private void Awake()
    {
        controls = new MyNameIsUter();
    }

    private void OnEnable()
    {
        controls.UI.Enable();
        controls.UI.Cancel.started += PauseGame;
    }

    private void OnDisable()
    {
        controls.UI.Disable();
        controls.UI.Cancel.started -= PauseGame;
    }

    void Start()
    {
        snapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Game Is Paused");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        DisableArrows();
    }

    public void DisableArrows()
    {
        foreach (GameObject go in arrows)
        {
            go.SetActive(false);
        }
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (GameIsPaused)
            {
                if (OptionsMenuUI.activeSelf)
                {
                    OptionsMenuUI.SetActive(false);
                    pauseMenuUI.SetActive(true);
                    DisableArrows();
                    Pause(true);
                }
                else
                {
                    Resume();
                }
            }
            else
            {
                Pause(true);
            }
        }
    }

public void Resume()
{
    DisableArrows();
    snapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    GameIsPaused = false;
    MouseDisable();
}

public void Pause(bool withUI)
{
        if (withUI)
        {
            ContinueButton.Select();
            arrows[0].SetActive(true);
            snapshot.start();
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            MouseEnable();
        }
        else
        {
            snapshot.start();
            Time.timeScale = 0f;
            MouseEnable();
        }
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

