﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuScriptKeybinds : MonoBehaviour
{
    public GameObject keybindings_menu, options_menu, data_container;

    private GameObject current_button;

    private string current_action, current_key;

    public Text move_forward, move_backward, move_left, move_right,
        jump, squat, speed_toggle, action_general,
        attack_primary, attack_secondary, attack_tertiary, kick, block,
        toolbelt_one, toolbelt_two, toolbelt_three, toolbelt_four, toolbelt_five,
        toolbelt_six, toolbelt_seven, toolbelt_eight, toolbelt_nine, toolbelt_ten,
        toolbelt_scroll_up, toolbelt_scroll_down, drop,
        quicksave, quickload, pause, log, objectives, stats,
        gamma_up, gamma_down;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("General Action"))
        {
            DeveloperPreferences.Keybinds();
        }

        action_general.text = PlayerPrefs.GetString("General Action");
        toolbelt_one.text = PlayerPrefs.GetString("Toolbelt One");
        toolbelt_two.text = PlayerPrefs.GetString("Toolbelt Two");
        toolbelt_three.text = PlayerPrefs.GetString("Toolbelt Three");
        toolbelt_four.text = PlayerPrefs.GetString("Toolbelt Four");
        toolbelt_five.text = PlayerPrefs.GetString("Toolbelt Five");
        toolbelt_six.text = PlayerPrefs.GetString("Toolbelt Six");
        toolbelt_seven.text = PlayerPrefs.GetString("Toolbelt Seven");
        toolbelt_eight.text = PlayerPrefs.GetString("Toolbelt Eight");
        toolbelt_nine.text = PlayerPrefs.GetString("Toolbelt Nine");
        toolbelt_ten.text = PlayerPrefs.GetString("Toolbelt Ten");
        attack_primary.text = PlayerPrefs.GetString("Primary Attack");
        attack_secondary.text = PlayerPrefs.GetString("Secondary Attack");
        attack_tertiary.text = PlayerPrefs.GetString("Tertiary Attack");
        kick.text = PlayerPrefs.GetString("Kick");
        block.text = PlayerPrefs.GetString("Block");
        move_forward.text = PlayerPrefs.GetString("Move Forward");
        move_backward.text = PlayerPrefs.GetString("Move Backward");
        move_left.text = PlayerPrefs.GetString("Move Left");
        move_right.text = PlayerPrefs.GetString("Move Right");
        jump.text = PlayerPrefs.GetString("Jump");
        squat.text = PlayerPrefs.GetString("Squat");
        speed_toggle.text = PlayerPrefs.GetString("Speed Toggle");
        toolbelt_scroll_up.text = PlayerPrefs.GetString("Toolbelt Scroll Up");
        toolbelt_scroll_down.text = PlayerPrefs.GetString("Toolbelt Scroll Down");
        drop.text = PlayerPrefs.GetString("Drop");
        quicksave.text = PlayerPrefs.GetString("Quicksave");
        quickload.text = PlayerPrefs.GetString("Quickload");
        pause.text = PlayerPrefs.GetString("Pause");
        log.text = PlayerPrefs.GetString("Log");
        objectives.text = PlayerPrefs.GetString("Objectives");
        stats.text = PlayerPrefs.GetString("Stats");
        gamma_up.text = PlayerPrefs.GetString("Gamma Up");
        gamma_down.text = PlayerPrefs.GetString("Gamma Down");
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerPrefs.SetString("General Action", action_general.text);
        //PlayerPrefs.SetString("Toolbelt One", toolbelt_one.text);
        //PlayerPrefs.SetString("Toolbelt Two", toolbelt_two.text);
        //PlayerPrefs.SetString("Toolbelt Three", toolbelt_three.text);
        //PlayerPrefs.SetString("Toolbelt Four", toolbelt_four.text);
        //PlayerPrefs.SetString("Toolbelt Five", toolbelt_five.text);
        //PlayerPrefs.SetString("Toolbelt Six", toolbelt_six.text);
        //PlayerPrefs.SetString("Toolbelt Seven", toolbelt_seven.text);
        //PlayerPrefs.SetString("Toolbelt Eight", toolbelt_eight.text);
        //PlayerPrefs.SetString("Toolbelt Nine", toolbelt_nine.text);
        //PlayerPrefs.SetString("Toolbelt Ten", toolbelt_ten.text);
        //PlayerPrefs.SetString("Primary Attack", attack_primary.text);
        //PlayerPrefs.SetString("Secondary Attack", attack_secondary.text);
        //PlayerPrefs.SetString("Tertiary Attack", attack_tertiary.text);
        //PlayerPrefs.SetString("Kick", kick.text);
        //PlayerPrefs.SetString("Block", block.text);
        //PlayerPrefs.SetString("Move Forward", move_forward.text);
        //PlayerPrefs.SetString("Move Backward", move_backward.text);
        //PlayerPrefs.SetString("Move Left", move_left.text);
        //PlayerPrefs.SetString("Move Right", move_right.text);
        //PlayerPrefs.SetString("Jump", jump.text);
        //PlayerPrefs.SetString("Squat", squat.text);
        //PlayerPrefs.SetString("Speed Toggle", speed_toggle.text);
        //PlayerPrefs.SetString("Toolbelt Scroll Up", toolbelt_scroll_up.text);
        //PlayerPrefs.SetString("Toolbelt Scroll Down", toolbelt_scroll_down.text);
        //PlayerPrefs.SetString("Drop", drop.text);
        //PlayerPrefs.SetString("Quicksave", quicksave.text);
        //PlayerPrefs.SetString("Quickload", quickload.text);
        //PlayerPrefs.SetString("Pause", pause.text);
        //PlayerPrefs.SetString("Log", log.text);
        //PlayerPrefs.SetString("Objectives", objectives.text);
        //PlayerPrefs.SetString("Stats", stats.text);
        //PlayerPrefs.SetString("Gamma Up", gamma_up.text);
        //PlayerPrefs.SetString("Gamma Down", gamma_down.text);
    }

    private void OnGUI()
    {
        if (current_button != null) // set when a button is clicked
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
        current_button.transform.GetChild(0).GetComponent<Text>().text = entry;
        current_button = null;

        PlayerPrefs.SetString(current_action,entry);
        Debug.Log(current_action +" " + entry);
    }

    public void ChangeKey(GameObject clicked)
    {
        current_button = clicked;
    }

    public void SetCurrentAction(string action)
    {
        current_action = action;
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
}
