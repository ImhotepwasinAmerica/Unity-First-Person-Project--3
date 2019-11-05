using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedBindsScript
{
    public string[] keybind_log;

    public void Init(string[] new_log)
    {
        keybind_log = new_log;
    }

    public void DeveloperPreferences()
    {
        keybind_log[0] = "E";
        keybind_log[1] = "Alpha1";
        keybind_log[2] = "Alpha2";
        keybind_log[3] = "Alpha3";
        keybind_log[4] = "Alpha4";
        keybind_log[5] = "Alpha5";
        keybind_log[6] = "Alpha6";
        keybind_log[7] = "Alpha7";
        keybind_log[8] = "Alpha8";
        keybind_log[9] = "Alpha9";
        keybind_log[10] = "Alpha0";
        keybind_log[11] = "Mouse0";
        keybind_log[12] = "Mouse1";
        keybind_log[13] = "Mouse2";
        keybind_log[14] = "F";
        keybind_log[15] = "C";
        keybind_log[16] = "W";
        keybind_log[17] = "S";
        keybind_log[18] = "A";
        keybind_log[19] = "D";
        keybind_log[20] = "Space";
        keybind_log[21] = "LeftControl";
        keybind_log[22] = "LeftShift";
        keybind_log[23] = "MouseWheelUp";
        keybind_log[24] = "MouseWheelDown";
        keybind_log[25] = "F6";
        keybind_log[26] = "F9";
        keybind_log[27] = "Escape";
        keybind_log[28] = "L";
        keybind_log[29] = "O";
        keybind_log[30] = "Tab";
        keybind_log[31] = "Equals";
        keybind_log[32] = "Minus";
        keybind_log[33] = "Q";
    }
}
