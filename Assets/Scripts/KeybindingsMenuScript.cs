using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/*
 * KeybindingsMenuScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         These scripts handle all the functions of the keybindings options menu.
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
 * 33               drop
*/
public class KeybindingsMenuScript : MonoBehaviour {

    public GameObject keybindings_menu, options_menu, button_set, data_container;

    private GameObject current_key;

    //private SavedBindsScript local_data;
    private SavedBindsScript local_data;

    private int current_index;
    private string current_inputaxis;

    public Text move_forward, move_backward, move_left, move_right,
        jump, squat, speed_toggle, action_general,
        attack_primary, attack_secondary, attack_tertiary, kick, block,
        toolbelt_one, toolbelt_two, toolbelt_three, toolbelt_four, toolbelt_five,
        toolbelt_six, toolbelt_seven, toolbelt_eight, toolbelt_nine, toolbelt_ten,
        toolbelt_scroll_up, toolbelt_scroll_down, drop,
        quicksave, quickload, pause, log, objectives, stats,
        gamma_up, gamma_down;

    void Start ()
    {
        local_data = data_container.GetComponent<DataContainment>().binds;

        move_forward.text = local_data.keybind_log[16];
        move_backward.text = local_data.keybind_log[17];
        move_left.text = local_data.keybind_log[18];
        move_right.text = local_data.keybind_log[19];
        jump.text = local_data.keybind_log[20];
        squat.text = local_data.keybind_log[21];
        speed_toggle.text = local_data.keybind_log[22];
        action_general.text = local_data.keybind_log[0];
        attack_primary.text = local_data.keybind_log[11];
        attack_secondary.text = local_data.keybind_log[12];
        attack_tertiary.text = local_data.keybind_log[13];
        kick.text = local_data.keybind_log[14];
        block.text = local_data.keybind_log[15];
        toolbelt_one.text = local_data.keybind_log[1];
        toolbelt_two.text = local_data.keybind_log[2];
        toolbelt_three.text = local_data.keybind_log[3];
        toolbelt_four.text = local_data.keybind_log[4];
        toolbelt_five.text = local_data.keybind_log[5];
        toolbelt_six.text = local_data.keybind_log[6];
        toolbelt_seven.text = local_data.keybind_log[7];
        toolbelt_eight.text = local_data.keybind_log[8];
        toolbelt_nine.text = local_data.keybind_log[9];
        toolbelt_ten.text = local_data.keybind_log[10];
        toolbelt_scroll_up.text = local_data.keybind_log[23];
        toolbelt_scroll_down.text = local_data.keybind_log[24];
        drop.text = local_data.keybind_log[33];
        quicksave.text = local_data.keybind_log[25];
        quickload.text = local_data.keybind_log[26];
        pause.text = local_data.keybind_log[27];
        log.text = local_data.keybind_log[28];
        objectives.text = local_data.keybind_log[29];
        stats.text = local_data.keybind_log[30];
        gamma_up.text = local_data.keybind_log[31];
        gamma_down.text = local_data.keybind_log[32];
    }

    public void SetCurrentIndex(int new_index)
    {
        current_index = new_index;
    }

    public void SetCurrentInputaxis(string new_inputaxis)
    {
        current_inputaxis = new_inputaxis;
    }

    private void OnGUI()
    {
        if (current_key != null) // set when a button is clicked
        {
            Event e = Event.current;
            if (e.isKey)
            {
                KeyEntry(e.keyCode.ToString());
            }
            else if (e.shift)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    KeyEntry(KeyCode.LeftShift.ToString());
                }
                else if (Input.GetKey(KeyCode.RightShift))
                {
                    KeyEntry(KeyCode.RightShift.ToString());
                }
            }
            else if (e.isMouse)
            {
                if (e.button == 0)
                {
                    KeyEntry(KeyCode.Mouse0.ToString());
                }
                else if (e.button == 1)
                {
                    KeyEntry(KeyCode.Mouse1.ToString());
                }
                else if (e.button == 2)
                {
                    KeyEntry(KeyCode.Mouse2.ToString());
                }
            }
            else if (e.isScrollWheel)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    KeyEntry("MouseWheelUp");
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    KeyEntry("MouseWheelDown");
                }
            }
        }
    }

    public void KeyEntry(string entry)
    {
        current_key.transform.GetChild(0).GetComponent<Text>().text = entry;
        current_key = null;

        SetCurrentInputaxis(entry);
        CreateNewEntry();
        ApplyBinds();
        SaveBinds();
    }

    public void ChangeKey(GameObject clicked)
    {
        current_key = clicked;
    }

    public void CreateNewEntry()
    {
        local_data.keybind_log[current_index] = current_inputaxis;
    }

    public void CreateNewEntry2()
    {
        data_container.GetComponent<DataContainment>().binds.keybind_log[current_index] = current_inputaxis;
    }

    public void Open_Menu_Page(GameObject page)
    {
        page.SetActive(true);
        keybindings_menu.SetActive(false);
    }

    public void Open_Options()
    {
        Open_Menu_Page(options_menu);
    }

    public void ApplyBinds()
    {
        data_container.GetComponent<DataContainment>().binds = local_data;
    }

    public void LoadBinds()
    {
        //local_data = SaveLoad.Load<SavedBindsScript>("keybinds");
        local_data = SaveLoad.Load<SavedBindsScript>(Application.persistentDataPath + "/saves/keybinds.dat");
    }

    public void SaveBinds()
    {
        //SaveLoad.Save<SavedBindsScript>(local_data, "keybinds");
        SaveLoad.Save<SavedBindsScript>(local_data, Application.persistentDataPath + "/saves/keybinds.dat");
    }
}
