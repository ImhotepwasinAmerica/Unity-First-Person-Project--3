using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * SoundMenuScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         These scripts handle all operations on the sound menu.
 * 
 * Notes:
 * 
*/
public class SoundMenuScript : MonoBehaviour
{

    private SavedSoundScript local_data;

    public GameObject options_menu, sound_menu, data_container;

    public Slider master_slider, music_slider, voices_slider, environmental_slider;

    public Text master_text, music_text, voices_text, environmental_text;

    // Use this for initialization
    void Start ()
    {
         local_data = data_container.GetComponent<DataContainment>().sound;

        master_slider.value = local_data.master + 0.01f;
        music_slider.value = local_data.music + 0.01f;
        voices_slider.value = local_data.voices + 0.01f;
        environmental_slider.value = local_data.environment + 0.01f;

        master_text.text = local_data.master + "";
        music_text.text = local_data.music + "";
        voices_text.text = local_data.voices + "";
        environmental_text.text = local_data.environment + "";
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        local_data.master = ((int)master_slider.value);
        local_data.music = ((int)music_slider.value);
        local_data.voices = ((int)voices_slider.value);
        local_data.environment = ((int)environmental_slider.value);

        master_text.text = local_data.master + "";
        music_text.text = local_data.music + "";
        voices_text.text = local_data.voices + "";
        environmental_text.text = local_data.environment + "";
    }

    public void SaveSettings()
    {
        SaveLoad.Save<SavedSoundScript>(local_data, Application.persistentDataPath + "/saves/soundsettings.dat");
    }

    public void OpenOptions()
    {
        OpenMenuPage(options_menu);
    }

    public void OpenMenuPage(GameObject page)
    {
        page.SetActive(true);
        sound_menu.SetActive(false);
    }
}
