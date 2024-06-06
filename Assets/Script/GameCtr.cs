using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameCtr : MonoBehaviour
{
    [SerializeField] private GameObject levelText;
    [SerializeField] private GameObject parentLevelText;
    [SerializeField] private List<Sprite> sprites;
    public static GameCtr instance;
    public GameObject HexagridPrefab;
    public GameObject BulongPrefab;
    public GameObject ScrewPrefab;

    public GameObject BulongModelPrefab;
    public GameObject gridContainer;
    public GameObject objectContainer;

    public int colNumber;
    public int rowNumber;
    public List<GameObject> listPrefabs;

    public List<Sprite> BulongfacespriteList;
    public List<Sprite> BulongbodyspriteList;
    public List<Sprite> ScrewspriteList;
    public List<GridPrefab> lstGrid;

    public List<GameObject> lstBulong;
    public List<GameObject> lstCrew;

    private List<string> bulongTags = new List<string>(); // List chứa các tag của Bulong
    private List<string> crewTags = new List<string>(); // List chứa các tag của Screw
    public int lv = 1;
    // public Text Map;

    private float hexWidth = 1.0f; // chiều rộng của một hexagon
    // private float hexHeight = Mathf.Sqrt(1.25f) / 2 * 1.0f;
    private float hexHeight = Mathf.Sqrt(0.8f) / 2 * 1.0f;

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
        Input.multiTouchEnabled = false;
        setUpLv();
        StartCoroutine(DelayedGenerateGrid());
    }

    IEnumerator DelayedGenerateGrid()
    {
        yield return new WaitForSeconds(0.5f); // Change the delay time as needed
        onGenerateGrid();
    }
    public void buttonNext()
    {
        nextLv();
        SceneManager.LoadScene(0);
    }
    public void ReplayLoseBtn()
    {
        // var rect = losePopup.GetComponent<RectTransform>();
        // rect.DOAnchorPos(new Vector2(rect.anchoredPosition.x, 2000), 0.5f)
        //     .SetEase(Ease.InFlash)
        //     .OnComplete(() => { SceneManager.LoadScene(0); });
    }
    public void SetLevelText()
    {
        var level = PlayerPrefs.GetInt("lv");
        string levelString = level.ToString();
        foreach (var digit in levelString)
        {
            int digitValue = (int)char.GetNumericValue(digit);
            var imageLevelClone = Instantiate(levelText, Vector2.zero, Quaternion.identity, parentLevelText.transform);
            var imageLevel = imageLevelClone.GetComponent<Image>();
            imageLevel.sprite = sprites[digitValue];
        }
    }
    public void ReplayBtn()
    {
        SceneManager.LoadScene(0);
    }

    void setUpLv()
    {
        if (!PlayerPrefs.HasKey("lv"))
        {
            lv = 1;
            PlayerPrefs.SetInt("lv", lv);
        }

    }

    public void nextLv()
    {
        // GameFirebase.SendEvent("level-win", PlayerPrefs.GetInt("lv").ToString());

        int lv = PlayerPrefs.GetInt("lv") + 1;
        if (lv > 15)
        {
            lv = 1;
        }
        PlayerPrefs.SetInt("lv", lv);
    }

    void onGenerateGrid()
    {
        for (int i = 0; i < rowNumber; i++)
        {
            for (int j = 0; j < colNumber; j++)
            {
                // Tính toán vị trí của từng hexagon
                float xPos = j * hexWidth * 0.5f;
                float yPos = i * hexHeight;

                // Nếu hàng là hàng lẻ thì dịch chuyển vị trí của hexagon
                if (j % 2 == 1)
                {
                    yPos += hexHeight / 2;
                }

                var tempGrid = Instantiate(HexagridPrefab, gridContainer.transform);
                tempGrid.transform.localPosition = new Vector3(xPos, yPos, 10);
                var gridRender = tempGrid.GetComponent<GridPrefab>();
                gridRender.col = j;
                gridRender.row = i;
                lstGrid.Add(gridRender);
            }
        }
        onGenerateObject();
    }

    public void CheckLose()
    {
        CheckWin();
        CheckTags();
    }

    void ReadTags()
    {
        bulongTags.Clear();
        crewTags.Clear();

        foreach (GameObject bulong in lstBulong)
        {
            bulongTags.Add(bulong.tag);
        }

        foreach (GameObject crew in lstCrew)
        {
            crewTags.Add(crew.tag);
        }
    }

    void CheckTags()
    {
        foreach (string tag in bulongTags)
        {
            if (!crewTags.Contains(tag))
            {
                Debug.Log("Lose: Tag " + tag + " from lstBulong is not in lstCrew");
            }
        }
    }

    void CheckWin()
    {
        if (lstBulong.Count == 0)
        {
            Debug.Log("Win");
        }
    }


    //dongf nafy de chay lai lan nua
    [ContextMenu("onGenerateObject")]
    void onGenerateObject()
    {
        // Lấy level hiện tại từ danh sách levels dựa trên giá trị lv lưu trong PlayerPrefs
        var currentLevel = LVConfig.Instance.levels[0];
        // Chọn ngẫu nhiên một sub-level từ danh sách sub-levels của level hiện tại
        int randomSubLevelIndex = Random.Range(0, currentLevel.subLevelsLists.Count);
        var subLevels = currentLevel.subLevelsLists[randomSubLevelIndex];

        // Cập nhật text của Map với chỉ số sub-level được chọn
        // Map.text = "Map: " + randomSubLevelIndex.ToString();
        // Debug.Log("Map: " + randomSubLevelIndex);

        // Duyệt qua từng phần tử trong subLevels
        foreach (var subLevel in subLevels)
        {
            GameObject prefabToInstantiate = null;

            // Chọn prefab phù hợp dựa trên loại (type) của subLevel
            if (subLevel.type == 1)
            {
                prefabToInstantiate = BulongPrefab;
            }
            else if (subLevel.type == 2)
            {
                prefabToInstantiate = ScrewPrefab;
            }

            // Nếu prefab được chọn không null
            if (prefabToInstantiate != null)
            {
                // Tìm grid tương ứng với vị trí của subLevel (dựa trên row và col)
                var targetGrid = lstGrid.Find(grid => grid.row == subLevel.row && grid.col == subLevel.col);

                // Nếu tìm thấy targetGrid
                if (targetGrid)
                {
                    // Instantiate đối tượng prefab tại vị trí của targetGrid
                    var instantiatedObject = Instantiate(prefabToInstantiate, targetGrid.transform.position, Quaternion.identity, targetGrid.transform);
                    // Cập nhật tỷ lệ scale của đối tượng tạo ra
                    instantiatedObject.transform.localScale = new Vector3(1.66f, 1.66f, 1.66f);

                    // Cập nhật sprite của đối tượng được tạo dựa trên subLevel và colorIndex
                    UpdateSprite(instantiatedObject, subLevel, subLevel.color);

                    // Đặt parent của đối tượng được tạo là objectContainer
                    instantiatedObject.transform.SetParent(this.objectContainer.transform);

                    // Thêm đối tượng vào danh sách tương ứng (lstBulong hoặc lstCrew) dựa trên loại của subLevel
                    if (subLevel.type == 1)
                    {
                        lstBulong.Add(instantiatedObject);
                    }
                    else if (subLevel.type == 2)
                    {
                        lstCrew.Add(instantiatedObject);
                    }
                }
            }
        }
        ReadTags();
    }

    void UpdateSprite(GameObject obj, SubLevel subLevel, int colorIndex)
    {
        // Nếu loại của subLevel là 1 (Bulong)
        if (subLevel.type == 1)
        {
            // Tìm và cập nhật sprite của phần thân và mặt của Bulong
            var bulongBody = obj.transform.Find("Bulongbody").GetComponent<SpriteRenderer>();
            var bulongFace = obj.transform.Find("Bulongface").GetComponent<SpriteRenderer>();
            var bulongFace2 = obj.transform.Find("Bulongface2").GetComponent<SpriteRenderer>();

            bulongBody.sprite = LVConfig.Instance.BulongBodyColor[colorIndex];
            if (colorIndex == 0 || colorIndex == 2 || colorIndex == 3 || colorIndex == 5)
            {
                // obj.transform.Find("Bulongbody").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 90f);
                bulongBody.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            //chỉnh lại góc quay do hình bị lệch.
            bulongFace.sprite = LVConfig.Instance.BulongFaceColor[colorIndex];
            if (colorIndex == 2 || colorIndex == 4 || colorIndex == 5)
            {
                // obj.transform.Find("Bulongface").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 90f);
                bulongFace.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }

            bulongFace2.sprite = LVConfig.Instance.BulongFaceColor2[colorIndex];
            if (colorIndex == 2 || colorIndex == 5)
            {
                // obj.transform.Find("Bulongface2").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 90f);
                bulongFace2.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
            //gắn tag
            obj.tag = colorIndex.ToString();
        }
        // Nếu loại của subLevel là 2 (Screw)
        else if (subLevel.type == 2)
        {
            // Tìm và cập nhật sprite của Screw
            var scew = obj.transform.Find("Screw").GetComponent<SpriteRenderer>();
            scew.sprite = LVConfig.Instance.ScewColor[colorIndex];
            if (colorIndex == 7)
            {
                obj.transform.Find("Screw").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            obj.tag = colorIndex.ToString();
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
