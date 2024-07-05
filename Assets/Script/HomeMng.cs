using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMng : MonoBehaviour
{

    // public GameObject Home;
    public GameObject Skin;
    public GameObject Image;

    public static HomeMng instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void btnLoadGame()
    {
        gameObject.SetActive(false);
        GameCtr.instance.loadgame();
    }

    public void btnHome()
    {
        Skin.SetActive(false);
        Image.SetActive(false);
    }

    public void btnSkin()
    {
        Skin.SetActive(true);
        Image.SetActive(false);
    }

    public void btnImage()
    {
        Skin.SetActive(false);
        Image.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
