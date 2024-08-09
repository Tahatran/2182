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
    float time_win = 0;
    public int numberTrise = 0;
    public int totalImage = 0;
    public int totalSkin = 2;

    private void Start()
    {
        ResetValue();
        numberTrise += (PlayerPrefs.GetInt("numbertries") > 0 ? PlayerPrefs.GetInt("numbertries") : 0);
        totalImage = (PlayerPrefs.GetInt("totalImage") > 0 ? PlayerPrefs.GetInt("totalImage") : 0);
        totalSkin = (PlayerPrefs.GetInt("totalSkin") > 0 ? PlayerPrefs.GetInt("totalSkin") : 0);
        timeStartLevel -= (PlayerPrefs.GetFloat("timeplaylevel") > 0 ? PlayerPrefs.GetFloat("timeplaylevel") : 0);
        total_playtime += (PlayerPrefs.GetFloat("totalplaytime") > 0 ? PlayerPrefs.GetFloat("totalplaytime") : 0);
        Debug.Log("Stat---------------: " + total_playtime);
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
        PlayerPrefs.SetFloat("totalplaytime", totalPlayTime());
        PlayerPrefs.SetFloat("timeplaylevel", GetTimePlay());
        PlayerPrefs.SetInt("numbertries", GetNumberTriesLevel());
    }
    void ShowLogPauseQuit()
    {
        Debug.Log("set" + totalPlayTime().ToString());
        PlayerPrefs.SetFloat("totalplaytime", totalPlayTime());
        Debug.Log("Get" + PlayerPrefs.GetFloat("totalplaytime"));
        GameFirebase.SendEvent("pause_game", "id_level", PlayerPrefs.GetInt("lv").ToString(), "time_play", Mathf.Round(GetTimePlay()).ToString(), "number_tries", GetNumberTriesLevel().ToString(),
        "totalplaytime", Mathf.Round(totalPlayTime()).ToString(),
        "total_image", totalImage.ToString(),
        "total_skin", totalSkin.ToString()
        );
        // Debug.Log("alltime" + Mathf.Round(totalPlayTime()).ToString());
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
        "time_play", Mathf.Round(GetTimePlay()).ToString(), "number_tries", GetNumberTriesLevel().ToString()
        , "total_playtime", Mathf.Round(totalPlayTime()).ToString()
        , "time_win", Mathf.Round(Time.time - time_win).ToString()
        );
        // Debug.Log("timewin" + Mathf.Round(Time.time - time_win).ToString());
        // Debug.Log("totalplay" + Mathf.Round(totalPlayTime()).ToString());
        // Debug.Log("win" + GetNumberTriesLevel());
        ResetValue();

    }
    public void ShowStartLevel()
    {
        GameFirebase.SendEvent("start_level", "id_level", PlayerPrefs.GetInt("lv").ToString(),
                                "total_playtime", Mathf.Round(totalPlayTime()).ToString());

    }

    public void LogBuildDone()
    {
        totalImage += 1;
        PlayerPrefs.SetFloat("totalImage", totalImage);
        GameFirebase.SendEvent("image_done", "id_image", DataConfig.ImageIndex.ToString(),
                                "total_image", totalImage.ToString());
        // Debug.Log("builddone" + DataConfig.ImageIndex.ToString());
        // Debug.Log("totalimage" + totalImage.ToString());
    }
    public void LogChangeSkin(int id)
    {
        totalSkin += 1;
        PlayerPrefs.SetFloat("totalSkin", totalSkin);
        GameFirebase.SendEvent("unlock_skin", "id_skin", id.ToString(),
                                "total_skin", totalSkin.ToString());
        // Debug.Log("Slkin" + id.ToString());
    }

}
