using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSetter : Singleton<WeatherSetter>
{


#if UNITY_EDITOR
    private void Start()
    {
        StartCoroutine(TestCode());
    }

    public IEnumerator TestCode()
    {
        Set_Random_Weather();
        yield return new WaitForSeconds(2);
        StartCoroutine(TestCode());
    }
#endif
    public Light main_light;
    public Color32 midnight_sunlight_color;
    public Color32 clouding_sunlight_color;
    public Color32 raining_sunlight_color;


    public Color32 midnight_fog_color;
    public Color32 clouding_fog_color;
    public Color32 raining_fog_color;

    public Material midnight_material;
    public Material clouding_material;
    public Material rainig_material;

    public GameObject raining_additional_effect;
    public GameObject clouding_additional_effect;
    public enum weather_type
    {
    RAINNING,
    CLOUDING,
    MIDNIGHT
    }
    weather_type weather;
    public weather_type p_weather
    {
        get {return weather; }
        set { weather = value; Update_Weather(); }
    }
    public void Set_Weather(weather_type Weather)
    {
        p_weather = Weather;
    }
    public void Set_Random_Weather()
    {
        int count = System.Enum.GetValues(typeof(weather_type)).Length;
        Set_Weather((weather_type)Random.Range(0, count));
    }
    public weather_type Get_Current_Weather()
    {
        return p_weather;
    }

    /// <summary>
    /// Recommend Start = -173  End = 14.75 <-이수치들 한번 재조정 필요 적당히 잘보이는값임
    /// </summary>
    /// <param name="on_off"></param>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    public void Fog_Setting(bool on_off, float Start, float End)
    {
        RenderSettings.fog = on_off;
        if(on_off)
        {
            RenderSettings.fogStartDistance = Start;
            RenderSettings.fogEndDistance = End;
            
        }
    }
    public void Fog_Setting(bool on_off)
    {
        RenderSettings.fog = on_off;
    }
    public void Fog_Color(Color32 fog_color)
    {
        RenderSettings.fogColor = fog_color;
    }
    public void Skybox_Setting(Material skybox)
    {
        RenderSettings.skybox = skybox;
    }
    
    public void Update_Weather()
    {
         switch(p_weather)
         {
            case weather_type.RAINNING:
                Fog_Setting(true, -173, 10f);
                Fog_Color(raining_fog_color);
                Skybox_Setting(rainig_material);
                break;
            case weather_type.CLOUDING:
                Fog_Setting(true, -173, 10f);
                Fog_Color(clouding_fog_color);
                Skybox_Setting(clouding_material);
                break;
            case weather_type.MIDNIGHT:
                Fog_Setting(false);
                Fog_Color(midnight_fog_color);
                Skybox_Setting(midnight_material);
                break;
         }
    }
}
