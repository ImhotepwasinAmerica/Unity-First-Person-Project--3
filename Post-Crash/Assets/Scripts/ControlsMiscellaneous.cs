using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMiscellaneous : MonoBehaviour
{
    public static bool paused = false;
    public GameObject data_containment, pause_UI, main_menu, options_menu, keybindings_menu, graphics_menu, sound_menu, gameplay_menu, game_files_menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(PlayerPrefs.GetString("Pause")))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetButtonDown(PlayerPrefs.GetString("Quicksave")))
        {

        }

        if (Input.GetButtonDown(PlayerPrefs.GetString("Quickload")))
        {

        }

        if (Input.GetButtonDown(PlayerPrefs.GetString("Log")))
        {

        }

        if (Input.GetButtonDown(PlayerPrefs.GetString("Objectives")))
        {

        }

        if (Input.GetButtonDown(PlayerPrefs.GetString("Stats")))
        {

        }

        if (Input.GetButtonDown(PlayerPrefs.GetString("Gamma Up")))
        {

        }

        if (Input.GetButtonDown(PlayerPrefs.GetString("Gamma Down")))
        {

        }
    }

    public void Resume()
    {
        pause_UI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pause_UI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
