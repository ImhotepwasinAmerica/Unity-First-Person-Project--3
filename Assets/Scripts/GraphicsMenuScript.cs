using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*
 * GraphicsMenuScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         This class handles all operations of the graphics settings menu. 
 * 
 * Notes:
 * 
*/
public class GraphicsMenuScript : MonoBehaviour {

    public GameObject options_menu;
    public GameObject graphics_menu;
    public GameObject data_container;

    private SavedGraphicsScript local_data;

    int res_array_pointer;
    Resolution[] res_array;


    public Text res_text;
    public Text fov_text;
    public Text frame_cap_text;
    public Text pixelization_text;
    public Text gamma_text;

    public Toggle fullscreen_button;
    public Toggle vsync_button;
    public Toggle bilinear_filtering_button;
    public Toggle bloom_button;
    public Toggle flares_button;

    public Slider fov_slider;
    public Slider frame_cap_slider;
    public Slider pixelization_slider;
    public Slider gamma_slider;

    // Use this for initialization
    void Start ()
    {
        local_data = data_container.GetComponent<DataContainment>().graphics;

        res_array = new Resolution[] 
        {
            new Resolution(320,200),
            new Resolution(400,300),
            new Resolution(512,384),
            new Resolution(640,400),
            new Resolution(800,600),
            new Resolution(1024,768),
            new Resolution(1152,864),
            new Resolution(1280,600),
            new Resolution(1360,768),
            new Resolution(1400,1050),
            new Resolution(1440,900),
            new Resolution(1680,1050),
            new Resolution(1920,1080)
        };

        if (local_data == default(SavedGraphicsScript) ||
            local_data.res_width == 1920) { res_array_pointer = res_array.Length - 1; }
        else if (local_data.res_width == 320) { res_array_pointer = 0; }
        else if (local_data.res_width == 400) { res_array_pointer = 1; }
        else if (local_data.res_width == 512) { res_array_pointer = 2; }
        else if (local_data.res_width == 640) { res_array_pointer = 3; }
        else if (local_data.res_width == 800) { res_array_pointer = 4; }
        else if (local_data.res_width == 1024) { res_array_pointer = 5; }
        else if (local_data.res_width == 1152) { res_array_pointer = 6; }
        else if (local_data.res_width == 1280) { res_array_pointer = 7; }
        else if (local_data.res_width == 1360) { res_array_pointer = 8; }
        else if (local_data.res_width == 1400) { res_array_pointer = 9; }
        else if (local_data.res_width == 1440) { res_array_pointer = 10; }
        else if (local_data.res_width == 1680) { res_array_pointer = 11; }
        else { res_array_pointer = res_array.Length - 1; }

        fullscreen_button.isOn = local_data.fullscreen;
        vsync_button.isOn = local_data.vsync;
        bilinear_filtering_button.isOn = local_data.bilinear_filtering;
        bloom_button.isOn = local_data.bloom;
        flares_button.isOn = local_data.bloom;

        fov_slider.value = local_data.fov + 0.01f;
        frame_cap_slider.value = local_data.frame_cap + 0.01f;
        pixelization_slider.value = local_data.pixelization + 0.01f;
        gamma_slider.value = local_data.gamma + 0.01f;

        fov_text.text = local_data.fov + "";
        frame_cap_text.text = local_data.frame_cap + "";
        pixelization_text.text = local_data.pixelization + "";
        gamma_text.text = local_data.gamma + "";
    }
	
	// Update is called once per frame
	void Update ()
    {
        local_data.res_height = (res_array[res_array_pointer].get_height());
        local_data.res_width = (res_array[res_array_pointer].get_width());

        local_data.fullscreen = (fullscreen_button.isOn);
        local_data.vsync = (vsync_button.isOn);
        local_data.bilinear_filtering = (bilinear_filtering_button.isOn);
        local_data.bloom = (bloom_button.isOn);
        local_data.flares = (flares_button.isOn);

        local_data.fov = ((int)fov_slider.value);
        local_data.frame_cap = ((int)frame_cap_slider.value);
        local_data.pixelization = ((int)pixelization_slider.value);
        local_data.gamma = ((int)gamma_slider.value);

        res_text.text = local_data.res_width + " x " + local_data.res_height;

        fov_text.text = local_data.fov + "";
        frame_cap_text.text = local_data.frame_cap + "";
        pixelization_text.text = local_data.pixelization + "";
        gamma_text.text = local_data.gamma + " %";
    }

    public void Open_Options()
    {
        Open_Menu_Page(options_menu);
    }

    public void Res_Scroll_Down()
    {
        if (res_array_pointer > 0)
        {
            res_array_pointer -= 1;
        }
    }

    public void Res_Scroll_Up()
    {
        if (res_array_pointer < (res_array.Length - 1))
        {
            res_array_pointer += 1;
        }
    }

    public void ResSet(int index)
    {
        if ((res_array_pointer < (res_array.Length - 1)) && (res_array_pointer > 0))
        {
            res_array_pointer = index;
        }
    }

    public void Open_Menu_Page(GameObject page)
    {
        page.SetActive(true);
        graphics_menu.SetActive(false);
    }

    public void LoadGraphics()
    {
        local_data = SaveLoad.Load<SavedGraphicsScript>(Application.persistentDataPath + "/saves/graphicsettings.dat");
    }

    public void SaveGraphics()
    {
        SaveLoad.Save<SavedGraphicsScript>(local_data, "graphicsettings");
    }

    public void ApplyButton()
    {
        data_container.GetComponent<DataContainment>().graphics = local_data;
        Debug.Log(local_data.gamma);
    }

    public class Resolution
    {
        int width;
        int height;

        public Resolution() { }

        public Resolution(int width_new, int height_new)
        {
            width = width_new;
            height = height_new;
        }

        public void set_width(int width_new)
        {
            width = width_new;
        }

        public int get_width()
        {
            return width;
        }

        public void set_height(int height_new)
        {
            height = height_new;
        }

        public int get_height()
        {
            return height;
        }
    }
}
