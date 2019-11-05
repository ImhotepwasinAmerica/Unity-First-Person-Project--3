using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * SomeControls
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         These scripts handle controls not associated with movement or the camera
 * 
 * Notes:
 * The the system by which keybinds are saved involves saving key names to an array, 
 * in which the index numbers represent different actions.
 * These indexes and their reflective actions are as follows:
 * 0                action_general
 * 1                toolbelt_one
 * 2                toolbelt_two
 * 3                toolbelt_three
 * 4                toolbelt_four
 * 5                toolbelt_five
 * 6                toolbelt_six
 * 7                toolbelt_seven
 * 8                toolbelt_eight
 * 9                toolbelt_nine
 * 10               toolbelt_ten
 * 11               attack_primary
 * 12               attack_secondary
 * 13               attack_tertiary
 * 14               kick
 * 15               block
 * 16               move_forward
 * 17               move_backward
 * 18               move_left
 * 19               move_right
 * 20               jump
 * 21               squat
 * 22               speed_toggle
 * 23               toolbelt_scroll_up
 * 24               toolbelt_scroll_down
 * 25               quicksave
 * 26               quickload
 * 27               pause
 * 28               log
 * 29               objectives
 * 30               stats
 * 31               gamma_up
 * 32               gamma_down
*/
public class SomeControls : MonoBehaviour
{
    private string pause, quicksave, quickload, log, objectives, stats, gamma_up, gamma_down;
    public static bool paused = false;
    public GameObject data_containment,pause_UI,main_menu, options_menu, keybindings_menu, graphics_menu, sound_menu, gameplay_menu, game_files_menu;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        pause = data_containment.GetComponent<DataContainment>().binds.keybind_log[27];
        quicksave = data_containment.GetComponent<DataContainment>().binds.keybind_log[25];
        quickload = data_containment.GetComponent<DataContainment>().binds.keybind_log[26];
        log = data_containment.GetComponent<DataContainment>().binds.keybind_log[28];
        objectives = data_containment.GetComponent<DataContainment>().binds.keybind_log[29];
        stats = data_containment.GetComponent<DataContainment>().binds.keybind_log[30];
        gamma_up = data_containment.GetComponent<DataContainment>().binds.keybind_log[31];
        gamma_down = data_containment.GetComponent<DataContainment>().binds.keybind_log[32];

        if (Input.GetButtonDown(pause))
        {
            if (paused && !keybindings_menu.activeSelf)
            {
                options_menu.SetActive(false);
                keybindings_menu.SetActive(false);
                graphics_menu.SetActive(false);
                sound_menu.SetActive(false);
                gameplay_menu.SetActive(false);
                game_files_menu.SetActive(false);

                main_menu.SetActive(true);

                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetButtonDown(quicksave))
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
