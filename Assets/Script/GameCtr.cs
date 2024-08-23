using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nami.Controller;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
using Unity.VisualScripting;

// [System.Serializable]
// public class Serialization<T>
// {
//     public List<T> target;

//     public Serialization(List<T> target)
//     {
//         this.target = target;
//     }
// }
public class GameCtr : MonoBehaviour
{
    // adb shell setprop debug.firebase.analytics.app nami.screw.tinkerer.puzzlegame

    public List<Sprite> lstWinSprite;
    public GameObject WinRewardPanel;
    public GameObject bglose;
    public GameObject TutorLv2;
    public GameObject Loading;
    [SerializeField] private GameObject levelText;
    [SerializeField] private GameObject levelText2;
    [SerializeField] private GameObject parentLevelText;
    [SerializeField] private GameObject LevelPannel;
    [SerializeField] private GameObject parentLevelTextLose;
    [SerializeField] public List<Sprite> sprites;
    [SerializeField] private GameObject bgblue;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Winpanel;
    [SerializeField] private GameObject Winpanel2;
    [SerializeField] private GameObject btnNextlvAds;
    [SerializeField] private GameObject ScrewWin;
    [SerializeField] private GameObject WINtext;
    [SerializeField] private GameObject PLAYtext;
    [SerializeField] private GameObject btnReset;
    [SerializeField] private GameObject SoundandReplay;

    public List<GameObject> lstBling;
    [SerializeField] private GameObject LosePanel;
    public static GameCtr instance;
    public GameObject HexagridPrefab;
    public GameObject BulongPrefab;
    public GameObject ScrewPrefab;

    public GameObject BulongModelPrefab;
    public GameObject gridContainer;
    public GameObject objectContainer;

    public GameObject audioToggle;

    public int colNumber;
    public int rowNumber;

    public List<GridPrefab> lstGrid;

    public List<GameObject> lstBulong;
    public List<GameObject> lstCrew;

    public int lv = 1;
    private float hexWidth = 1.0f; // chiều rộng của một hexagon
    private float hexHeight = Mathf.Sqrt(0.8f) / 2 * 1.0f;
    private bool isNextLvCalled = false;

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
        //tools tắt
        GameAds.Get.ShowBanner();
        GameAds.Get.ShowInterstitialAd();

