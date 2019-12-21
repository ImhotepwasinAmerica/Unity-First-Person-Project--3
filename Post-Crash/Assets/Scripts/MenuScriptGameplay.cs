using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScriptGameplay : MonoBehaviour
{
    public Text squat_text, carry_text;
    public Slider enemy_health, enemy_damage;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("togglehold_carry"))
        {
            DeveloperPreferences.Gameplay();
        }

        squat_text.text = PlayerPrefs.GetString("togglehold_squat");
        carry_text.text = PlayerPrefs.GetString("togglehold_carry");

        enemy_health.value = PlayerPrefs.GetInt("enemy_health_percentage") + 0.01f;
        enemy_damage.value = PlayerPrefs.GetInt("enemy_damage_percentage") + 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetString("togglehold_squat", squat_text.text);
        PlayerPrefs.SetString("togglehold_carry", carry_text.text);

        PlayerPrefs.SetInt("enemy_health_percentage", (int)enemy_health.value);
        PlayerPrefs.SetInt("enemy_damage_percentage", (int)enemy_damage.value);
    }

    public void ButtonTextSet(Text button_text)
    {
        if (button_text.text == "toggle")
        {
            button_text.text = "hold";
        }
        else if(button_text.text == "hold")
        {
            button_text.text = "toggle";
        }
    }
}
