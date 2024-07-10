using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class ImageCtr : MonoBehaviour
{
    //
    public GameObject TextLevel;
    public GameObject btnDelete;
    public GameObject btnDelete2;
    public GameObject btnGen1;
    public GameObject btnGen2;
    public bool Delete1;
    public bool Delete2;
    public bool gen;
    private string textLevelstring = "";
    //list cac oc de chon
    public List<Sprite> ScrewColor;
    public List<GameObject> lstScewImage;

    //not cha
    public GameObject objectContainer;
    public GameObject gridContainer;
    //danh sach cac hexa 
    public List<GameObject> lstBulong;
    public List<GridPrefab> lstGrid;
    public List<GameObject> lstCrew;
    //prefab
    public GameObject HexagridPrefab;
    public GameObject BulongPrefab;
    public GameObject ScrewPrefab;

    //so hang, so cot
    public int colNumber;
    public int rowNumber;
    public int indexColor;
    public GameObject objinstance;

    public bool checkbulongorscrew = true;


    //2 cai nay goi sinh o canvas, khong dung nua, haha
    // private float hexWidth = 100f; // chiều rộng của một hexagon
    // private float hexHeight = Mathf.Sqrt(0.8f) / 2 * 10f;
    private float hexWidth = 1.0f; // chiều rộng của một hexagon
    private float hexHeight = Mathf.Sqrt(0.8f) / 2 * 0.7f;
    public static ImageCtr instance;

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
        // //tool thi bat len
        // btnDelete.GetComponent<Button>().onClick.AddListener(Edit);
        // btnDelete2.GetComponent<Button>().onClick.AddListener(Edit2);
        // btnGen1.GetComponent<Button>().onClick.AddListener(GenMap1);
        // btnGen2.GetComponent<Button>().onClick.AddListener(GenMap2);



    }

    public void onGenerateGrid()
    {
        for (int i = 0; i < rowNumber; i++)
        {
            for (int j = 0; j < colNumber; j++)
            {
                // Tính toán vị trí của từng hexagon
                float xPos = j * hexWidth * 0.25f;
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
                tempGrid.GetComponent<SpriteRenderer>().sortingOrder = 8;
                lstGrid.Add(gridRender);
            }
        }
        onGenerateObject();
    }


    public void btnOutput()
    {
        GenLevelfromGrid();
    }

    public void GenLevelfromGrid()
    {
        if (gen)
        {
            textLevelstring = "";
            for (int i = 0; i < GameCtr.instance.lstGrid.Count; i++)
            {
                gridComponent grid = GameCtr.instance.lstGrid[i].GetComponent<gridComponent>();
                if (grid.bulong != null)
                {
                    Debug.Log(GameCtr.instance.lstGrid[i].GetComponent<GridPrefab>().row);
                    Debug.Log(GameCtr.instance.lstGrid[i].GetComponent<GridPrefab>().col);
                    Debug.Log(grid.bulong.tag);

                    string text = "new SubLevel {row= " + GameCtr.instance.lstGrid[i].GetComponent<GridPrefab>().row + ", col=" + GameCtr.instance.lstGrid[i].GetComponent<GridPrefab>().col + ", type= 2, color= " + grid.bulong.tag + "},\n";
                    textLevelstring += text;
                    Debug.Log(textLevelstring);
                    TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
                    // Debug.Log(TextLevel);
                    // Debug.Log(TextLevel.text);
                }
                if (grid.screw != null)
                {
                    Debug.Log(GameCtr.instance.lstGrid[i].GetComponent<GridPrefab>().row);
                    Debug.Log(GameCtr.instance.lstGrid[i].GetComponent<GridPrefab>().col);
                    Debug.Log(grid.screw.tag);

                    string text = "new SubLevel {row= " + GameCtr.instance.lstGrid[i].GetComponent<GridPrefab>().row + ", col=" + GameCtr.instance.lstGrid[i].GetComponent<GridPrefab>().col + ", type= 1, color= " + grid.screw.tag + "},\n";
                    textLevelstring += text;
                    Debug.Log(textLevelstring);
                    TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
                }
            }

        }
        else
        {
            textLevelstring = "";
            for (int i = 0; i < lstGrid.Count; i++)
            {
                gridComponent grid = lstGrid[i].GetComponent<gridComponent>();
                if (grid.bulong != null)
                {
                    // Debug.Log(lstGrid[i].GetComponent<GridPrefab>().row);
                    // Debug.Log(lstGrid[i].GetComponent<GridPrefab>().col);
                    // Debug.Log(grid.bulong.tag);

                    string text = "new SubLevel {row= " + lstGrid[i].GetComponent<GridPrefab>().row + ", col=" + lstGrid[i].GetComponent<GridPrefab>().col + ", type= 2, color= " + grid.bulong.tag + "},\n";
                    textLevelstring += text;
                    // Debug.Log(textLevelstring);
                    TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
                    // Debug.Log(TextLevel);
                    // Debug.Log(TextLevel.text);
                }
                if (grid.screw != null)
                {
                    // Debug.Log(lstGrid[i].GetComponent<GridPrefab>().row);
                    // Debug.Log(lstGrid[i].GetComponent<GridPrefab>().col);
                    // Debug.Log(grid.screw.tag);

                    string text = "new SubLevel {row= " + lstGrid[i].GetComponent<GridPrefab>().row + ", col=" + lstGrid[i].GetComponent<GridPrefab>().col + ", type= 1, color= " + grid.screw.tag + "},\n";
                    textLevelstring += text;
                    // Debug.Log(textLevelstring);
                    TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
                }
            }
            if (DataConfig.ImageIndex == 0)
            {
                PlayerPrefs.SetString("0", textLevelstring);
            }
            else if (DataConfig.ImageIndex == 1)
            {
                PlayerPrefs.SetString("1", textLevelstring);
            }
            else if (DataConfig.ImageIndex == 2)
            {
                PlayerPrefs.SetString("2", textLevelstring);
            }
            else if (DataConfig.ImageIndex == 3)
            {
                PlayerPrefs.SetString("3", textLevelstring);
            }
            PlayerPrefs.Save();
            // Debug để kiểm tra
            // Debug.Log("Saved Level Data: " + textLevelstring);
            //goi luc an get image
            // LoadLevelData(DataConfig.ImageIndex);
        }
    }

    public void LoadLevelData(int a)
    {
        // Tải textLevelstring từ PlayerPrefs
        string loadedTextLevelstring = PlayerPrefs.GetString(a.ToString(), "");
        // Debug.Log("Loaded Level Data: " + loadedTextLevelstring);

        // Chuyển đổi dữ liệu đã tải về thành danh sách SubLevel
        List<SubLevel> newSubLevels = ParseSubLevelsFromString(loadedTextLevelstring);
        // Debug.Log(newSubLevels.Count);
        // foreach (SubLevel subLevel in newSubLevels)
        // {
        //     // Debug.Log("aa");
        //     Debug.Log($"SubLevel: row={subLevel.row}, col={subLevel.col}, type={subLevel.type}, color={subLevel.color}");
        // }


        // Thay thế các SubLevel hiện có trong Imageshigh
        if (a < LVConfig.instance.Imageshigh.Count)
        {
            LVConfig.instance.Imageshigh[a].subLevelsLists.Clear();
            LVConfig.instance.Imageshigh[a].subLevelsLists.Add(newSubLevels); // Thêm danh sách mới vào
            foreach (List<SubLevel> subLevels in LVConfig.instance.Imageshigh[a].subLevelsLists)
            {
                foreach (SubLevel subLevel in subLevels)
                {
                    Debug.Log($"SubLevel: row={subLevel.row}, col={subLevel.col}, type={subLevel.type}, color={subLevel.color}");
                }
            }
        }
        else
        {
            Debug.LogWarning($"Level index {a} is out of range for Imageshigh.");
        }
    }

    // Hàm này chuyển đổi một chuỗi đầu vào chứa dữ liệu SubLevel thành một danh sách các đối tượng SubLevel
    List<SubLevel> ParseSubLevelsFromString(string subLevelsString)
    {
        List<SubLevel> subLevels = new List<SubLevel>();

        // Adjusted pattern to handle spaces around '=' and '{}'
        string pattern = @"new SubLevel\s*{\s*row\s*=\s*(\d+)\s*,\s*col\s*=\s*(\d+)\s*,\s*type\s*=\s*(\d+)\s*,\s*color\s*=\s*(\d+)\s*}";

        MatchCollection matches = Regex.Matches(subLevelsString, pattern);

        // Debug log to check the number of matches found
        // Debug.Log($"Number of matches found: {matches.Count}");

        foreach (Match match in matches)
        {
            SubLevel subLevel = new SubLevel
            {
                row = int.Parse(match.Groups[1].Value),
                col = int.Parse(match.Groups[2].Value),
                type = int.Parse(match.Groups[3].Value),
                color = int.Parse(match.Groups[4].Value)
            };

            subLevels.Add(subLevel);
        }

        return subLevels;
    }

    public void onGenerateObject()
    {
        gen = false;
        var currentLevel = LVConfig.Instance.Imageslow[DataConfig.ImageIndex];
        int randomSubLevelIndex = 0;
        var subLevels = currentLevel.subLevelsLists[randomSubLevelIndex];
        // Duyệt qua từng phần tử trong subLevels
        foreach (var subLevel in subLevels)
        {
            // Tìm grid tương ứng với vị trí của subLevel (dựa trên row và col)
            var targetGrid = lstGrid.Find(grid => grid.row == subLevel.row && grid.col == subLevel.col);

            // Nếu tìm thấy targetGrid
            if (targetGrid)
            {
                SpriteRenderer spriteRenderer = targetGrid.GetComponent<SpriteRenderer>();
                Color color = spriteRenderer.color;

                // Thiết lập giá trị alpha về 1 (không trong suốt)
                color.a = 255f;
                // Gán lại màu cho SpriteRenderer
                spriteRenderer.color = color;
                targetGrid.tag = subLevel.color.ToString();
            }
        }
        onGenerateObjectSave();
    }

    public void onGenerateObjectSave()
    {
        gen = false;
        var currentLevel = LVConfig.Instance.Imageshigh[DataConfig.ImageIndex];
        int randomSubLevelIndex = 0;
        var subLevels = currentLevel.subLevelsLists[randomSubLevelIndex];
        // Duyệt qua từng phần tử trong subLevels
        foreach (var subLevel in subLevels)
        {
            // Tìm grid tương ứng với vị trí của subLevel (dựa trên row và col)
            var targetGrid = lstGrid.Find(grid => grid.row == subLevel.row && grid.col == subLevel.col);

            // Nếu tìm thấy targetGrid
            if (targetGrid)
            {
                targetGrid.GetComponent<SpriteRenderer>().sprite = ScrewColor[subLevel.color];
                targetGrid.GetComponent<SpriteRenderer>().color = Color.white;
                targetGrid.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
                targetGrid.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                targetGrid.tag = subLevel.color.ToString();
                targetGrid.GetComponent<gridComponent>().bulong = targetGrid.gameObject;
                foreach (Transform child in targetGrid.transform)
                {
                    child.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
        }

    }
    public void Edit()
    {
        btnDelete.GetComponent<Image>().color = Color.red;
        btnDelete2.GetComponent<Image>().color = Color.white;
        Delete1 = true;
        Delete2 = false;
    }
    public void Edit2()
    {
        btnDelete2.GetComponent<Image>().color = Color.red;
        btnDelete.GetComponent<Image>().color = Color.white;
        Delete2 = true;
        Delete1 = false;
    }

    public void GenMap1()
    {
        foreach (Transform child in gridContainer.transform)
        {
            Destroy(child.gameObject);
        }
        lstGrid.Clear();
        GameCtr.instance.onGenerateGrid2(HexagridPrefab);
        gen = true;
    }
    public void GenMap2()
    {
        foreach (Transform child in GameCtr.instance.gridContainer.transform)
        {
            Destroy(child.gameObject);
        }
        GameCtr.instance.lstGrid.Clear();
        onGenerateGrid();
        gen = false;
    }


    /////////////////////////////////////////////////
    //     void onGenerateGrid()
    //     {
    //         for (int i = 0; i < rowNumber; i++)
    //         {
    //             for (int j = 0; j < colNumber; j++)
    //             {
    //                 // Tính toán vị trí của từng hexagon

    //                 float xPos = j * hexWidth * 0.7f;
    //                 float yPos = i * hexHeight * 15f;

    //                 // Nếu hàng là hàng lẻ thì dịch chuyển vị trí của hexagon
    //                 if (j % 2 == 1)
    //                 {
    //                     yPos += hexHeight * 10f;
    //                 }

    //                 var tempGrid = Instantiate(HexagridPrefab, gridContainer.GetComponent<RectTransform>());
    //                 tempGrid.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 10);
    //                 var gridRender = tempGrid.GetComponent<GridPrefab>();
    //                 gridRender.col = j;
    //                 gridRender.row = i;
    //                 lstGrid.Add(gridRender);
    //             }
    //         }
    //         // onGenerateObject();

    //         // Sắp xếp danh sách tạm theo vị trí x giảm dần
    //         lstGrid.Sort((a, b) =>
    //         {
    //             int compareX = b.GetComponent<RectTransform>().localPosition.x.CompareTo(a.GetComponent<RectTransform>().localPosition.x);
    //             if (compareX == 0) // Nếu x giống nhau, sắp xếp theo y giảm dần
    //             {
    //                 return b.GetComponent<RectTransform>().localPosition.y.CompareTo(a.GetComponent<RectTransform>().localPosition.y);
    //             }
    //             return compareX;
    //         });

    //         // Thiết lập sibling index cho từng đối tượng theo thứ tự đã sắp xếp
    //         for (int i = 0; i < lstGrid.Count; i++)
    //         {
    //             lstGrid[i].transform.SetSiblingIndex(i);
    //         }



    //         // lstGrid.Sort((a, b) => b.GetComponent<RectTransform>().localPosition.y.CompareTo(a.GetComponent<RectTransform>().localPosition.y));
    //         // for (int i = 0; i < lstGrid.Count; i++)
    //         // {
    //         //     lstGrid[i].transform.SetSiblingIndex(i);
    //         // }




    //         //
    //         lstGrid.Sort((a, b) =>
    // {
    //    int compareY = b.GetComponent<RectTransform>().localPosition.y.CompareTo(a.GetComponent<RectTransform>().localPosition.y);
    //    if (compareY == 0) // Nếu y giống nhau, sắp xếp theo x giảm dần
    //    {
    //        return b.GetComponent<RectTransform>().localPosition.x.CompareTo(a.GetComponent<RectTransform>().localPosition.x);
    //    }
    //    return compareY;
    // });

    //         // Thiết lập sibling index cho từng đối tượng theo thứ tự đã sắp xếp
    //         for (int i = 0; i < lstGrid.Count; i++)
    //         {
    //             lstGrid[i].transform.SetSiblingIndex(i);
    //         }
    //     }

    // Update is called once per frame
    void Update()
    {

    }
}