using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SavedSoundScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         This class contains the saved sound settings. 
 * 
 * Notes:
 * 
*/
[System.Serializable]
public class SavedSoundScript
{
    public int master,music,voices,environment;

    public void DeveloperPreferences()
    {
        master = 100;
        music = 100;
        voices = 100;
        environment = 100;
    }
}
