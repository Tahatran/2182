using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image2 : MonoBehaviour
{
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
        LoadAllImages();
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

    void LoadAllImages()
    {
        for (int i = 1; i <= 4; i++)
        {
            LoadImage(i);
        }
    }

    void LoadImage(int imageIndex)
    {
        List<GameObject> selectedImageList = null;
        string key = imageIndex.ToString();

        switch (imageIndex)
        {
            case 1:
                selectedImageList = Img1;
                break;
            case 2:
                selectedImageList = Img2;
                break;
            case 3:
                selectedImageList = Img3;
                break;
            case 4:
                selectedImageList = Img4;
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
            }
        }

        Debug.Log($"Loaded {activeCount} pieces for image {imageIndex}");
    }

    public void Fill(int imageIndex)
    {
        List<GameObject> selectedImageList = null;
        string key = imageIndex.ToString();

        switch (imageIndex)
        {
            case 1:
                selectedImageList = Img1;
                break;
            case 2:
                selectedImageList = Img2;
                break;
            case 3:
                selectedImageList = Img3;
                break;
            case 4:
                selectedImageList = Img4;
                break;
            default:
                Debug.LogError("Invalid image index");
                return;
        }

        int activeCount = PlayerPrefs.GetInt(key, 0);
        Debug.Log($"Active count for image {imageIndex}: {activeCount}");

        for (int i = activeCount; i < selectedImageList.Count && DataConfig.ScoreImage > 0; i++)
        {
            if (!selectedImageList[i].activeSelf)
            {
                selectedImageList[i].SetActive(true);
                DataConfig.ScoreImage--;
                activeCount++;
            }
        }

        PlayerPrefs.SetInt(key, activeCount);
        PlayerPrefs.Save();

        Debug.Log($"Remaining ScoreImage: {DataConfig.ScoreImage}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
