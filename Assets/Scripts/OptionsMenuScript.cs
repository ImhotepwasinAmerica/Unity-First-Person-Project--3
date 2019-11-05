using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * OptionsMenuScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         This class handles the operations of the options menu hub. 
 * 
 * Notes:
 * 
*/
public class OptionsMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject options_menu, main_menu, keybindings_menu, graphics_menu, sound_menu, gameplay_menu;

    public void Open_Menu_Page(GameObject page)
    {
        page.SetActive(true);
        options_menu.SetActive(false);
    }

    public void Open_Main ()
    {
        Open_Menu_Page(main_menu);
    }

    public void Open_Keybindings ()
    {
        Open_Menu_Page(keybindings_menu);
    }

    public void Open_Graphics()
    {
        Open_Menu_Page(graphics_menu);
    }

    public void OpenSound()
    {
        Open_Menu_Page(sound_menu);
    }

    public void OpenGameplay()
    {
        Open_Menu_Page(gameplay_menu);
    }
}
