using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using GoogleMobileAds.Api.Mediation.LiftoffMonetize;
using System.Globalization;
using System.Data.Common;


public class ImageCtr : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject hexa;
    public Texture2D image;
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
    public GameObject gridContainer2;
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
        // if (mainCamera == null)
        // {
        //     mainCamera = Camera.main; // Nếu chưa gán, sử dụng camera chính
        // }

    }




    public void onGenerateGrid()
    {
        lstCrew.Clear();
        Debug.Log("2");
        gen = false;
        var currentLevel = LVConfig.Instance.Imageslow[DataConfig.ImageIndex];
        int randomSubLevelIndex = 0;
        var subLevels = currentLevel.subLevelsLists[randomSubLevelIndex];
        foreach (var subLevel in subLevels)
        {
            Vector3 spawnPosition = new Vector3(subLevel.row, subLevel.col, 0f);
            GameObject hexagon = Instantiate(hexa, spawnPosition, Quaternion.identity, gridContainer.transform); // Tạo prefab tại vị trí chạm và thiết lập parent
            hexagon.transform.localPosition = spawnPosition;
            hexagon.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, -90f);
            hexagon.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            hexagon.SetActive(true);
            lstCrew.Add(hexagon);
            // Debug.Log("spawn" + spawnPosition);
            // Debug.Log("pos" + hexagon.transform.position);
            // Debug.Log("local" + hexagon.transform.localPosition);
        }
        onGenerateObjectSave();
    }
    public void onGenerateObjectSave()
    {
        // int a =
        for (int i = 0; i < lstCrew.Count; i++)
        {
            GameObject hexagon = lstCrew[i];
            // if (i < )
        }
        Debug.Log("5");
        gen = false;
        var currentLevel = LVConfig.Instance.Imageshigh[DataConfig.ImageIndex];
        int randomSubLevelIndex = 0;
        var subLevels = currentLevel.subLevelsLists[randomSubLevelIndex];
        Debug.Log(subLevels.Count);
        // Duyệt qua từng phần tử trong subLevels
        foreach (var subLevel in subLevels)
        {
            Debug.Log("6");
            // Tìm grid tương ứng với vị trí của subLevel (dựa trên row và col)
            var targetGrid = lstCrew.Find(grid =>
            Mathf.Approximately(grid.GetComponent<Transform>().localPosition.x, subLevel.row) &&
            Mathf.Approximately(grid.GetComponent<Transform>().localPosition.y, subLevel.col));
            // Nếu tìm thấy targetGrid
            if (targetGrid)
            {
                Debug.Log("find");
                targetGrid.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                targetGrid.GetComponent<Screw2>().isBulong = true;
            }
            else
            {
                Debug.Log("Target grid not found for this SubLevel.");
            }
        }
    }


    public void btnOutput()
    {
        // GenLevelfromGrid();
    }

    public void Fill()
    {
        Debug.Log("3");
        if (DataConfig.ScoreImage > 0)
        {
            for (int i = 0; i < lstCrew.Count; i++) // Duyệt qua tất cả các phần tử trong lstCrew
            {
                GameObject a = lstCrew[i];
                if (a.GetComponent<Screw2>().isBulong == false)
                {
                    a.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    a.GetComponent<Screw2>().isBulong = true;
                    DataConfig.ScoreImage--; // Giảm giá trị của DataConfig.ScoreImage mỗi khi một phần tử được xử lý
                    if (DataConfig.ScoreImage == 0)
                    {
                        break; // Ngắt vòng lặp nếu DataConfig.ScoreImage đạt giá trị 10
                    }
                }
            }
        }
        GenRed();
    }
    public void GenRed()
    {
        Debug.Log("4");
        int dem = 0;
        for (int i = 0; i < lstCrew.Count; i++)
        {
            dem++;
            GameObject a = lstCrew[i];
            if (a.GetComponent<Screw2>().isBulong == true)
            {
                // string rowStr = a.GetComponent<Transform>().localPosition.x.ToString(CultureInfo.InvariantCulture);
                // string colStr = a.GetComponent<Transform>().localPosition.y.ToString(CultureInfo.InvariantCulture);
                // string text = $"new SubLevel {{row= {rowStr}f, col= {colStr}f, type= 2, color= {a.tag}}},\n";
                // textLevelstring += text;
                textLevelstring += dem;
            }
        }
        PlayerPrefs.SetString(DataConfig.ImageIndex.ToString(), textLevelstring);
        PlayerPrefs.Save();
    }

    public void LoadLevelData(int a)
    {
        Debug.Log("7");
        // Tải textLevelstring từ PlayerPrefs
        string loadedTextLevelstring = PlayerPrefs.GetString(a.ToString(), "");

        // Chuyển đổi dữ liệu đã tải về thành danh sách SubLevel
        List<SubLevel> newSubLevels = ParseSubLevelsFromString(loadedTextLevelstring);

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
        // Biểu thức chính quy để khớp với dữ liệu SubLevel
        string pattern = @"new SubLevel\s*{\s*row\s*=\s*(-?\d+(\.\d+)?)\s*,\s*col\s*=\s*(-?\d+(\.\d+)?)\s*,\s*type\s*=\s*(\d+)\s*,\s*color\s*=\s*(\d+)\s*}";
        MatchCollection matches = Regex.Matches(subLevelsString, pattern);
        foreach (Match match in matches)
        {
            SubLevel subLevel = new SubLevel
            {
                row = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture),
                col = float.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture),
                type = int.Parse(match.Groups[5].Value),
                color = int.Parse(match.Groups[6].Value)
            };
            Debug.Log($"Parsed SubLevel: row={subLevel.row}, col={subLevel.col}, type={subLevel.type}, color={subLevel.color}");
            subLevels.Add(subLevel);
        }
        return subLevels;
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
                    //tools
                    // Debug.Log(textLevelstring);
                    // TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
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
                    //tools
                    // Debug.Log(textLevelstring);
                    // TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
                }
            }

        }
        //tools luu colum row
        // else
        // {
        //     textLevelstring = "";
        //     for (int i = 0; i < lstGrid.Count; i++)
        //     {
        //         gridComponent grid = lstGrid[i].GetComponent<gridComponent>();
        //         if (grid.bulong != null)
        //         {
        //             // Debug.Log(lstGrid[i].GetComponent<GridPrefab>().row);
        //             // Debug.Log(lstGrid[i].GetComponent<GridPrefab>().col);
        //             // Debug.Log(grid.bulong.tag);

        //             string text = "new SubLevel {row= " + lstGrid[i].GetComponent<GridPrefab>().row + ", col=" + lstGrid[i].GetComponent<GridPrefab>().col + ", type= 2, color= " + grid.bulong.tag + "},\n";
        //             textLevelstring += text;
        //             // Debug.Log(textLevelstring);
        //             TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
        //             // Debug.Log(TextLevel);
        //             // Debug.Log(TextLevel.text);
        //         }
        //         if (grid.screw != null)
        //         {
        //             // Debug.Log(lstGrid[i].GetComponent<GridPrefab>().row);
        //             // Debug.Log(lstGrid[i].GetComponent<GridPrefab>().col);
        //             // Debug.Log(grid.screw.tag);

        //             string text = "new SubLevel {row= " + lstGrid[i].GetComponent<GridPrefab>().row + ", col=" + lstGrid[i].GetComponent<GridPrefab>().col + ", type= 1, color= " + grid.screw.tag + "},\n";
        //             textLevelstring += text;
        //             // Debug.Log(textLevelstring);
        //             TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
        //         }
        //     }
        //     if (DataConfig.ImageIndex == 0)
        //     {
        //         PlayerPrefs.SetString("0", textLevelstring);
        //     }
        //     else if (DataConfig.ImageIndex == 1)
        //     {
        //         PlayerPrefs.SetString("1", textLevelstring);
        //     }
        //     else if (DataConfig.ImageIndex == 2)
        //     {
        //         PlayerPrefs.SetString("2", textLevelstring);
        //     }
        //     else if (DataConfig.ImageIndex == 3)
        //     {
        //         PlayerPrefs.SetString("3", textLevelstring);
        //     }
        //     PlayerPrefs.Save();
        //     // Debug để kiểm tra
        //     // Debug.Log("Saved Level Data: " + textLevelstring);
        //     //goi luc an get image
        //     // LoadLevelData(DataConfig.ImageIndex);
        // }
        else
        {
            RemoveMissingOrNullElements();
            textLevelstring = "";
            for (int i = 0; i < lstCrew.Count; i++)
            {
                string rowStr = lstCrew[i].GetComponent<Transform>().localPosition.x.ToString(CultureInfo.InvariantCulture);
                string colStr = lstCrew[i].GetComponent<Transform>().localPosition.y.ToString(CultureInfo.InvariantCulture);
                // gridComponent grid = lstGrid[i].GetComponent<gridComponent>();

                // Debug.Log(lstGrid[i].GetComponent<GridPrefab>().row);
                // Debug.Log(lstGrid[i].GetComponent<GridPrefab>().col);
                // Debug.Log(grid.bulong.tag);

                string text = "new SubLevel {row= " + rowStr + "f, col=" + colStr + "f, type= 2, color= " + lstCrew[i].tag + "},\n";
                textLevelstring += text;
                // Debug.Log(textLevelstring);
                TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
                // Debug.Log(TextLevel);
                // Debug.Log(TextLevel.text);

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
    public void RemoveMissingOrNullElements()
    {
        for (int i = lstCrew.Count - 1; i >= 0; i--) // Duyệt ngược từ cuối danh sách về đầu
        {
            if (lstCrew[i] == null) // Kiểm tra nếu phần tử bị null
            {
                lstCrew.RemoveAt(i); // Xóa phần tử khỏi danh sách
            }
        }
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
    public void Clear()
    {
        lstGrid.Clear();
        lstCrew.Clear();
        GameCtr.instance.lstGrid.Clear();
        GameCtr.instance.lstBulong.Clear();
        GameCtr.instance.lstCrew.Clear();
        foreach (Transform child in gridContainer2.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in gridContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in GameCtr.instance.gridContainer.transform)
        {
            Destroy(child.gameObject);
        }
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
        //tool
        // if (Input.GetMouseButtonDown(0)) // Kiểm tra nếu người dùng click chuột trái
        // {
        //     Debug.Log("â");
        //     SpawnHexagon();
        // }
    }

    void SpawnHexagon()
    {
        Vector3 mousePosition = Input.mousePosition; // Lấy vị trí chuột
        mousePosition.z = 10f; // Đặt khoảng cách Z từ camera, tùy chỉnh theo cảnh của bạn

        Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(mousePosition); // Chuyển đổi vị trí chuột sang tọa độ thế giới

        // Giới hạn spawnPosition.y trong khoảng từ -0.1 đến 6.2 và spawnPosition.x trong khoảng từ -0.2 đến 3.72
        if (spawnPosition.y >= -3.6f && spawnPosition.y <= 2.9f)
        {
            GameObject hexagon = Instantiate(hexa, spawnPosition, Quaternion.identity, gridContainer2.transform); // Tạo prefab tại vị trí chạm và thiết lập parent
            hexagon.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, -90f);
            hexagon.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            lstCrew.Add(hexagon);
        }
        else
        {
            Debug.Log("Spawn position out of bounds: " + spawnPosition);
        }
    }

}
