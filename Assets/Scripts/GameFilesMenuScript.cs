using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*
 * GameFilesMenuScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         These scripts handle all operations on the game files menu.
 * 
 * Notes:
 * This is how the game's files are organized:
 * 
 * Options settings:
 * #Application.persistentDataPath#/saves/keybinds
 * #Application.persistentDataPath#/saves/graphicsettings
 * #Application.persistentDataPath#/saves/soundsettings
 * #Application.persistentDataPath#/saves/gameplaysettings
 * 
 * Savegame data not related to scenes:
 * #Application.persistentDataPath#/saves/savedgames/#saveslot#/character
 * #Application.persistentDataPath#/saves/savedgames/#saveslot#/basicdata
 * 
 * Savegame data regarding individual scenes:
 * #Application.persistentDataPath#/saves/savedgames/#saveslot#/#scene#/scene
 * #Application.persistentDataPath#/saves/savedgames/#saveslot#/#scene#/list
*/
public class GameFilesMenuScript : MonoBehaviour
{
    public GameObject data_container;
    public GameObject loading_screen;

    public GameObject game_files_menu, main_menu;

    public Text quicksave, save1, save2, save3, save4, save5, save6, save7, save8, save9, save10;
    public GameObject button0, button1, button2, button3, button4, button5, button6, button7, button8, button9, button10,
        load0, load1, load2, load3, load4, load5, load6, load7, load8, load9, load10;

    private SavedGameScript[] local_data;
    private SavedGameScript current_save;
    public Slider slider;

    // Use this for initialization
    void Start ()
    {
        local_data = new SavedGameScript[11];

        //InitiateSaveSlot(quicksave, "savedgames/quicksave/basicdata", 0);
        //InitiateSaveSlot(save1, "savedgames/save1/basicdata", 1);
        //InitiateSaveSlot(save2,"savedgames/save2/basicdata",2);
        //InitiateSaveSlot(save3, "savedgames/save3/basicdata",3);
        //InitiateSaveSlot(save4, "savedgames/save4/basicdata",4);
        //InitiateSaveSlot(save5, "savedgames/save5/basicdata",5);
        //InitiateSaveSlot(save6, "savedgames/save6/basicdata",6);
        //InitiateSaveSlot(save7, "savedgames/save7/basicdata",7);
        //InitiateSaveSlot(save8, "savedgames/save8/basicdata",8);
        //InitiateSaveSlot(save9, "savedgames/save9/basicdata",9);
        //InitiateSaveSlot(save10, "savedgames/save10/basicdata",10);
    }
	
