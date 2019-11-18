using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MenuScriptSound : MonoBehaviour
{
    public GameObject options_menu, sound_menu, data_container;

    public Text universal_text, music_text, dialogue_text, environment_text;

    public Slider universal_slider, music_slider, dialogue_slider, environment_slider;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("sound_universal"))
        {
            universal_slider.value = PlayerPrefs.GetInt("sound_universal") + 0.01f;
            universal_text.text = PlayerPrefs.GetInt("sound_universal") + "";
        }
        else
        {
            universal_slider.value = 100.01f;
            universal_text.text = 100 + "";
        }

        if (PlayerPrefs.HasKey("sound_music"))
        {
            music_slider.value = PlayerPrefs.GetInt("sound_music") + 0.01f;
            music_text.text = PlayerPrefs.GetInt("sound_music") + "";
        }
        else
        {
            music_slider.value = 100.01f;
            music_text.text = 100 + "";
        }

        if (PlayerPrefs.HasKey("sound_dialogue"))
        {
            dialogue_slider.value = PlayerPrefs.GetInt("sound_dialogue") + 0.01f;
            dialogue_text.text = PlayerPrefs.GetInt("sound_dialogue") + "";
        }
        else
        {
            dialogue_slider.value = 100.01f;
            dialogue_text.text = 100 + "";
        }

        if (PlayerPrefs.HasKey("sound_environment"))
        {
            environment_slider.value = PlayerPrefs.GetInt("sound_environment") + 0.01f;
            environment_text.text = PlayerPrefs.GetInt("sound_environment") + "";
        }
        else
        {
            environment_slider.value = 100.01f;
            environment_text.text = 100 + "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("sound_universal", ((int)universal_slider.value));
        PlayerPrefs.SetInt("sound_music", ((int)music_slider.value));
        PlayerPrefs.SetInt("sound_dialogue", ((int)dialogue_slider.value));
        PlayerPrefs.SetInt("sound_environment", ((int)environment_slider.value));

        universal_text.text = (int)universal_slider.value + "";
        music_text.text = (int)music_slider.value + "";
        dialogue_text.text = (int)dialogue_slider.value + "";
        environment_text.text = (int)environment_slider.value + "";
    }

    public void Open_Options()
    {
        Open_Menu_Page(options_menu);
    }

    public void Open_Menu_Page(GameObject page)
    {
        page.SetActive(true);
        sound_menu.SetActive(false);
    }
}
