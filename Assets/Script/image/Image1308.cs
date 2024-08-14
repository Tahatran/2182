using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Image1308 : MonoBehaviour
{
    //khoi tao list chua phan tu select
    public GameObject lstUpgameobject;
    public List<GameObject> lstUp;

    // khoi tao list cac hinh 
    public List<GameObject> lstDown;
    public List<Sprite> lstSprites;
    public List<GameObject> lstimgbg;
    public GameObject bulongObject;
    public GameObject menuPanel;
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
        gameObject.SetActive(false);
    }

    public void FillandSaveScore()
    {
        DataConfig.ScoreImage--;
        if (DataConfig.ScoreImage <= 0)
        {
            DataConfig.ScoreImage = 0;
            menuPanel.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            menuPanel.transform.GetChild(0).gameObject.SetActive(false);
        }

        PlayerPrefs.SetInt("ScoreImage", DataConfig.ScoreImage);
        SetScore(parentScoreImage);
    }

    public void LoadImage()
    {
        if (DataConfig.ScoreImage <= 0)
        {
            DataConfig.ScoreImage = 0;
            menuPanel.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            menuPanel.transform.GetChild(0).gameObject.SetActive(false);
        }

        lstUpgameobject.SetActive(true);
        LoadSaveImage();
        gameObject.SetActive(true);
        ResetImage();
        ResetSelect();
        lstUp[0].gameObject.transform.GetChild(0).gameObject.SetActive(true);
        idSelect = 0;
        lstimgbg[DataConfig.ImageIndex].SetActive(true);

        lstDown[DataConfig.ImageIndex].SetActive(true);

        SetScore(parentScoreImage);
    }



    //load owr imagemng
    public int ImageShowPanel(int i)
    {
        int totalCheckfill = 0;
        int totalChildren = 0;

        // for (int i = 0; i < lstDown.Count; i++)
        // {
        for (int j = 0; j < lstDown[i].transform.childCount; j++)
        {
            var screw = lstDown[i].transform.GetChild(j).GetComponent<setScrew>();
            totalChildren++;

            if (screw.Checkfill)
            {
                totalCheckfill++;
            }
        }
        // }

        if (totalCheckfill == 0)
        {
            return 0; // No `Checkfill` set to true
        }
        else if (totalCheckfill == totalChildren)
        {
            return 1; // All `Checkfill` are true
        }
        else
        {
            return 2; // Some `Checkfill` are true, but not all
        }
    }


    public void SaveImage()
    {
        List<string> savedData = new List<string>();

        for (int i = 0; i < lstDown.Count; i++)
        {
            for (int j = 0; j < lstDown[i].transform.childCount; j++)
            {
                var screw = lstDown[i].transform.GetChild(j).GetComponent<setScrew>();
                if (screw != null)
                {
                    string data = $"{screw.idSprite},{screw.Checkfill}";
                    savedData.Add(data);
                }
            }
        }

        string serializedData = string.Join(";", savedData);
        PlayerPrefs.SetString(SaveKey, serializedData);
        PlayerPrefs.Save();
    }


    public void LoadSaveImage()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string serializedData = PlayerPrefs.GetString(SaveKey);
            string[] savedData = serializedData.Split(';');

            int index = 0;
            for (int i = 0; i < lstDown.Count; i++)
            {
                for (int j = 0; j < lstDown[i].transform.childCount; j++)
                {
                    if (index >= savedData.Length)
                        return;

                    var screw = lstDown[i].transform.GetChild(j).GetComponent<setScrew>();
                    if (screw != null)
                    {
                        string[] data = savedData[index].Split(',');
                        if (data.Length == 2)
                        {
                            screw.idSprite = int.Parse(data[0]);
                            screw.Checkfill = bool.Parse(data[1]);

                            // Set the sprite based on the idSprite


                            // Change color if Checkfill is true
                            if (screw.Checkfill)
                            {
                                screw.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = lstSprites[screw.idSprite];
                                screw.gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                                // Color semiTransparentRed = new Color(screw.gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.r, screw.gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.g, screw.gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.b, 255);
                                // spriteColor = semiTransparentRed; // 1f is the maximum value for alpha in Unity's Color, equivalent to 255

                                // screw.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.red; // Change to your desired color
                            }
                        }
                        index++;
                    }
                }
            }
        }
    }

    public void ResetSelect()
    {
        for (int i = 0; i < lstUp.Count; i++)
        {
            for (int j = 0; j < lstUp[i].transform.childCount; j++)
            {
                lstUp[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void ResetImage()
    {
        // lstUpgameobject.SetActive(false);
        for (int i = 0; i < lstimgbg.Count; i++)
        {
            lstimgbg[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < lstDown.Count; i++)
        {
            lstDown[i].gameObject.SetActive(false);
        }

    }


    public void SetScore(GameObject parentScoreImage)
    {
        Debug.LogError(DataConfig.ScoreImage);
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

    public void ActivefalseAll()
    {

        for (int i = 0; i < lstDown.Count; i++)
        {
            lstDown[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
