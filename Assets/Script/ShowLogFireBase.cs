using Nami.Controller;
using System.Collections;
using System.Collections.Generic;
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

    float timeStartLevel = 0;
    public int numberTrise = 0;

    private void Start()
    {
        ResetValue();
        numberTrise += (PlayerPrefs.GetInt("numbertries") > 0 ? PlayerPrefs.GetInt("numbertries") : 0);
        timeStartLevel -= (PlayerPrefs.GetFloat("timeplaylevel") > 0 ? PlayerPrefs.GetFloat("timeplaylevel") : 0);
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
        PlayerPrefs.SetFloat("timeplaylevel", GetTimePlay());
        PlayerPrefs.SetInt("numbertries", GetNumberTriesLevel());
    }
    void ShowLogPauseQuit()
    {
        GameFirebase.SendEvent("pause_game", "id_level", PlayerPrefs.GetInt("lv").ToString(), "time_play", Mathf.Round(GetTimePlay()).ToString(), "number_tries", GetNumberTriesLevel().ToString());
    }
    void StartTimingLevel()
    {
        timeStartLevel = Time.time;

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
        StartTimingLevel();
        StartNumberTries();
    }

    public void ShowCompleteLevel()
    {
        GameFirebase.SendEvent("complete_level", "id_level", PlayerPrefs.GetInt("lv").ToString(), "time_play", Mathf.Round(GetTimePlay()).ToString(), "number_tries", GetNumberTriesLevel().ToString());
        // Debug.Log("win" + GetNumberTriesLevel());
        ResetValue();

    }
    public void ShowStartLevel()
    {
        GameFirebase.SendEvent("start_level", "id_level", PlayerPrefs.GetInt("lv").ToString());

    }

}
