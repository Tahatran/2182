using Nami.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class ShowLogFireBase : MonoBehaviour
{
    private static ShowLogFireBase instance;
    public static ShowLogFireBase Instance => instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

    }
    float timeStart = 0;
    float timeStartLevel = 0;
    float total_playtime = 0;
    public float time_win = 0;
    public int numberTrise = 0;
    public int totalImage = 0;
    public int totalSkin = 0;

    private void Start()
    {
        ResetValue();
        numberTrise += (PlayerPrefs.GetInt("numbertries") > 0 ? PlayerPrefs.GetInt("numbertries") : 0);
        totalImage += (PlayerPrefs.GetInt("totalImage") > 0 ? PlayerPrefs.GetInt("totalImage") : 0);
        totalSkin += (PlayerPrefs.GetInt("totalSkin") > 0 ? PlayerPrefs.GetInt("totalSkin") : 2);
        timeStartLevel -= (PlayerPrefs.GetFloat("timeplaylevel") > 0 ? PlayerPrefs.GetFloat("timeplaylevel") : 0);
        total_playtime += (PlayerPrefs.GetFloat("totalplaytime") > 0 ? PlayerPrefs.GetFloat("totalplaytime") : 0);
        // Debug.Log("Stat---------------: " + total_playtime);
        time_win = Time.time;
        timeStart = Time.time;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("pause application");
            ShowLogPauseQuit();
            SaveDataPlayLevel();
        }
    }
    private void OnApplicationQuit()
    {
        Debug.Log("quit application");
        ShowLogPauseQuit();
    }
    void SaveDataPlayLevel()
    {
        // PlayerPrefs.SetFloat("totalplaytime", totalPlayTime());
        PlayerPrefs.SetFloat("totalplaytime", totalPlayTime());
        PlayerPrefs.SetFloat("timeplaylevel", GetTimePlay());
        PlayerPrefs.SetInt("numbertries", GetNumberTriesLevel());
    }
    void ShowLogPauseQuit()
    {
        PlayerPrefs.SetFloat("totalplaytime", totalPlayTime());
        GameFirebase.SendEvent("pause_game", "id_level", PlayerPrefs.GetInt("lv").ToString(),
         "time_play", Mathf.Round(GetTimePlay()).ToString(),
          "number_tries", GetNumberTriesLevel().ToString(),
        "totalplaytime", Mathf.Round(totalPlayTime()).ToString(),
        "total_image", totalImage.ToString(),
        "total_skin", totalSkin.ToString()
        );
        // Debug.LogError("------------------------------lv=: " + PlayerPrefs.GetInt("lv").ToString());
        // Debug.LogError("------------------------------number tries:  " + GetNumberTriesLevel().ToString());
        // Debug.LogError("------------------------------timepplay: " + Mathf.Round(GetTimePlay()).ToString());
        // Debug.LogError("------------------------------total play time: " + Mathf.Round(totalPlayTime()).ToString());
        // Debug.LogError("------------------------------totalSkin" + totalSkin.ToString());
        // Debug.LogError("------------------------------totalimage" + totalImage.ToString());

    }
    void StartTimingLevel()
    {
        timeStartLevel = Time.time;

    }
    void timewin()
    {
        time_win = Time.time;
    }
    float GetTimePlay()
    {
        return Time.time - timeStartLevel;
    }
    public void AddNumberTriesLevel()
    {
        numberTrise++;
    }
    int GetNumberTriesLevel()
    {
        return numberTrise;
    }
    void StartNumberTries()
    {
        numberTrise = 1;
    }
    void ResetValue()
    {
        timewin();
        StartTimingLevel();
        StartNumberTries();
    }
    float totalPlayTime()
    {
        // Debug.Log("totalplaytime-----------------" + total_playtime);
        // Debug.Log("totalplaytime-----------------" + timeStart);
        return total_playtime + (Time.time - timeStart);
    }

    public void ShowCompleteLevel()
    {
        GameFirebase.SendEvent("complete_level", "id_level", PlayerPrefs.GetInt("lv").ToString(),
        "time_play", Mathf.Round(GetTimePlay()).ToString(),
         "number_tries", GetNumberTriesLevel().ToString()
        , "total_playtime", Mathf.Round(totalPlayTime()).ToString()
        , "time_win", Mathf.Round(Time.time - time_win).ToString()
        );
        // Debug.LogError("------------------------------lv=: " + PlayerPrefs.GetInt("lv").ToString());
        // Debug.LogError("------------------------------number tries:  " + GetNumberTriesLevel().ToString());
        // Debug.LogError("------------------------------timepplay: " + Mathf.Round(GetTimePlay()).ToString());
        // Debug.LogError("------------------------------total play time: " + Mathf.Round(totalPlayTime()).ToString());
        // Debug.LogError("------------------------------timnewin: " + Mathf.Round(Time.time - time_win).ToString());
        ResetValue();

    }
    public void ShowStartLevel()
    {
        GameFirebase.SendEvent("start_level", "id_level", PlayerPrefs.GetInt("lv").ToString(),
                                "total_playtime", Mathf.Round(totalPlayTime()).ToString());
        // Debug.LogError("-------------------------------lv:" + PlayerPrefs.GetInt("lv").ToString());
        // Debug.LogError("------------------------------totalplaytime" + Mathf.Round(totalPlayTime()).ToString());

    }

    public void LogBuildDone()
    {
        PlayerPrefs.SetInt("totalImage", totalImage += 1);
        GameFirebase.SendEvent("image_done", "id_image", DataConfig.ImageIndex.ToString(),
                                "total_image", totalImage.ToString());
        // Debug.LogError("------------------------------builddone id=: " + DataConfig.ImageIndex.ToString());
        // Debug.LogError("------------------------------totalimage" + totalImage.ToString());
    }
    public void LogChangeSkin(int id)
    {
        PlayerPrefs.SetInt("totalSkin", totalSkin += 1);
        GameFirebase.SendEvent("unlock_skin", "id_skin", id.ToString(),
                                "total_skin", totalSkin.ToString());
        // Debug.LogError("------------------------------Skin id=: " + id.ToString());
        // Debug.LogError("------------------------------totalSkin" + totalSkin.ToString());
    }

}
