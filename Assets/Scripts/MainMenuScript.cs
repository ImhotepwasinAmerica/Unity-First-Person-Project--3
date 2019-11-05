using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * MainMenuScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         These scripts handle all operations on the main menu.
 * 
 * Notes:
 * 
*/
public class MainMenuScript : MonoBehaviour
{

    public GameObject data_container, main_menu, loading_screen, options_menu, load_game_menu, some_controls;
    public Slider slider;
    public Canvas canvas;
    public Text newresume_text, quitmainmenu_text;

    // Use this for initialization
    void Start ()
    {
        // The settings files are created in their default state, if they have not done so already.
        //  This is unlikely to occur at any time except for the first time the game is started. 
        if (SaveLoad.SaveExists(Application.persistentDataPath + "/saves/keybinds.dat"))
        {
            data_container.GetComponent<DataContainment>().binds = SaveLoad.Load<SavedBindsScript>(Application.persistentDataPath + "/saves/keybinds.dat");
        }
        else
        {
            data_container.GetComponent<DataContainment>().binds = new SavedBindsScript();
            data_container.GetComponent<DataContainment>().binds.Init(new string[50]);
            data_container.GetComponent<DataContainment>().binds.DeveloperPreferences();
        }

        if (SaveLoad.SaveExists(Application.persistentDataPath + "/saves/graphicsettings.dat"))
        {
            data_container.GetComponent<DataContainment>().graphics = SaveLoad.Load<SavedGraphicsScript>(Application.persistentDataPath + "/saves/graphicsettings.dat");
        }
        else
        {
            data_container.GetComponent<DataContainment>().graphics = new SavedGraphicsScript();
            data_container.GetComponent<DataContainment>().graphics.DeveloperPreferences();
        }

        if (SaveLoad.SaveExists(Application.persistentDataPath + "/saves/soundsettings.dat"))
        {
            data_container.GetComponent<DataContainment>().sound = SaveLoad.Load<SavedSoundScript>(Application.persistentDataPath + "/saves/soundsettings.dat");
        }
        else
        {
            data_container.GetComponent<DataContainment>().sound = new SavedSoundScript();
            data_container.GetComponent<DataContainment>().sound.DeveloperPreferences();
        }

        // The gameplay settings menu has not been created yet,
        // and will not be until 
        //if (SaveLoad.SaveExists(Application.persistentDataPath + "/saves/gameplaysettings"))
        //{
        //    data_container.GetComponent<DataContainment>().gameplay = SaveLoad.Load<SavedGameplayScript>("gameplaysettings");
        //}
        //else
        //{
        //    data_container.GetComponent<DataContainment>().gameplay = (SavedGameplayScript)ScriptableObject.CreateInstance("gameplaysettings");
        //}
    }

    void Update ()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            newresume_text.text = "New Game";
            quitmainmenu_text.text = "Quit Game";
        }
        else
        {
            newresume_text.text = "Resume Game";
            quitmainmenu_text.text = "Quit to Main Menu";
        }
	}

    public void NewResume_Action()
    {
        // Save_game_slot equalling "new game"
        // indicates that the game has not been saved to a save slot yet.
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            data_container.GetComponent<DataContainment>().game = new SavedGameScript();
            data_container.GetComponent<DataContainment>().saved_objects = new List<SavedObjectScript>();
            data_container.GetComponent<DataContainment>().game.NoneYet();
            data_container.GetComponent<DataContainment>().saved_game_slot = "new game";

            LoadLevel01();
        }
        else
        {
            some_controls.GetComponent<SomeControls>().Resume();
        }
    }

    public void QuitMainMenuAction()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            Quit_Game();
        }
        else
        {
            LoadMainMenu();
        }
    }


    public void LoadLevel01 ()
    {
        StartCoroutine(Load_Asynchronously("SampleScene"));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(Load_Asynchronously("MainMenu"));
    }

    IEnumerator Load_Asynchronously (string scene_name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene_name);

        loading_screen.SetActive(true);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }

    public void Quit_Game ()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Open_Menu_Page (GameObject page)
    {
        page.SetActive(true);
        main_menu.SetActive(false);
    }

    public void Open_Options ()
    {
        Open_Menu_Page(options_menu);
    }

    public void Open_Load_Level ()
    {
        Open_Menu_Page(load_game_menu);
    }
}
