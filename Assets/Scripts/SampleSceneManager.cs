using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * SampleSceneManager
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         This script handles conditions unique to the scene 'SampleScene'
 * 
 * Notes:
 * 
*/
public class SampleSceneManager : MonoBehaviour
{
    public GameObject data_container, player;

    private void Awake() {}

    // Use this for initialization
    void Start ()
    {
        // If the scene has been loaded before in the current save,
        // the objects in it shall be destroyed.
        // Spawning in the saved objects is handled by the universal level manager, LevelManager.
        string slot = data_container.GetComponent<DataContainment>().saved_game_slot;
        string scene = data_container.GetComponent<DataContainment>().saved_game_scene;

        data_container.GetComponent<DataContainment>().game.shall_be_loaded[1] = true;
        data_container.GetComponent<DataContainment>().game.SetSceneName("SampleScene");
        data_container.GetComponent<DataContainment>().game.SetSceneIndex(1);

        
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        
    }

    public bool SaveExists(string file_location)
    {
        return SaveLoad.SaveExists(file_location);
    }

    public bool DirectoryExists(string directory_location)
    {
        return SaveLoad.DirectoryExists(directory_location);
    }
}