        // DataConfig.ScoreImage = PlayerPrefs.GetInt("ScoreImage", 0);
        DataConfig.ScoreImage = PlayerPrefs.GetInt("ScoreImage", 12);
        //BuildTurnoff
        // DataConfig.ScoreImage = 100;
        DOTween.KillAll();
        // Input.multiTouchEnabled = false;
        setUpLv();
        SetLevelText(parentLevelText);
        //tools thì tắt
        Loadgame();
        ShowLogFireBase.Instance.ShowStartLevel();
        ShowLogFireBase.Instance.time_win = Time.time;
    }
    public void TurnOffTurtorialLv2()
    {
        TutorLv2.SetActive(false);
    }

    public void DisableAllColliders()
    {
        foreach (GameObject obj in lstCrew)
        {
            obj.GetComponent<Screw>().enabled = false;
            obj.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    public void EnbleAllColliders()
    {
        foreach (GameObject obj in lstCrew)
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }

    public void Loadgame()
    {
        Input.multiTouchEnabled = false;
        UI.transform.DOMoveY(-0.4f, 0.3f).SetEase(Ease.OutQuad).OnUpdate(() =>
        {
            SoundandReplay.GetComponent<RectTransform>().DOAnchorPosY(-185, 0.3f).SetEase(Ease.OutQuad);
        })
        .OnComplete(() =>
                  {
                      parentLevelText.SetActive(true);
                      StartCoroutine(DelayedGenerateGrid());
                  });
    }
    public void GameClear()
    {
        UI.transform.DOMoveY(1f, 0.3f).SetEase(Ease.OutQuad);
        lstGrid.Clear();
        lstBulong.Clear();
        lstCrew.Clear();
        // DisableAllColliders();
        btnReset.SetActive(false);
        audioToggle.SetActive(false);
        foreach (Transform child in gridContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in objectContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    IEnumerator DelayedGenerateGrid()
    {
        yield return new WaitForSeconds(0.5f); // Change the delay time as needed
        onGenerateGrid();
    }
    public void ButtonNext()
    {
        nextLv();
        SceneManager.LoadScene(0);
    }
    public void ReplayLoseBtn()
    {
        Audio.instance.sfxClick.Play();
        GameFirebase.SendEvent("Level", "level-game-lose " + PlayerPrefs.GetInt("lv"));
        SceneManager.LoadScene(0);
    }
    public void SetLevelText(GameObject parentLevelText)
    {
        var level = PlayerPrefs.GetInt("lv");  // Lấy giá trị level từ PlayerPrefs
        string levelString = level.ToString();     // Chuyển giá trị level thành chuỗi
        // string levelString = "2";
        foreach (Transform child in parentLevelText.transform)   // Xóa tất cả các hình ảnh con trước đó (nếu có)
        {
            Destroy(child.gameObject);
        }
        float spacing = 0.89f; // điều chỉnh khoảng cách giữa các chữ số tùy thuộc vào yêu cầu của bạn

        for (int i = 0; i < levelString.Length; i++)   // Duyệt qua từng chữ số trong chuỗi levelString
        {
            int digitValue = (int)char.GetNumericValue(levelString[i]);   // Chuyển chữ số thành giá trị số nguyên
            var imageLevelClone = Instantiate(levelText, Vector2.zero, Quaternion.identity, parentLevelText.transform); // Instantiate một bản sao của levelText và đặt nó làm con của parentLevelText
            float xPos = i * spacing;  // Tính toán vị trí x của hình ảnh chữ số
            if (int.Parse(levelString) < 10)
            {
                imageLevelClone.transform.localPosition = new Vector3(xPos - 0.03f, 0, 0);    // Đặt vị trí của imageLevelClone
            }
            else
            {
                imageLevelClone.transform.localPosition = new Vector3(xPos - 0.48f, 0, 0);
            }
            imageLevelClone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            var imageLevel = imageLevelClone.GetComponent<SpriteRenderer>();
            imageLevel.sprite = sprites[digitValue];  // Gắn sprite tương ứng với chữ số
        }
    }

    public void SetLevelTextEnd(GameObject parentLevelText)
    {
        var level = PlayerPrefs.GetInt("lv");  // Lấy giá trị level từ PlayerPrefs
        string levelString = level.ToString();      // Chuyển giá trị level thành chuỗi
        // string levelString = "10";   
        foreach (Transform child in parentLevelText.transform)
        {
            Destroy(child.gameObject);  // Xóa tất cả các hình ảnh con trước đó (nếu có)
        }
        float spacing = 108f; // điều chỉnh khoảng cách giữa các chữ số tùy thuộc vào yêu cầu của bạn
        for (int i = 0; i < levelString.Length; i++)   // Duyệt qua từng chữ số trong chuỗi levelString
        {
            // Chuyển chữ số thành giá trị số nguyên
            int digitValue = (int)char.GetNumericValue(levelString[i]);

            // Instantiate một bản sao của levelText và đặt nó làm con của parentLevelText
            var imageLevelClone = Instantiate(levelText2, Vector2.zero, Quaternion.identity, parentLevelText.transform);

            // Tính toán vị trí x của hình ảnh chữ số
            float xPos = i * spacing;
            // Đặt vị trí của imageLevelClone
            RectTransform rectTransform = imageLevelClone.GetComponent<RectTransform>();
            if (int.Parse(levelString) < 10)
            {
                rectTransform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                rectTransform.localPosition = new Vector3(xPos - 50f, 0, 0);
            }
            imageLevelClone.GetComponent<Image>().SetNativeSize();
            // imageLevelClone.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            var imageLevel = imageLevelClone.GetComponent<Image>();

            // Gắn sprite tương ứng với chữ số
            imageLevel.sprite = sprites[digitValue];
        }
    }

    public void ReplayBtn()
    {
        // Audio.instance.sfxClick.Stop();
        Audio.instance.sfxClick.Play();
        ShowLogFireBase.Instance.AddNumberTriesLevel();
        // xoa cac phan tu 
        lstBulong.Clear();
        lstCrew.Clear();
        foreach (Transform child in objectContainer.transform)
        {
            Destroy(child.gameObject);
        }
        UI.transform.DOMoveY(0.1f, 0.3f).SetEase(Ease.OutQuad).OnUpdate(() =>
        {
            SoundandReplay.GetComponent<RectTransform>().DOAnchorPosY(115, 0.3f).SetEase(Ease.OutQuad);
        })
        .OnComplete(() =>
                  {
                      UI.transform.DOMoveY(-0.4f, 0.3f).SetEase(Ease.OutQuad).OnUpdate(() =>
                        {
                            SoundandReplay.GetComponent<RectTransform>().DOAnchorPosY(-185, 0.3f).SetEase(Ease.OutQuad);
                        })
                          .OnComplete(() =>
                                {
                                    // StartCoroutine(DelayedGenerateGrid());
                                    onGenerateObject();
                                });
                  });
        // SceneManager.LoadScene(0);
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
        int lv = PlayerPrefs.GetInt("lv") + 1;
        if (lv > 25)
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
    public void onGenerateGrid2(GameObject a)
    {
        for (int i = 0; i < rowNumber; i++)
        {
            for (int j = 0; j < colNumber; j++)
            {
                // Tính toán vị trí của từng hexagon
                float xPos = j * hexWidth * 0.3f;
                float yPos = i * hexHeight * 0.7f;

                // Nếu hàng là hàng lẻ thì dịch chuyển vị trí của hexagon
                if (j % 2 == 1)
                {
                    yPos += hexHeight / 2.5f;
                }

                var tempGrid = Instantiate(a, gridContainer.transform);
                tempGrid.transform.localPosition = new Vector3(xPos, yPos, 10);
                var gridRender = tempGrid.GetComponent<GridPrefab>();
                gridRender.col = j;
                gridRender.row = i;
                lstGrid.Add(gridRender);
            }
        }
        // onGenerateObject();
    }

    public void btnTest()
    {
        foreach (var screwObject in lstCrew)
        {
            var screw = screwObject.GetComponent<Screw>();
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
                            SetLevelTextEnd(parentLevelTextLose);
                        });
                 });
            }
        }
    }
    public void CheckLose()
    {
        CheckWin();
        if (lstCrew.Count != 1)
        {
            CheckTags();
        }

    }

    void CheckTags()
    {
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
                    btnReset.SetActive(false);
                    audioToggle.gameObject.SetActive(false);
                    Audio.instance.sfxLose.Play();
                    ShowLogFireBase.Instance.AddNumberTriesLevel();
                    Debug.Log(ShowLogFireBase.Instance.numberTrise);
                    GameFirebase.SendEvent("Level", "level-game-lose " + PlayerPrefs.GetInt("lv"));
                    var screw2 = screwObject.transform.Find("Screw").gameObject;
                    var spriteRenderer = screw2.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        Color originalColor = spriteRenderer.color;
                        spriteRenderer.DOColor(Color.red, 0.1f).SetLoops(8, LoopType.Yoyo).OnKill(() =>
                        {
                            // spriteRenderer.color = originalColor;
                            spriteRenderer.color = Color.red;
                        }).OnComplete(() =>
                         {
                             DOVirtual.DelayedCall(0.65f, () =>
                                      {
                                          LevelPannel.SetActive(false);
                                          TweenScrews();
                                          bgblue.transform.DOMoveY(-0.65f, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                                             {
                                                 bglose.SetActive(true);
                                                 LosePanel.SetActive(true);
                                                 SetLevelTextEnd(parentLevelTextLose);
                                             });
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
            ShowLogFireBase.Instance.ShowCompleteLevel();
            GameObject lastCrew = lstCrew[0];
            btnReset.SetActive(false);
            audioToggle.gameObject.SetActive(false);
            nextLv();
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
                                    // lỗi next 2 level 1 lúc có thể là do 2 hàm dưới cùng gọi sau khi OnComplete. Di rời hàm +1 vào level ra ngoài hoặc sửa lại tween hoặc đặt biến check để không bị lỗi. 
                                    WINtext.transform.DOMoveX(-0.5f, 0.1f).SetEase(Ease.OutQuad);
                                    PLAYtext.transform.DOMoveX(-3.7f, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
                                    {
                                        if (!isNextLvCalled)
                                        {
                                            isNextLvCalled = true;
                                            StartCoroutine(ToggleGameObjectsContinuously(0.05f));
                                            StartCoroutine(DelayedNextLevel(1f));
                                        }
                                    });
                                });
                            });
                        });
                  });
        }
    }

    // public void NextLV()
    // {
    //     // Call the delay method with a delay time of 0.5 seconds and the action to load the next level
    //     DelayMethod(0.8f, () =>
    //     {
    //         nextLv();
    //         SceneManager.LoadScene(0);
    //     });
    // }

    private IEnumerator DelayedNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        // autonextlvwhenwin();
        Winpanel.SetActive(false);
        if (PlayerPrefs.GetInt("lv") == 2 || PlayerPrefs.GetInt("lv") == 3 || PlayerPrefs.GetInt("lv") == 6 || PlayerPrefs.GetInt("lv") == 9
        || PlayerPrefs.GetInt("lv") == 13 || PlayerPrefs.GetInt("lv") == 16 && PlayerPrefs.GetInt("lv") == 19 && PlayerPrefs.GetInt("lv") == 21)
        {
            WinRewardPanel.SetActive(true);
            WinRewardPanel.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(true);
            if (PlayerPrefs.GetInt("lv") == 2 || PlayerPrefs.GetInt("lv") == 16)
            {
                WinRewardPanel.transform.GetChild(3).gameObject.SetActive(true);
                WinRewardPanel.transform.GetChild(4).transform.GetChild(2).gameObject.SetActive(true);
                if (PlayerPrefs.GetInt("lv") == 2)
                {
                    // WinRewardPanel.transform.GetChild(3).transform.GetChild(0).transform.GetComponent<Image>().sprite = lstWinSprite[0];
                    WinRewardPanel.transform.GetChild(3).transform.GetComponent<Image>().sprite = lstWinSprite[0];
                    WinRewardPanel.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
                }
                if (PlayerPrefs.GetInt("lv") == 16)
                {
                    WinRewardPanel.transform.GetChild(3).transform.GetComponent<Image>().sprite = lstWinSprite[1];
                }
            }
            else
            {
                WinRewardPanel.transform.GetChild(2).gameObject.SetActive(true);
                WinRewardPanel.transform.GetChild(4).transform.GetChild(1).gameObject.SetActive(true);
                if (PlayerPrefs.GetInt("lv") == 3)
                {
                    WinRewardPanel.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
                    WinRewardPanel.transform.GetChild(2).transform.GetComponent<Image>().sprite = lstWinSprite[2];
                }
                if (PlayerPrefs.GetInt("lv") == 6)
                {
                    WinRewardPanel.transform.GetChild(2).transform.GetComponent<Image>().sprite = lstWinSprite[3];
                }
                if (PlayerPrefs.GetInt("lv") == 9)
                {
                    WinRewardPanel.transform.GetChild(2).transform.GetComponent<Image>().sprite = lstWinSprite[4];
                }
                if (PlayerPrefs.GetInt("lv") == 13)
                {
                    WinRewardPanel.transform.GetChild(2).transform.GetComponent<Image>().sprite = lstWinSprite[5];
                }
                if (PlayerPrefs.GetInt("lv") == 19)
                {
                    WinRewardPanel.transform.GetChild(2).transform.GetComponent<Image>().sprite = lstWinSprite[6];
                }
                if (PlayerPrefs.GetInt("lv") == 21)
                {
                    WinRewardPanel.transform.GetChild(2).transform.GetComponent<Image>().sprite = lstWinSprite[7];
                }

            }
        }
        else
        {
            Winpanel2.SetActive(true);
            Winpanel2.transform.GetChild(2).DOScale(0.5f, 0.8f).SetEase(Ease.OutQuad)
            .OnComplete(() =>
                      {
                          DOVirtual.DelayedCall(0.2f, () =>
                             {
                                 //   yield return new WaitForSeconds(0.3f);
                                 Winpanel2.transform.GetChild(3).gameObject.SetActive(true);
                                 Winpanel2.transform.GetChild(4).gameObject.SetActive(true);
                             });

                      });
        }

        // StartCoroutine(DelayedbtnNext(2f));
    }

    public void autonextlvwhenwin()
    {
        // foreach (Transform child in gridContainer.transform)
        // {
        //     Destroy(child.gameObject);
        // }

        int scoreImage = PlayerPrefs.GetInt("ScoreImage", 0);
        // PlayerPrefs.SetInt("ScoreImage", scoreImage + 1);
        //1308
        PlayerPrefs.SetInt("ScoreImage", scoreImage + 12);
        DataConfig.ScoreImage = PlayerPrefs.GetInt("ScoreImage", 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene(0);
    }

    public void autonextlvwhenwinAds()
    {
        //adssssssssssssssss
        Loading.GetComponent<TweenLoading>().ShowLoading();
        GameAds.Get.LoadAndShowRewardAd((onComplete) =>
        {
            if (onComplete)
            {
                int scoreImage = PlayerPrefs.GetInt("ScoreImage", 0);
                // PlayerPrefs.SetInt("ScoreImage", scoreImage + 2);
                //1308
                PlayerPrefs.SetInt("ScoreImage", scoreImage + 24);
                DataConfig.ScoreImage = PlayerPrefs.GetInt("ScoreImage", 0);
                PlayerPrefs.Save();

                // foreach (Transform child in gridContainer.transform)
                // {
                //     Destroy(child.gameObject);
                // }
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("Reward win failed");
            }
            GameCtr.instance.Loading.GetComponent<TweenLoading>().HideLoading();
        });

    }

    IEnumerator ToggleGameObjectsContinuously(float toggleInterval)
    {
        while (true)
        {
            foreach (var gameObject in lstBling)
            {
                bool isActive = UnityEngine.Random.Range(0, 2) == 0; // 50% cơ hội bật hoặc tắt
                gameObject.SetActive(isActive);
            }

            yield return new WaitForSeconds(toggleInterval);
        }
    }

    void onGenerateObject()
    {

        // Lấy level hiện tại từ danh sách levels dựa trên giá trị lv lưu trong PlayerPrefs
        // var currentLevel = LVConfig.Instance.levels[0];
        var currentLevel = LVConfig.Instance.levels[PlayerPrefs.GetInt("lv") - 1];

        //dễ nhất lv2
        // var currentLevel = LVConfig.Instance.levels[0];
        // Chọn ngẫu nhiên một sub-level từ danh sách sub-levels của level hiện tại
        // int randomSubLevelIndex = Random.Range(0, currentLevel.subLevelsLists.Count);
        int randomSubLevelIndex = 0;
        var subLevels = currentLevel.subLevelsLists[randomSubLevelIndex];

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
                        instantiatedObject.GetComponent<BulongAction>().col = (int)subLevel.col;
                        instantiatedObject.GetComponent<BulongAction>().row = (int)subLevel.row;

                        //ty le scale
                        instantiatedObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

                        if (DataConfig.EffectIndex != 0)
                        {
                            instantiatedObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        }

                        lstBulong.Add(instantiatedObject);
                    }
                    else if (subLevel.type == 2)
                    {
                        instantiatedObject.GetComponent<Screw>().col = (int)subLevel.col;
                        instantiatedObject.GetComponent<Screw>().row = (int)subLevel.row;
                        lstCrew.Add(instantiatedObject);

                        //can thiet thi scale o day
                        // if (DataConfig.EffectIndex != 0)
                        // {
                        //     instantiatedObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        // }

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
        //1308 thay bang lv 3
        // if (PlayerPrefs.GetInt("lv") == 2 && PlayerPrefs.GetInt("CheckTutorialSkin") == 0 || PlayerPrefs.GetInt("lv") == 3 && PlayerPrefs.GetInt("CheckTutorialImage") == 0)
        // {
        //     DisableAllColliders();
        // }
        // try
        // {
        if (!btnReset.activeSelf)
        {
            GameClear();
        }
        // }
        // catch
        // {
        //     Debug.LogError("error");
        // }
    }

    void UpdateSprite(GameObject obj, SubLevel subLevel, int colorIndex)
    {
        LVConfig.Instance.list();
        // obj.GetComponent<Transform>().localScale = new Vector3(2f, 2f, 2f);
        // Nếu loại của subLevel là 1 (Bulong)
        if (subLevel.type == 1)
        {
            // Tìm và cập nhật sprite của phần thân và mặt của Bulong
            var bulongBody = obj.transform.Find("Bulongbody").GetComponent<SpriteRenderer>();
            var bulongFace = obj.transform.Find("Bulongface").GetComponent<SpriteRenderer>();
            var bulongFace2 = obj.transform.Find("Bulongface2").GetComponent<SpriteRenderer>();

            // bulongBody.sprite = LVConfig.Instance.BulongBodyColor[colorIndex];
            bulongBody.sprite = LVConfig.Instance.BulongBody[DataConfig.EffectIndex][colorIndex];

            //chỉnh lại góc quay do hình bị lệch.
            bulongFace.sprite = LVConfig.Instance.BulongColorFace[DataConfig.EffectIndex][colorIndex];
            bulongFace2.sprite = LVConfig.Instance.BulongColorFace2[DataConfig.EffectIndex][colorIndex];
            if (DataConfig.EffectIndex == 0)
            {
                if (colorIndex == 0 || colorIndex == 2 || colorIndex == 3 || colorIndex == 5)
                {
                    // obj.transform.Find("Bulongbody").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 90f);
                    bulongBody.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                }
                if (colorIndex == 2 || colorIndex == 4 || colorIndex == 5)
                {
                    // obj.transform.Find("Bulongface").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 90f);
                    bulongFace.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                }


                if (colorIndex == 2 || colorIndex == 5)
                {
                    // obj.transform.Find("Bulongface2").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 90f);
                    bulongFace2.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                }
            }
            if (DataConfig.EffectIndex != 0)
            {
                Vector3 newPosition = bulongBody.transform.position;

                // Điều chỉnh giá trị y
                newPosition.y += 0.075f;

                // Gán lại vị trí mới
                bulongBody.transform.position = newPosition;
            }
            //gắn tag
            obj.tag = colorIndex.ToString();
        }
        // Nếu loại của subLevel là 2 (Screw)
        else if (subLevel.type == 2)
        {
            // Tìm và cập nhật sprite của Screw
            var scew = obj.transform.Find("Screw").GetComponent<SpriteRenderer>();
            if (DataConfig.EffectIndex == 0)
            {
                scew.sprite = LVConfig.Instance.ScewColor[colorIndex];
                if (colorIndex == 7)
                {
                    obj.transform.Find("Screw").GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
            else
            {
                obj.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                scew.sprite = LVConfig.Instance.ScewColorNew[colorIndex];
            }

            obj.tag = colorIndex.ToString();
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
