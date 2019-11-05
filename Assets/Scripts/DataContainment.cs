using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataContainment : MonoBehaviour
{ 
    public GameObject this_thing;

    //public SavedBindsScript binds;
    public SavedBindsScript binds;
    public SavedGraphicsScript graphics;
    public SavedSoundScript sound;
    public SavedGameplayScript gameplay;

    public SavedCharacterScript character;
    public SavedSceneScript scene;
    public SavedGameScript game;

    public List<SavedObjectScript> saved_objects;

    //  When a game is loaded, these shall comprise the saved file location.
    //  "savedgames/saved_game_slot/saved_game_scene/..."
    public string saved_game_slot, saved_game_scene; 

    private GameObject[] alla_deeze;

    private void Start()
    {
        alla_deeze = GameObject.FindGameObjectsWithTag("Data Containment");

        for (int i = 0; i < alla_deeze.Length; i++)
        {
            if (alla_deeze[i] != this.gameObject)
            {
                this.binds = alla_deeze[i].GetComponent<DataContainment>().binds;
                this.graphics = alla_deeze[i].GetComponent<DataContainment>().graphics;
                this.sound = alla_deeze[i].GetComponent<DataContainment>().sound;
                this.gameplay = alla_deeze[i].GetComponent<DataContainment>().gameplay;

                this.character = alla_deeze[i].GetComponent<DataContainment>().character;
                this.scene = alla_deeze[i].GetComponent<DataContainment>().scene;
                this.game = alla_deeze[i].GetComponent<DataContainment>().game;

                this.saved_objects = alla_deeze[i].GetComponent<DataContainment>().saved_objects;

                this.saved_game_slot = alla_deeze[i].GetComponent<DataContainment>().saved_game_slot;
                this.saved_game_scene = alla_deeze[i].GetComponent<DataContainment>().saved_game_scene;

                Destroy(alla_deeze[i]);
            }
        }

        Object.DontDestroyOnLoad(transform.gameObject);
    }
}
