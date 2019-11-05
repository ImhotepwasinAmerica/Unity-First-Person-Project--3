using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SavedGraphicsScript
 * Author:          Andrew Potisk
 * Finalized on:    --/--/----
 * Purpose:         This class contains all saved graphics settings
 * 
 * Notes:
 * 
*/
[System.Serializable]
public class SavedGraphicsScript
{
    public int res_width;
    public int res_height;

    public bool fullscreen;
    public bool vsync;
    public bool bilinear_filtering;
    public bool bloom;
    public bool flares;

    public int fov;
    public int frame_cap;
    public int pixelization;
    public int gamma;
    
    public void DeveloperPreferences()
    {
        res_width = 1920;
        res_height = 1080;

        fullscreen = true;
        vsync = false;
        bilinear_filtering = true;
        bloom = false;
        flares = false;

        fov = 100;
        frame_cap = 100;
        pixelization = 0;
        gamma = 100;
    }
}
