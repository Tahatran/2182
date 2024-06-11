using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameCtr : MonoBehaviour
{
    [SerializeField] private GameObject levelText;
    [SerializeField] private GameObject parentLevelText;
    [SerializeField] private GameObject LevelPannel;
    [SerializeField] private GameObject parentLevelTextLose;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private GameObject bgblue;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Winpanel;
    [SerializeField] private GameObject ScrewWin;
    [SerializeField] private GameObject WINtext;
    [SerializeField] private GameObject PLAYtext;
    [SerializeField] private GameObject btnReset;

    public List<GameObject> lstBling;
    [SerializeField] private GameObject LosePanel;
    public static GameCtr instance;
    public GameObject HexagridPrefab;
    public GameObject BulongPrefab;
    public GameObject ScrewPrefab;

    public GameObject BulongModelPrefab;
    public GameObject gridContainer;
    public GameObject objectContainer;

    public Toggle audioToggle;

    public int colNumber;
    public int rowNumber;

    public List<Sprite> BulongfacespriteList;
    public List<Sprite> BulongbodyspriteList;
    public List<Sprite> ScrewspriteList;
    public List<GridPrefab> lstGrid;

    public List<GameObject> lstBulong;
    public List<GameObject> lstCrew;

    public List<string> bulongTags = new List<string>(); // List chứa các tag của Bulong
    public List<string> crewTags = new List<string>(); // List chứa các tag của Screw
    public int lv = 1;

    private float hexWidth = 1.0f; // chiều rộng của một hexagon
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
        SetLevelText(parentLevelText);
        loadgame();
        // StartCoroutine(DelayedGenerateGrid());
    }

    void loadgame()
    {
        UI.transform.DOMoveY(-0.1f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                  {
                      parentLevelText.SetActive(true);
                      StartCoroutine(DelayedGenerateGrid());
                  });
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
        // Audio.instance.sfxClick.Stop();
        Audio.instance.sfxClick.Play();
        // Debug.Log("aaaa");
        SceneManager.LoadScene(0);
        // var rect = losePopup.GetComponent<RectTransform>();
        // rect.DOAnchorPos(new Vector2(rect.anchoredPosition.x, 2000), 0.5f)
        //     .SetEase(Ease.InFlash)
        //     .OnComplete(() => { SceneManager.LoadScene(0); });
    }
    public void SetLevelText(GameObject parentLevelText)
    {
        // Lấy giá trị level từ PlayerPrefs
        var level = PlayerPrefs.GetInt("lv");

        // Chuyển giá trị level thành chuỗi
        // string levelString = level.ToString();
        string levelString = level.ToString();
        // Debug.Log("level" + levelString);

        // Xóa tất cả các hình ảnh con trước đó (nếu có)
        foreach (Transform child in parentLevelText.transform)
        {
            Destroy(child.gameObject);
        }

        // Đặt khoảng cách giữa các chữ số
        float spacing = 0.55f; // điều chỉnh khoảng cách giữa các chữ số tùy thuộc vào yêu cầu của bạn

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
            imageLevelClone.transform.localPosition = new Vector3(xPos, 0, 0);
            imageLevelClone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            var imageLevel = imageLevelClone.GetComponent<SpriteRenderer>();

            // Gắn sprite tương ứng với chữ số
            imageLevel.sprite = sprites[digitValue];
        }
    }

    public void ReplayBtn()
    {
        // Audio.instance.sfxClick.Stop();
        Audio.instance.sfxClick.Play();
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
        if (lstCrew.Count != 1)
        {
            CheckTags();
        }

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
            if (crew.GetComponent<Screw>().HasBulong == false)
            {
                crewTags.Add(crew.tag);
            }
        }
    }

    void CheckTags()
    {
        // foreach (string tag in crewTags)
        // {
        //     if (!bulongTags.Contains(tag))
        //     {
        //         bgblue.transform.DOMoveY(-0.5f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
        //           {
        //               LosePanel.SetActive(true);
        //               SetLevelText(parentLevelTextLose);
        //           });
        //     }
        // }
        foreach (var screwObject in lstCrew)
        {
            var screw = screwObject.GetComponent<Screw>();

            if (!screw.HasBulong)
            {
                string screwTag = screw.tag;
                bool tagExists = false;

                foreach (var bulongObject in lstBulong)
                {
                    var bulong = bulongObject.GetComponent<BulongAction>();
                    if (bulong.tag == screwTag)
                    {
                        tagExists = true;
                        break;
                    }
                }

                if (!tagExists)
                {
                    // Debug.Log("aaa");
                    // Audio.instance.sfxLose.Stop();
                    // Input.
                    btnReset.SetActive(false);
                    audioToggle.gameObject.SetActive(false);
                    Audio.instance.sfxLose.Play();
                    var screw2 = screwObject.transform.Find("Screw").gameObject;
                    var spriteRenderer = screw2.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        Color originalColor = spriteRenderer.color;
                        spriteRenderer.DOColor(Color.red, 0.1f).SetLoops(5, LoopType.Yoyo).OnKill(() =>
                        {
                            spriteRenderer.color = originalColor;
                        }).OnComplete(() =>
                         {
                             LevelPannel.SetActive(false);
                             TweenScrews();
                             bgblue.transform.DOMoveY(-0.5f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                                {
                                    LosePanel.SetActive(true);
                                    SetLevelText(parentLevelTextLose);
                                });
                         });
                    }

                    break; // Thực hiện hàm khác và dừng kiểm tra thêm các `screw` khác
                }
            }
        }
    }

    void TweenScrews()
    {
        foreach (var screwObject in lstCrew)
        {
            var screw = screwObject.transform.Find("Screw").gameObject;
            var screwComponent = screwObject.GetComponent<Screw>();
            var bulong = screwComponent.Bulong;

            float xPosition = screw.transform.position.x;
            float targetX = xPosition < 0 ? -5f : 5f; // Xác định vị trí mục tiêu dựa trên giá trị x hiện tại

            // Bay lên và di chuyển theo hình parabol
            List<Vector3> path = new List<Vector3>
            {
                screw.transform.position,
                new Vector3(targetX, screw.transform.position.y + 2f, screw.transform.position.z),
                new Vector3(targetX, screw.transform.position.y - 5f, screw.transform.position.z)
            };

            // Thực hiện tween theo đường cong hình parabol
            screw.transform.DOPath(path.ToArray(), 2f, PathType.CatmullRom).SetEase(Ease.OutQuad);

            if (bulong != null)
            {
                // Bay lên và di chuyển theo hình parabol
                List<Vector3> bulongPath = new List<Vector3>
                {
                    bulong.transform.position,
                    new Vector3(targetX, bulong.transform.position.y + 2f, bulong.transform.position.z),
                    new Vector3(targetX, bulong.transform.position.y - 5f, bulong.transform.position.z)
                };

                // Thực hiện tween theo đường cong hình parabol
                bulong.transform.DOPath(bulongPath.ToArray(), 2f, PathType.CatmullRom).SetEase(Ease.OutQuad);
            }
        }
    }

    void CheckWin()
    {
        if (lstBulong.Count == 0)
        {
            // Audio.instance.sfxWin.Stop();
            Audio.instance.sfxWin.Play();
            GameObject lastCrew = lstCrew[0];
            btnReset.SetActive(false);
            audioToggle.gameObject.SetActive(false);
            Vector3 targetPosition = ScrewWin.transform.position;
            UI.transform.DOMoveY(1f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                  {
                      lastCrew.transform.DOMove(targetPosition, 0.2f).SetEase(Ease.OutQuad).OnUpdate(() =>
                    {
                        Winpanel.SetActive(true);
                        var spriteRenderer = ScrewWin.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            spriteRenderer.DOFade(1f, 0.5f); // Tăng opacity lên 255 (1f tương ứng với 255/255)
                        }
                    }).OnComplete(() =>
                        {
                            lastCrew.SetActive(false);
                            WINtext.transform.DOMoveX(-0.2f, 0.15f).SetEase(Ease.OutQuad).OnUpdate(() =>
                            {
                                PLAYtext.transform.DOMoveX(-4f, 0.15f).SetEase(Ease.OutQuad).OnComplete(() =>
                                {
                                    WINtext.transform.DOMoveX(-0.5f, 0.1f).SetEase(Ease.OutQuad);
                                    PLAYtext.transform.DOMoveX(-3.7f, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
                                    {
                                        StartCoroutine(ToggleGameObjectsContinuously(0.05f));
                                        NextLV();
                                    });
                                });
                            });


                        });
                  });
        }
    }

    public void NextLV()
    {
        // Call the delay method with a delay time of 0.5 seconds and the action to load the next level
        DelayMethod(0.8f, () =>
        {
            nextLv();
            SceneManager.LoadScene(0);
        });
    }

    // Method to handle the delay
    public void DelayMethod(float delay, System.Action action)
    {
        StartCoroutine(DelayCoroutine(delay, action));
    }

    // Coroutine to handle the delay and call the action
    private IEnumerator DelayCoroutine(float delay, System.Action action)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);

        // Execute the action
        action.Invoke();
    }


    IEnumerator ToggleGameObjectsContinuously(float toggleInterval)
    {
        while (true)
        {
            foreach (var gameObject in lstBling)
            {
                bool isActive = Random.Range(0, 2) == 0; // 50% cơ hội bật hoặc tắt
                gameObject.SetActive(isActive);
            }

            yield return new WaitForSeconds(toggleInterval);
        }
    }


    //dongf nafy de chay lai lan nua
    [ContextMenu("onGenerateObject")]
    void onGenerateObject()
    {
        // Lấy level hiện tại từ danh sách levels dựa trên giá trị lv lưu trong PlayerPrefs
        // var currentLevel = LVConfig.Instance.levels[0];
        // var currentLevel = LVConfig.Instance.levels[PlayerPrefs.GetInt("lv") - 1];

        //dễ nhất lv2
        var currentLevel = LVConfig.Instance.levels[2];
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
                        instantiatedObject.GetComponent<BulongAction>().col = subLevel.col;
                        instantiatedObject.GetComponent<BulongAction>().row = subLevel.row;
                        instantiatedObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        lstBulong.Add(instantiatedObject);
                    }
                    else if (subLevel.type == 2)
                    {
                        instantiatedObject.GetComponent<Screw>().col = subLevel.col;
                        instantiatedObject.GetComponent<Screw>().row = subLevel.row;
                        lstCrew.Add(instantiatedObject);
                    }
                }
            }
        }
        foreach (var screwObject in lstCrew)
        {
            var screw = screwObject.GetComponent<Screw>();
            var rowS = screw.row;
            var colS = screw.col;

            foreach (var bulongObject in lstBulong)
            {
                var bulongAction = bulongObject.GetComponent<BulongAction>();
                var rowB = bulongAction.row;
                var colB = bulongAction.col;

                if (rowS == rowB && colS == colB)
                {
                    screw.HasBulong = true;
                    screw.Bulong = bulongObject;
                    break; // Dừng vòng lặp khi đã tìm thấy Bulong tương ứng
                }
            }
        }
        // ReadTags();
    }

    void UpdateSprite(GameObject obj, SubLevel subLevel, int colorIndex)
    {
        // obj.GetComponent<Transform>().localScale = new Vector3(2f, 2f, 2f);
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
