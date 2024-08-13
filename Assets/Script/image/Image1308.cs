using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Image1308 : MonoBehaviour
{
    //khoi tao list chua phan tu select
    public List<GameObject> lstUp;

    // khoi tao list cac hinh 
    public List<GameObject> lstDown;
    public List<Sprite> lstSprites;
    public GameObject bulongObject;
    public int idSelect = 0;
    [SerializeField] private GameObject scoreImagePrefab;
    [SerializeField] private GameObject parentScoreImage;
    private const string SaveKey = "SavedIdSprites";

    public static Image1308 instance;

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
        // Transform[] children = new Transform[menuObj.transform.childCount];
        // for (int i = 0; i < menuObj.transform.childCount; i++)
        // {
        //     children[i] = menuObj.transform.GetChild(i);
        // }


        // foreach (Transform child in gridContainer.transform)
        // {
        //     Destroy(child.gameObject);
        // }
    }

    public void LoadImage()
    {
        lstDown[DataConfig.ImageIndex].SetActive(true);

    }

    public void SaveImage()
    {
        List<int> savedIdSprites = new List<int>();

        foreach (GameObject item in lstDown)
        {
            setScrew[] screws = item.GetComponentsInChildren<setScrew>();
            foreach (var screw in screws)
            {
                savedIdSprites.Add(screw.idSprite);
            }
        }

        // Convert the list of integers to a string
        string serializedData = string.Join(",", savedIdSprites);

        // Save the string to PlayerPrefs
        PlayerPrefs.SetString(SaveKey, serializedData);
        PlayerPrefs.Save();
    }

    public void LoadSaveImage()
    {
        // Check if the key exists
        if (PlayerPrefs.HasKey(SaveKey))
        {
            // Retrieve the string from PlayerPrefs
            string serializedData = PlayerPrefs.GetString(SaveKey);

            // Convert the string back to a list of integers
            string[] stringArray = serializedData.Split(',');
            List<int> savedIdSprites = new List<int>();

            foreach (string s in stringArray)
            {
                if (int.TryParse(s, out int id))
                {
                    savedIdSprites.Add(id);
                }
            }

            // Apply the saved idSprites to the objects
            int index = 0;

            foreach (GameObject item in lstDown)
            {
                setScrew[] screws = item.GetComponentsInChildren<setScrew>();
                foreach (var screw in screws)
                {
                    if (index < savedIdSprites.Count)
                    {
                        screw.idSprite = savedIdSprites[index];
                        screw.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = lstSprites[screw.idSprite];
                        index++;
                    }
                }
            }
        }
    }


    public void SetScore(GameObject parentScoreImage)
    {
        string levelString = DataConfig.ScoreImage.ToString();

        // Xóa tất cả các hình ảnh con trước đó (nếu có)
        foreach (Transform child in parentScoreImage.transform)
        {
            Destroy(child.gameObject);
        }

        // Xóa tất cả các hình ảnh con trước đó (nếu có)
        foreach (Transform child in parentScoreImage.transform)
        {
            Destroy(child.gameObject);
        }

        // Đặt khoảng cách giữa các chữ số
        float spacing = 115f; // điều chỉnh khoảng cách giữa các chữ số tùy thuộc vào yêu cầu của bạn

        // Duyệt qua từng chữ số trong chuỗi levelString
        for (int i = 0; i < levelString.Length; i++)
        {
            // Chuyển chữ số thành giá trị số nguyên
            int digitValue = (int)char.GetNumericValue(levelString[i]);

            // Instantiate một bản sao của levelText và đặt nó làm con của parentLevelText
            var imageLevelClone = Instantiate(scoreImagePrefab, Vector2.zero, Quaternion.identity, parentScoreImage.transform);

            // Tính toán vị trí x của hình ảnh chữ số
            float xPos = i * spacing;
            // Đặt vị trí của imageLevelClone
            RectTransform rectTransform = imageLevelClone.GetComponent<RectTransform>();
            if (int.Parse(levelString) < 10)
            {
                rectTransform.localPosition = new Vector3(-115f, 0, 0);
            }
            else
            {
                rectTransform.localPosition = new Vector3(xPos - 125f, 0, 0);
            }
            imageLevelClone.GetComponent<Image>().SetNativeSize();
            // imageLevelClone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            var imageLevel = imageLevelClone.GetComponent<Image>();

            // Gắn sprite tương ứng với chữ số
            imageLevel.sprite = GameCtr.instance.sprites[digitValue];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