	// Update is called once per frame
	void Update ()
    {
        InitiateSaveSlot(quicksave, "savedgames/quicksave/basicdata", 0);
        InitiateSaveSlot(save1, "savedgames/save1/basicdata", 1);
        InitiateSaveSlot(save2, "savedgames/save2/basicdata", 2);
        InitiateSaveSlot(save3, "savedgames/save3/basicdata", 3);
        InitiateSaveSlot(save4, "savedgames/save4/basicdata", 4);
        InitiateSaveSlot(save5, "savedgames/save5/basicdata", 5);
        InitiateSaveSlot(save6, "savedgames/save6/basicdata", 6);
        InitiateSaveSlot(save7, "savedgames/save7/basicdata", 7);
        InitiateSaveSlot(save8, "savedgames/save8/basicdata", 8);
        InitiateSaveSlot(save9, "savedgames/save9/basicdata", 9);
        InitiateSaveSlot(save10, "savedgames/save10/basicdata", 10);

        if (local_data[0] != null) { ToggleActivation(load0, true); }
        else { ToggleActivation(load0, false); }

        if (local_data[1] != null) { ToggleActivation(load1, true); }
        else{ ToggleActivation(load1, false); }

        if (local_data[2] != null) { ToggleActivation(load2, true); }
        else { ToggleActivation(load2, false); }

        if (local_data[3] != null) { ToggleActivation(load3, true); }
        else { ToggleActivation(load3, false); }

        if (local_data[4] != null) { ToggleActivation(load4, true); }
        else { ToggleActivation(load4, false); }

        if (local_data[5] != null) { ToggleActivation(load5, true); }
        else { ToggleActivation(load5, false); }

        if (local_data[6] != null) { ToggleActivation(load6, true); }
        else { ToggleActivation(load6, false); }

        if (local_data[7] != null) { ToggleActivation(load7, true); }
        else { ToggleActivation(load7, false); }

        if (local_data[8] != null) { ToggleActivation(load8, true); }
        else { ToggleActivation(load8, false); }

        if (local_data[9] != null) { ToggleActivation(load9, true); }
        else { ToggleActivation(load9, false); }

        if (local_data[10] != null) { ToggleActivation(load10, true); }
        else { ToggleActivation(load10, false); }

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            ToggleActivation(button0, false);
            ToggleActivation(button1, false);
            ToggleActivation(button2, false);
            ToggleActivation(button3, false);
            ToggleActivation(button4, false);
            ToggleActivation(button5, false);
            ToggleActivation(button6, false);
            ToggleActivation(button7, false);
            ToggleActivation(button8, false);
            ToggleActivation(button9, false);
            ToggleActivation(button10, false);
        }
    }

    public void InitiateSaveSlot(Text display,string location, int index)
    {
        if (CheckSaveExistence(location))
        {
            local_data[index] = SaveLoad.Load<SavedGameScript>(Application.persistentDataPath + "/saves/" + location + ".dat");

            Set_Text(display, local_data[index].date_and_time);
        }
        else
        {
            Set_Text(display, "Slot empty");
        }
    }

    public bool CheckSaveExistence(string location)
    {
        return SaveLoad.SaveExists(Application.persistentDataPath + "/saves/" + location + ".dat");
    }

    public void SetCurrentSave(int index)
    {
        if (index >= 0 || index < 11)
        {
            current_save = local_data[index];
        }
    }

    public void DeleteEntry(int index)
    {
        local_data[index] = null;
    }
    
    public void Delete_Text(Text text)
    {
        text.text = "Slot empty";
    }

    public void Set_Text(Text text, string content)
    {
        text.text = content;
    }

    public void ToggleActivation(GameObject thing, bool new_setting)
    {
        thing.SetActive(new_setting);
    }

    public void LoadButton(string directory)
    {
        data_container.GetComponent<DataContainment>().game = current_save;
        data_container.GetComponent<DataContainment>().character = SaveLoad.Load<SavedCharacterScript>(Application.persistentDataPath + "/saves/" + directory + "character.dat");

        data_container.GetComponent<DataContainment>().scene = SaveLoad.Load<SavedSceneScript>(Application.persistentDataPath + "/saves/" + directory + data_container.GetComponent<DataContainment>().game.current_scene + "/scene.dat");
        data_container.GetComponent<DataContainment>().saved_objects = SaveLoad.Load<List<SavedObjectScript>>(Application.persistentDataPath + "/saves/" + directory + data_container.GetComponent<DataContainment>().game.current_scene + "/list.dat");

        SetSaveSlot(directory.Substring(11,directory.Length-12));

        LoadLevel(data_container.GetComponent<DataContainment>().game.current_scene);
    }

    public void SetSaveSlot(string slot)
    {
        data_container.GetComponent<DataContainment>().saved_game_slot = slot;
    }

    public void SaveButton(string directory)
    {
        // If a directory for the save slot does not exist, 
        // one is created.
        if (!Directory.Exists(Application.persistentDataPath + "/saves/" + directory))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/" + directory);
        }

        // If the directories for each scene which needs to be saved do not exist in the current slot,
        // they are created.
        if (!Directory.Exists(Application.persistentDataPath + "/saves/" + directory + "SampleScene") &&
            data_container.GetComponent<DataContainment>().game.shall_be_loaded[1] == true)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/" + directory + "SampleScene");
        }

        // The essential data of the game is saved.
        SaveLoad.Save<SavedGameScript>(data_container.GetComponent<DataContainment>().game, Application.persistentDataPath + "/saves/" + directory + "basicdata.dat");
        SaveLoad.Save<SavedCharacterScript>(data_container.GetComponent<DataContainment>().character, Application.persistentDataPath + "/saves/" + directory + "character.dat");

        // If a saved game has files stored in a different slot than what is being saved to, those files will be transferred over to the new slot, in their equivalent directories.
        if (data_container.GetComponent<DataContainment>().saved_game_slot != "new game")
        {
            SaveLoad.Save<SavedSceneScript>( SaveLoad.Load<SavedSceneScript>(Application.persistentDataPath + "/saves/savedgames/" + data_container.GetComponent<DataContainment>().saved_game_slot + "SampleScene/scene.dat") , Application.persistentDataPath + "/saves/" + directory + "SampleScene/scene.dat");
            SaveLoad.Save<List<SavedObjectScript>>(SaveLoad.Load<List<SavedObjectScript>>(Application.persistentDataPath + "/saves/savedgames/" + data_container.GetComponent<DataContainment>().saved_game_slot + "SampleScene/list.dat"), Application.persistentDataPath + "/saves/" + directory + "SampleScene/list.dat");
        }
        else
        {
            SaveLoad.Save<SavedSceneScript>(data_container.GetComponent<DataContainment>().scene, Application.persistentDataPath + "/saves/" + directory + "SampleScene/scene.dat");
            SaveLoad.Save<List<SavedObjectScript>>(data_container.GetComponent<DataContainment>().saved_objects, Application.persistentDataPath + "/saves/" + directory + "SampleScene/list.dat");
        }
        
    }

    public void DeleteDirectory(string directory)
    {
        SaveLoad.DeleteDirectory(Application.persistentDataPath + "/saves/" + directory);
        //if (SaveLoad.SaveExists(directory + "basicdata"))
        //{
        //    SaveLoad.DeleteDirectory(directory);
        //}
    }

    public void Open_Main_Menu()
    {
        Open_Menu_Page(main_menu);
    }

    public void Open_Menu_Page(GameObject page)
    {
        page.SetActive(true);
        game_files_menu.SetActive(false);
    }

    public void LoadLevel(string scene)
    {
        StartCoroutine(Load_Asynchronously(scene));
    }

    IEnumerator Load_Asynchronously(string scene_name)
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

    public void LoadThisSave()
    {
        LoadLevel(data_container.GetComponent<DataContainment>().game.current_scene);
    }
}

