using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject Time_line;
    public List<GameObject> ImageBlur;
    public List<GameObject> SKinBlur;
    public List<GameObject> uiElements;
    public List<GameObject> lstTutorialSkins;
    public List<GameObject> lstTutorialImages;
    public static Tutorial instance;
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
        TutorialSkin();
        TutorialImage();
    }

    public void WatchTutorial()
    {
        Image1308.instance.lstUpgameobject.SetActive(false);
        HomeMng.instance.btnBack2.SetActive(false);
        HomeMng.instance.ImageGameObject1308.SetActive(false);
        Time_line.SetActive(true);
        int scoreImage = PlayerPrefs.GetInt("ScoreImage", 0);
        PlayerPrefs.SetInt("ScoreImage", scoreImage + 4);
        DataConfig.ScoreImage = PlayerPrefs.GetInt("ScoreImage", 0);
    }
    public IEnumerator TurnOffAfterDelay()
    {
        // Đợi 30 giây
        yield return new WaitForSeconds(30f);

        // Debug.LogError("AAAAAAAAAAAAAAAAAAA");

        // Tắt GameObject

        // HomeMng.instance.gameObject.SetActive(true);
        EnbleAllRaycasts();
        HomeMng.instance.ImageGameObject1308.SetActive(true);
        HomeMng.instance.btnBack2.SetActive(true);
        Image1308.instance.lstUpgameobject.SetActive(true);
        Image1308.instance.SetScore(Image1308.instance.parentScoreImage);
        Time_line.SetActive(false);
        // HomeMng.instance.gameObject.SetActive(true);
        // Image1308.instance.LoadSaveImage();
        // HomeMng.instance.ImageMng.GetComponent<ImageMng>().LoadShop();
        // Image1308.instance.ResetImage();

        // HomeMng.instance.ImgageSelect.SetActive(true);
        // HomeMng.instance.Skin.SetActive(false);
        // HomeMng.instance.Image.SetActive(true);
        // HomeMng.instance.HomeSelect.SetActive(false);
        // HomeMng.instance.SkinSelect.SetActive(false);

    }
    public void TutorialSkin()
    {
        if (!PlayerPrefs.HasKey("CheckTutorialSkin"))
        {
            int CheckTutorialSkin = 0;
            PlayerPrefs.SetInt("CheckTutorialSkin", CheckTutorialSkin);
        }
        if (PlayerPrefs.GetInt("lv") == 2 && PlayerPrefs.GetInt("CheckTutorialSkin") == 0)
        {
            lstTutorialSkins[0].SetActive(true);
            // DisableAllRaycasts();
            EnableRaycast(uiElements[1]);
            SKinBlur[0].SetActive(true);
        }
    }

    public void TutorialImage()
    {
        if (!PlayerPrefs.HasKey("CheckTutorialImage"))
        {
            int CheckTutorialImage = 0;
            PlayerPrefs.SetInt("CheckTutorialImage", CheckTutorialImage);
        }
        //1308
        // int activeCount = PlayerPrefs.GetInt("0");
        // if (activeCount != 4)
        // {
        if (PlayerPrefs.GetInt("lv") == 3 && PlayerPrefs.GetInt("CheckTutorialImage") == 0)
        {
            lstTutorialImages[0].SetActive(true);
            GameCtr.instance.DisableAllColliders();
            // DisableAllRaycasts();
            EnableRaycast(uiElements[1]);
            ImageBlur[0].SetActive(true);
        }
        // }




    }
    public void DisableAllRaycasts()
    {
        foreach (GameObject uiElement in uiElements)
        {
            SetRaycastTarget(uiElement, false);
        }
    }


    public void EnbleAllRaycasts()
    {
        foreach (GameObject uiElement in uiElements)
        {
            SetRaycastTarget(uiElement, true);
        }
    }
    private void SetRaycastTarget(GameObject element, bool enabled)
    {
        // Get all Graphic components (Image, Text, etc.) in the element
        Graphic[] graphics = element.GetComponentsInChildren<Graphic>();
        foreach (Graphic graphic in graphics)
        {
            graphic.raycastTarget = enabled;
        }
    }
    public void EnableRaycast(GameObject targetElement)
    {
        // Disable all raycasts first
        DisableAllRaycasts();

        // Enable raycast for the target element
        SetRaycastTarget(targetElement, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
