using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Image2 : MonoBehaviour
{
    public GameObject Shader;
    public GameObject btnFill;
    [SerializeField] private GameObject levelText;
    // [SerializeField] private List<Sprite> sprites;
    [SerializeField] private GameObject parentScore;
    // Image
    public GameObject Image11;
    public GameObject Image12;
    public GameObject Image13;
    public GameObject Image14;
    public GameObject Image15;
    public GameObject Image16;
    // Add list
    public List<GameObject> lstEnimFinal;
    public List<GameObject> lstImgFinal;
    public List<GameObject> Img;
    public List<GameObject> Img1;
    public List<GameObject> Img2;
    public List<GameObject> Img3;
    public List<GameObject> Img4;
    public List<GameObject> Img5;
    public List<GameObject> Img6;

    //
    public List<GameObject> Imgbg;
    public List<GameObject> Img1bg;
    public List<GameObject> Img2bg;
    public List<GameObject> Img3bg;
    public List<GameObject> Img4bg;
    public List<GameObject> Img5bg;
    public List<GameObject> Img6bg;
    public int Dem = 0;

    public static Image2 instance;

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
        SetupFirst();
        //cái dưới nên gọi lúc selcet image

        gameObject.SetActive(false);
    }

    void SetupFirst()
    {
        Img.Add(Image11);
        Img.Add(Image12);
        Img.Add(Image13);
        Img.Add(Image14);
        Img.Add(Image15);
        Img.Add(Image16);

        int lv = 0;
        // Loop through image indices
        for (int i = 1; i <= 5; i++)
        {
            string key = i.ToString();
            if (!PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.SetInt(key, lv);
            }
        }
    }

    public void LoadAllImages()
    {
        for (int i = 1; i <= 5; i++)
        {
            LoadImage(i);
        }
    }

    public void LoadImage(int imageIndex)
    {
        List<GameObject> selectedImageList = null;
        List<GameObject> selectedBgList = null;
        string key = imageIndex.ToString();

        switch (imageIndex)
        {

            case 0:
                selectedImageList = Img1;
                selectedBgList = Img1bg;
                break;
            case 1:
                selectedImageList = Img2;
                selectedBgList = Img2bg;
                break;
            case 2:
                selectedImageList = Img3;
                selectedBgList = Img3bg;
                break;
            case 3:
                selectedImageList = Img4;
                selectedBgList = Img4bg;
                break;
            case 4:
                selectedImageList = Img5;
                selectedBgList = Img5bg;
                break;
            case 5:
                selectedImageList = Img6;
                selectedBgList = Img6bg;
                break;
            default:
                Debug.LogError("Invalid image index");
                return;
        }

        int activeCount = PlayerPrefs.GetInt(key, 0);

        for (int i = 0; i < activeCount; i++)
        {
            if (i < selectedImageList.Count)
            {

                selectedImageList[i].SetActive(true);
                selectedBgList[i].SetActive(false); // Tắt corresponding background
            }
        }
        ShowImageFinal2();
        Debug.Log($"Loaded {activeCount} pieces for image {imageIndex}");
    }

    IEnumerator ShowShader()
    {
        if (btnFill.activeSelf)
        {
            btnFill.SetActive(false);
            // var imageLevelClone = Instantiate(Shader, Vector2.zero, Quaternion.identity, gameObject.transform);
            // imageLevelClone.transform.localPosition = Shader.transform.localPosition;
            // imageLevelClone.SetActive(true);
            lstEnimFinal[DataConfig.ImageIndex].SetActive(true);
            int CheckTutorialImage = 1;
            PlayerPrefs.SetInt("CheckTutorialImage", CheckTutorialImage);
            yield return new WaitForSeconds(2.8f); // Change the delay time as needed
                                                   // if (lstEnimFinal[DataConfig.ImageIndex].activeSelf)
                                                   // {
                                                   // lstEnimFinal[DataConfig.ImageIndex].GetComponent<Animator>().Rebind();
                                                   // lstEnimFinal[DataConfig.ImageIndex].GetComponent<Animation>().Play();
                                                   // }
                                                   // lstEnimFinal[DataConfig.ImageIndex].SetActive(false);
                                                   // yield return new WaitForSeconds(0.2f);

            Audio.instance.blink.Play();
            lstEnimFinal[DataConfig.ImageIndex].SetActive(false);
            var imageLevelClone = Instantiate(Shader, Vector2.zero, Quaternion.identity, gameObject.transform);
            imageLevelClone.transform.localPosition = Shader.transform.localPosition;
            imageLevelClone.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            lstImgFinal[DataConfig.ImageIndex].SetActive(true);
            yield return new WaitForSeconds(1.5f);
            if (Dem == 4 && PlayerPrefs.GetInt("lv") == 5 && DataConfig.ScoreImage == 0)
            {
                Tutorial.instance.ImageBlur[4].SetActive(false);
                Tutorial.instance.ImageBlur[5].SetActive(true);
                Tutorial.instance.lstTutorialImages[4].SetActive(false);
                Tutorial.instance.lstTutorialImages[5].SetActive(true);
                Tutorial.instance.EnableRaycast(Tutorial.instance.uiElements[2]);
            }

        }

    }

    public void Fill()
    {
        Graphic[] graphics = btnFill.GetComponentsInChildren<Graphic>();
        foreach (Graphic graphic in graphics)
        {
            graphic.raycastTarget = false;
        }
        List<GameObject> selectedImageList = null;
        List<GameObject> selectedBgList = null;
        string key = DataConfig.ImageIndex.ToString();

        switch (DataConfig.ImageIndex)
        {
            case 0:
                selectedImageList = Img1;
                selectedBgList = Img1bg;
                break;
            case 1:
                selectedImageList = Img2;
                selectedBgList = Img2bg;
                break;
            case 2:
                selectedImageList = Img3;
                selectedBgList = Img3bg;
                break;
            case 3:
                selectedImageList = Img4;
                selectedBgList = Img4bg;
                break;
            case 4:
                selectedImageList = Img5;
                selectedBgList = Img5bg;
                break;
            case 5:
                selectedImageList = Img6;
                selectedBgList = Img6bg;
                break;
            default:
                Debug.LogError("Invalid image index");
                return;
        }

        int activeCount = PlayerPrefs.GetInt(key, 0);
        Debug.Log($"Active count for image {DataConfig.ImageIndex}: {activeCount}");

        // for (int i = activeCount; i < selectedImageList.Count && DataConfig.ScoreImage > 0; i++)
        // {
        //     if (!selectedImageList[i].activeSelf)
        //     {
        //         selectedImageList[i].SetActive(true);
        //         selectedBgList[i].SetActive(false); // Tắt corresponding background
        //         DataConfig.ScoreImage--;
        //         PlayerPrefs.SetInt("ScoreImage", DataConfig.ScoreImage);
        //         activeCount++;
        //     }
        // }

        StartCoroutine(ActivateImagesWithDelay(selectedImageList, selectedBgList, activeCount, key));

        // ShowImageFinal();
        // StartCoroutine(DelayeShowFinal());

        Debug.Log($"Remaining ScoreImage: {DataConfig.ScoreImage}");
        Dem += 1;
        if (Tutorial.instance.lstTutorialImages[4].activeSelf && Dem == 4 && PlayerPrefs.GetInt("lv") == 5)
        {
            Tutorial.instance.ImageBlur[4].SetActive(false);
            // Tutorial.instance.ImageBlur[5].SetActive(true);
            Tutorial.instance.lstTutorialImages[4].SetActive(false);
            // Tutorial.instance.lstTutorialImages[5].SetActive(true);
            Tutorial.instance.EnableRaycast(Tutorial.instance.uiElements[2]);
        }
        if (Dem < 4 && DataConfig.ScoreImage == 0 && PlayerPrefs.GetInt("lv") == 5)
        {
            Tutorial.instance.ImageBlur[4].SetActive(false);
            Tutorial.instance.ImageBlur[5].SetActive(true);
            Tutorial.instance.lstTutorialImages[4].SetActive(false);
            Tutorial.instance.lstTutorialImages[5].SetActive(true);
            Tutorial.instance.EnableRaycast(Tutorial.instance.uiElements[2]);
        }
    }

    private IEnumerator ActivateImagesWithDelay(List<GameObject> selectedImageList, List<GameObject> selectedBgList, int activeCount, string key)
    {

        if (!selectedImageList[activeCount].activeSelf && DataConfig.ScoreImage > 0)
        {
            Audio.instance.fill.Play();
            selectedImageList[activeCount].SetActive(true);
            selectedBgList[activeCount].SetActive(false); // Tắt corresponding background
            DataConfig.ScoreImage--;
            if (DataConfig.ScoreImage <= 0)
            {
                DataConfig.ScoreImage = 0;
            }

            PlayerPrefs.SetInt("ScoreImage", DataConfig.ScoreImage);
            activeCount++;
            ShowScore();
            yield return new WaitForSeconds(0.01f); // Adjust delay duration here
        }
        // for (int i = activeCount; i < selectedImageList.Count && DataConfig.ScoreImage > 0; i++)
        // {
        //     if (!selectedImageList[i].activeSelf)
        //     {
        //         selectedImageList[i].SetActive(true);
        //         selectedBgList[i].SetActive(false); // Tắt corresponding background
        //         DataConfig.ScoreImage--;
        //         PlayerPrefs.SetInt("ScoreImage", DataConfig.ScoreImage);
        //         activeCount++;
        //         ShowScore();
        //         yield return new WaitForSeconds(0.35f); // Adjust delay duration here
        //     }
        // }

        PlayerPrefs.SetInt(key, activeCount);
        PlayerPrefs.Save();
        StartCoroutine(DelayeShowFinal());
    }
    IEnumerator DelayeShowFinal()
    {
        yield return new WaitForSeconds(0.5f); // Change the delay time as needed
        Graphic[] graphics = btnFill.GetComponentsInChildren<Graphic>();
        foreach (Graphic graphic in graphics)
        {
            graphic.raycastTarget = true;
        }
        ShowImageFinal();
    }

    public void ShowImageFinal()
    {
        List<GameObject> selectedImageList = null;
        List<GameObject> selectedBgList = null;
        string key = DataConfig.ImageIndex.ToString();

        switch (DataConfig.ImageIndex)
        {
            case 0:
                selectedImageList = Img1;
                selectedBgList = Img1bg;
                break;
            case 1:
                selectedImageList = Img2;
                selectedBgList = Img2bg;
                break;
            case 2:
                selectedImageList = Img3;
                selectedBgList = Img3bg;
                break;
            case 3:
                selectedImageList = Img4;
                selectedBgList = Img4bg;
                break;
            case 4:
                selectedImageList = Img5;
                selectedBgList = Img5bg;
                break;
            case 5:
                selectedImageList = Img6;
                selectedBgList = Img6bg;
                break;
            default:
                Debug.LogError("Invalid image index");
                return;
        }
        int activeCount = PlayerPrefs.GetInt(key, 0);
        if (activeCount == selectedImageList.Count)
        {
            for (int i = 0; i < selectedImageList.Count; i++)
            {
                selectedImageList[i].SetActive(false);
                selectedBgList[i].SetActive(false);
            }
            StartCoroutine(ShowShader());

        }

    }
    public void ShowImageFinal2()
    {
        List<GameObject> selectedImageList = null;
        List<GameObject> selectedBgList = null;
        string key = DataConfig.ImageIndex.ToString();

        switch (DataConfig.ImageIndex)
        {
            case 0:
                selectedImageList = Img1;
                selectedBgList = Img1bg;
                break;
            case 1:
                selectedImageList = Img2;
                selectedBgList = Img2bg;
                break;
            case 2:
                selectedImageList = Img3;
                selectedBgList = Img3bg;
                break;
            case 3:
                selectedImageList = Img4;
                selectedBgList = Img4bg;
                break;
            case 4:
                selectedImageList = Img5;
                selectedBgList = Img5bg;
                break;
            case 5:
                selectedImageList = Img6;
                selectedBgList = Img6bg;
                break;
            default:
                Debug.LogError("Invalid image index");
                return;
        }
        int activeCount = PlayerPrefs.GetInt(key, 0);
        if (activeCount == selectedImageList.Count)
        {
            for (int i = 0; i < selectedImageList.Count; i++)
            {
                selectedImageList[i].SetActive(false);
                selectedBgList[i].SetActive(false);
            }
            // StartCoroutine(ShowShader());
            lstImgFinal[DataConfig.ImageIndex].SetActive(true);
            btnFill.SetActive(false);

        }
        else
        {
            btnFill.SetActive(true);
        }


    }

    public void ShowScore()
    {
        SetLevelText(parentScore);
    }

    void SetLevelText(GameObject parentLevelText)
    {
        //tools
        // DataConfig.ScoreImage = 10;
        // Lấy giá trị level từ PlayerPrefs
        // var level = PlayerPrefs.GetInt("lv");
        // string levelString = level.ToString();
        string levelString = DataConfig.ScoreImage.ToString();

        // Xóa tất cả các hình ảnh con trước đó (nếu có)
        foreach (Transform child in parentLevelText.transform)
        {
            Destroy(child.gameObject);
        }

        // Đặt khoảng cách giữa các chữ số
        float spacing = 0.8f; // điều chỉnh khoảng cách giữa các chữ số tùy thuộc vào yêu cầu của bạn

        // Duyệt qua từng chữ số trong chuỗi levelString
        for (int i = 0; i < levelString.Length; i++)
        {
            // Chuyển chữ số thành giá trị số nguyên
            int digitValue = (int)char.GetNumericValue(levelString[i]);

            // Instantiate một bản sao của levelText và đặt nó làm con của parentLevelText
            var imageLevelClone = Instantiate(levelText, Vector2.zero, Quaternion.identity, parentLevelText.transform);

            // Tính toán vị trí x của hình ảnh chữ số
            float xPos = i * spacing;

            // Đặt vị trí của imageLevelClone
            if (int.Parse(levelString) < 10)
            {
                imageLevelClone.transform.localPosition = new Vector3(xPos + 0.3f, 0, 0);
                imageLevelClone.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }
            else if (int.Parse(levelString) > 9 && int.Parse(levelString) < 100)
            {
                imageLevelClone.transform.localPosition = new Vector3(xPos - 0.1f, 0, 0);
                imageLevelClone.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            else
            {
                xPos = i * (spacing - 0.15f);
                imageLevelClone.transform.localPosition = new Vector3(xPos - 0.3f, 0, 0);
                imageLevelClone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

            // imageLevelClone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            var imageLevel = imageLevelClone.GetComponent<SpriteRenderer>();

            // Gắn sprite tương ứng với chữ số
            imageLevel.sprite = GameCtr.instance.sprites[digitValue];
            // imageLevel.color = new Color(51 / 255f, 208 / 255f, 248 / 255f, 255 / 255f);
        }
    }
    // Update is called once per frame

    public void ActivefalseAll()
    {

        for (int i = 0; i < Img.Count; i++)
        {
            Img[i].SetActive(false);
        }
    }
    void Update()
    {

    }
}
