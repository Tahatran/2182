using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMng : MonoBehaviour
{

    public GameObject ImageGameObject;
    public GameObject Home;
    public GameObject HomeSelect;
    public GameObject Skin;
    public GameObject SkinSelect;
    public GameObject Image;
    public GameObject ImgageSelect;
    public GameObject btnBack;

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
    public void btnBacktoImage()
    {
        // ImageCtr.instance.GenLevelfromGrid();
        Image2.instance.ActivefalseAll();
        btnBack.SetActive(false);
        gameObject.SetActive(true);
        btnImage();
    }

    public void btncometoHome()
    {
        // GameCtr.instance.DisableAllColliders();

        GameCtr.instance.GameClear();
        ImageGameObject.SetActive(true);
        gameObject.SetActive(true);
        Audio.instance.AudioLoad();

    }


    public void btnLoadGame()
    {
        ImageGameObject.SetActive(false);
        gameObject.SetActive(false);
        foreach (Transform child in GameCtr.instance.gridContainer.transform)
        {
            Destroy(child.gameObject);
        }
        SceneManager.LoadScene(0);
    }

    public void btnHome()
    {
        // Home.SetActive(true);
        HomeSelect.SetActive(true);
        Skin.SetActive(false);
        SkinSelect.SetActive(false);
        ImgageSelect.SetActive(false);
        Image.SetActive(false);
    }

    public void btnSkin()
    {
        SkinSelect.SetActive(true);
        Skin.SetActive(true);
        Image.SetActive(false);
        HomeSelect.SetActive(false);
        ImgageSelect.SetActive(false);
    }

    public void btnImage()
    {
        ImgageSelect.SetActive(true);
        Skin.SetActive(false);
        Image.SetActive(true);
        HomeSelect.SetActive(false);
        SkinSelect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
