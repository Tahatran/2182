using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource sfxClick;
    public AudioSource sfxgone;
    public AudioSource sfxFire;
    public AudioSource sfxWin;
    public AudioSource sfxScrew;
    public AudioSource sfxLose;
    public AudioSource shak;
    public bool isStart = true;
    public AudioSource[] soundList;
    public GameObject panelStart;
    public bool isPanelStartOpen = false;

    [HideInInspector]
    public int musicState, soundState;
    public static Audio instance;

    private void Awake()
    {

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


        //bat len
        // sound
        GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
        if (PlayerPrefs.GetInt("Sound") == 1)
            soundState = 1;
        else soundState = 0;

        if (soundState == 1)
        {
            gameCtrInstance.audioToggle.isOn = false;
            ToogleSound(gameCtrInstance.audioToggle.isOn);
        }
        else
        {
            gameCtrInstance.audioToggle.isOn = true;
            ToogleSound(gameCtrInstance.audioToggle.isOn);
        }
        if (FindObjectsOfType(typeof(Audio)).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        Application.targetFrameRate = 60;


        DontDestroyOnLoad(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        isStart = true;
        isPanelStartOpen = true;
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

    public void ToogleSound(bool toogle)
    {
        GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
        toogle = !gameCtrInstance.audioToggle.isOn;
        Debug.Log("audio bool : " + toogle);
        if (toogle)
        {

            for (int i = 0; i < soundList.Length; i++)
            {
                soundList[i].volume = 1.0f;
            }
            soundList[5].volume = 0.3f;


            soundState = 1;
            PlayerPrefs.SetInt("Sound", 1);
        }

        else
        {
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
