using System.Collections;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;
using GoogleMobileAds.Api.Mediation.LiftoffMonetize;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource sfxClick;
    public AudioSource sfxgone;
    public AudioSource sfxFire;
    public AudioSource sfxWin;
    public AudioSource sfxScrew;
    public AudioSource sfxScrew2;
    public AudioSource sfxLose;
    public AudioSource shak;
    public AudioSource blink;
    public AudioSource fill;
    // public bool isStart = true;
    public AudioSource[] soundList;
    // public GameObject panelStart;
    public GameObject On;
    public GameObject Off;
    // public bool isPanelStartOpen = false;
    // public GameObject AudioPanel;

    // [HideInInspector]
    public int musicState, soundState;
    public static Audio instance;

    private void Awake()
    {

        // DontDestroyOnLoad(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject foundObject = GameObject.Find("On");
        On = foundObject;
        GameObject foundObject2 = GameObject.Find("Off");
        Off = foundObject2;

        //

        // if (!PlayerPrefs.HasKey("Music"))
        //     PlayerPrefs.SetInt("Music", 1);
        if (!PlayerPrefs.HasKey("Sound"))
            PlayerPrefs.SetInt("Sound", 1);
        //music
        // if (PlayerPrefs.GetInt("Music") == 1)
        //     musicState = 1;
        // else musicState = 0;

        // if (musicState == 1)
        // {        
        //     ToogleMusic(true);
        // }
        // else
        // {          
        //     ToogleMusic(false);
        // }

        // sound
        // GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
        if (PlayerPrefs.GetInt("Sound") == 1)
            soundState = 1;
        else soundState = 0;

        if (soundState == 1)
        {
            // On.SetActive(true);
            // Off.SetActive(false);
            for (int i = 0; i < soundList.Length; i++)
            {
                soundList[i].volume = 1.0f;
            }
            // gameCtrInstance.audioToggle.isOn = false;
            // ToogleSound();
        }
        else
        {
            // On.SetActive(false);
            // Off.SetActive(true);
            for (int i = 0; i < soundList.Length; i++)
                soundList[i].volume = 0.0f;
            soundState = 0;
            // gameCtrInstance.audioToggle.isOn = true;
            // ToogleSound();
        }
        if (FindObjectsOfType(typeof(Audio)).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        Application.targetFrameRate = 120;


        // isStart = true;
        // isPanelStartOpen = true;
    }

    public void AudioLoad()
    {
        GameObject foundObject = GameObject.Find("On");
        On = foundObject;
        GameObject foundObject2 = GameObject.Find("Off");
        Off = foundObject2;
        if (PlayerPrefs.GetInt("Sound") == 1)
            soundState = 1;
        else soundState = 0;

        if (soundState == 1)
        {
            On.SetActive(true);
            Off.SetActive(false);
            for (int i = 0; i < soundList.Length; i++)
            {
                soundList[i].volume = 1.0f;
            }
            // gameCtrInstance.audioToggle.isOn = false;
            // ToogleSound();
        }
        else
        {
            On.SetActive(false);
            Off.SetActive(true);
            for (int i = 0; i < soundList.Length; i++)
                soundList[i].volume = 0.0f;
            soundState = 0;
            // gameCtrInstance.audioToggle.isOn = true;
            // ToogleSound();
        }
    }
    public void ToogleMusic(bool toogle)
    {
        if (toogle)
        {
            musicState = 1;
            backgroundMusic.volume = 0.2f;
            PlayerPrefs.SetInt("Music", 1);
        }

        else
        {
            musicState = 0;
            backgroundMusic.volume = 0.0f;
            PlayerPrefs.SetInt("Music", 0);
        }

    }

    public void ToogleSound()
    {
        GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
        // toogle = !gameCtrInstance.audioToggle.isOn;
        // Debug.Log("audio bool : ");
        if (Off.activeSelf == true || On.activeSelf == false)
        {
            On.SetActive(true);
            Off.SetActive(false);
            for (int i = 0; i < soundList.Length; i++)
            {
                soundList[i].volume = 1.0f;
            }
            // soundList[0].volume = 0.3f;


            soundState = 1;
            PlayerPrefs.SetInt("Sound", 1);
        }

        else if (On.activeSelf == true || Off.activeSelf == false)
        {
            On.SetActive(false);
            Off.SetActive(true);
            for (int i = 0; i < soundList.Length; i++)
                soundList[i].volume = 0.0f;
            soundState = 0;
            PlayerPrefs.SetInt("Sound", 0);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
