using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;


/*
 * SavedGameScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         This object stores data essential to a saved game.
 * 
 * Notes:
 * The 'shall_be_loaded' bool array determines which scenes are relevant to gameplay, and should therefore be loaded.
 * This is for the purpose of keeping the saving process efficient, and memory free.
 * The position in the array which represents a particular scene is that scene's index, for simplicity's sake.
*/
[System.Serializable]
public class SavedGameScript
{
    [SerializeField]
    public string date_and_time; //System.DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy")

    [SerializeField]
    public string current_scene;

    [SerializeField]
    public int scene_index;

    [SerializeField]
    public bool[] shall_be_loaded;

    public void NoneYet()
    {
        shall_be_loaded = new bool[18] { false, false, false, false, false, false, false, false, false, false, false, false, false , false, false, false, false, false};
    }

    public void SetSceneName(string new_current_scene)
    {
        current_scene = new_current_scene;
    }

    public void SetSceneIndex(int new_scene_index)
    {
        scene_index = new_scene_index;
    }
}
