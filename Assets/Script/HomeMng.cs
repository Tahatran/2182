using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMng : MonoBehaviour
{

    public GameObject face1;
    public GameObject face2;
    public GameObject bulong;
    public GameObject bulongscale;
    private Coroutine toggleCoroutine;
    public GameObject ImageGameObject;

    public GameObject ImageMng;
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

        if (Tutorial.instance.lstTutorialImages[5].activeSelf)
        {
            Tutorial.instance.ImageBlur[5].SetActive(false);
            Tutorial.instance.lstTutorialImages[5].SetActive(false);

            // Tutorial.instance.lstTutorialImages[5].SetActive(true);
            int CheckTutorialImage = 1;
            PlayerPrefs.SetInt("CheckTutorialImage", CheckTutorialImage);
            Tutorial.instance.EnbleAllRaycasts();
            ImageMng.GetComponent<ImageMng>().LoadShop();
        }

    }

    public void btncometoHome()
    {
        // GameCtr.instance.DisableAllColliders();

        GameCtr.instance.GameClear();
        ImageGameObject.SetActive(true);
        gameObject.SetActive(true);
        Audio.instance.AudioLoad();
        if (Tutorial.instance.lstTutorialSkins[0].activeSelf)
        {
            Tutorial.instance.SKinBlur[0].SetActive(false);
            Tutorial.instance.SKinBlur[1].SetActive(true);
            Tutorial.instance.lstTutorialSkins[0].SetActive(false);
            Tutorial.instance.lstTutorialSkins[1].SetActive(true);
            Tutorial.instance.EnableRaycast(Tutorial.instance.uiElements[7]);
        }
        if (Tutorial.instance.lstTutorialImages[0].activeSelf)
        {
            Tutorial.instance.ImageBlur[0].SetActive(false);
            Tutorial.instance.ImageBlur[1].SetActive(true);
            Tutorial.instance.lstTutorialImages[0].SetActive(false);
            Tutorial.instance.lstTutorialImages[1].SetActive(true);
            Tutorial.instance.EnableRaycast(Tutorial.instance.uiElements[8]);
        }
    }
    IEnumerator ToggleFacesWhileTweening(GameObject BuLongface, GameObject Bulongface2)
    {
        while (true)
        {
            ToggleBulongFaces(BuLongface, Bulongface2);
            // BuLongface.SetActive(true);
            // Bulongface2.SetActive(false);
            yield return new WaitForSeconds(0.02f); // Điều chỉnh thời gian chờ theo ý muốn
        }
    }
    void ToggleBulongFaces(GameObject BuLongface, GameObject Bulongface2)
    {
        // Bật tắt xen kẽ giữa Bulongface và Bulongface2
        if (BuLongface.activeSelf)
        {
            BuLongface.SetActive(false);
            Bulongface2.SetActive(true);
        }
        else
        {
            BuLongface.SetActive(true);
            Bulongface2.SetActive(false);
        }
    }

    public void btnLoadGame()
    {
        Tutorial.instance.lstTutorialSkins[5].SetActive(false);
        Tutorial.instance.lstTutorialImages[5].SetActive(false);
        Tutorial.instance.DisableAllRaycasts();
        var bulongFaceDown = face1;
        var bulongFace2Down = face2;


        // Tạo vị trí mới cho Bulong để di chuyển xuống dưới
        Vector3 downPosition = new Vector3(0, -319, 0);
        if (toggleCoroutine != null)
        {
            StopCoroutine(toggleCoroutine);
            bulongFaceDown.SetActive(true);
            bulongFace2Down.SetActive(false);
        }
        toggleCoroutine = StartCoroutine(ToggleFacesWhileTweening(bulongFaceDown, bulongFace2Down));
        Tutorial.instance.SKinBlur[5].SetActive(false);
        // Tween Bulong xuống vị trí mới
        bulong.transform.DOLocalMove(downPosition, 0.5f).SetEase(Ease.OutQuad).OnUpdate(() =>
                    {
                        DOVirtual.DelayedCall(0.05f, () =>
                        {
                            bulongscale.transform.DOScale(0f, 0.2f).SetEase(Ease.OutQuad);
                        });
                    })
                    .OnComplete(() =>
                                    {

                                        if (toggleCoroutine != null)
                                        {
                                            StopCoroutine(toggleCoroutine);
                                        }
                                        bulongFace2Down.SetActive(true);
                                        bulongFaceDown.SetActive(false);

                                        DOVirtual.DelayedCall(0.12f, () =>
                                        {

                                            ImageGameObject.SetActive(false);
                                            gameObject.SetActive(false);
                                            foreach (Transform child in GameCtr.instance.gridContainer.transform)
                                            {
                                                Destroy(child.gameObject);
                                            }
                                            SceneManager.LoadScene(0);
                                        });

                                    });


        // ImageGameObject.SetActive(false);
        // gameObject.SetActive(false);
        // foreach (Transform child in GameCtr.instance.gridContainer.transform)
        // {
        //     Destroy(child.gameObject);
        // }
        // SceneManager.LoadScene(0);
    }

    public void btnHome()
    {
        // Home.SetActive(true);
        HomeSelect.SetActive(true);
        Skin.SetActive(false);
        SkinSelect.SetActive(false);
        ImgageSelect.SetActive(false);
        Image.SetActive(false);
        if (Tutorial.instance.lstTutorialSkins[4].activeSelf)
        {
            Tutorial.instance.SKinBlur[4].SetActive(false);
            Tutorial.instance.SKinBlur[5].SetActive(true);
            Tutorial.instance.lstTutorialSkins[4].SetActive(false);
            Tutorial.instance.lstTutorialSkins[5].SetActive(true);
            Tutorial.instance.EnableRaycast(Tutorial.instance.uiElements[9]);
            int CheckTutorialSkin = 1;
            PlayerPrefs.SetInt("CheckTutorialSkin", CheckTutorialSkin);
        }
    }

    public void btnSkin()
    {
        if (Tutorial.instance.lstTutorialSkins[1].activeSelf)
        {
            Tutorial.instance.lstTutorialSkins[1].SetActive(false);
            Tutorial.instance.lstTutorialSkins[2].SetActive(true);
            Tutorial.instance.SKinBlur[1].SetActive(false);
            Tutorial.instance.SKinBlur[2].SetActive(true);

        }
        SkinSelect.SetActive(true);
        Skin.SetActive(true);
        Image.SetActive(false);
        HomeSelect.SetActive(false);
        ImgageSelect.SetActive(false);
    }

    public void btnImage()
    {
        if (Tutorial.instance.lstTutorialImages[1].activeSelf)
        {
            Tutorial.instance.lstTutorialImages[1].SetActive(false);
            Tutorial.instance.lstTutorialImages[2].SetActive(true);
            Tutorial.instance.ImageBlur[1].SetActive(false);
            Tutorial.instance.ImageBlur[2].SetActive(true);
        }
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
