using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMenuScript : MonoBehaviour {

    public GameObject options_menu;
    public GameObject gameplay_menu;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Open_Options()
    {
        Open_Menu_Page(options_menu);
    }

    public void Open_Menu_Page(GameObject page)
    {
        page.SetActive(true);
        gameplay_menu.SetActive(false);
    }
}
