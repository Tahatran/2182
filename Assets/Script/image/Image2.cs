using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image2 : MonoBehaviour
{
    [SerializeField] private GameObject levelText;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private GameObject parentScore;
    // Image
    public GameObject Image11;
    public GameObject Image12;
    public GameObject Image13;
    public GameObject Image14;
    // Add list
    public List<GameObject> Img;
    public List<GameObject> Img1;
    public List<GameObject> Img2;
    public List<GameObject> Img3;
    public List<GameObject> Img4;

    //
    public List<GameObject> Imgbg;
    public List<GameObject> Img1bg;
    public List<GameObject> Img2bg;
    public List<GameObject> Img3bg;
    public List<GameObject> Img4bg;
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

        int lv = 0;
        // Loop through image indices
        for (int i = 1; i <= 4; i++)
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
        for (int i = 1; i <= 4; i++)
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

        Debug.Log($"Loaded {activeCount} pieces for image {imageIndex}");
    }

    public void Fill()
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
            default:
                Debug.LogError("Invalid image index");
                return;
        }

        int activeCount = PlayerPrefs.GetInt(key, 0);
        Debug.Log($"Active count for image {DataConfig.ImageIndex}: {activeCount}");

        for (int i = activeCount; i < selectedImageList.Count && DataConfig.ScoreImage > 0; i++)
        {
            if (!selectedImageList[i].activeSelf)
            {
                selectedImageList[i].SetActive(true);
                selectedBgList[i].SetActive(false); // Tắt corresponding background
                DataConfig.ScoreImage--;
                PlayerPrefs.SetInt("ScoreImage", DataConfig.ScoreImage);
                activeCount++;
            }
        }

        PlayerPrefs.SetInt(key, activeCount);
        PlayerPrefs.Save();

        Debug.Log($"Remaining ScoreImage: {DataConfig.ScoreImage}");
        ShowScore();
    }

    public void ShowScore()
    {
        SetLevelText(parentScore);
    }

    void SetLevelText(GameObject parentLevelText)
    {
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
        float spacing = 0.2f; // điều chỉnh khoảng cách giữa các chữ số tùy thuộc vào yêu cầu của bạn

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
            }
            else
            {
                imageLevelClone.transform.localPosition = new Vector3(xPos, 0, 0);
            }

            imageLevelClone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            var imageLevel = imageLevelClone.GetComponent<SpriteRenderer>();

            // Gắn sprite tương ứng với chữ số
            imageLevel.sprite = sprites[digitValue];
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
