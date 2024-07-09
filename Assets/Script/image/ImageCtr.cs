using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
                    Debug.Log(lstGrid[i].GetComponent<GridPrefab>().row);
                    Debug.Log(lstGrid[i].GetComponent<GridPrefab>().col);
                    Debug.Log(grid.bulong.tag);

                    string text = "new SubLevel {row= " + lstGrid[i].GetComponent<GridPrefab>().row + ", col=" + lstGrid[i].GetComponent<GridPrefab>().col + ", type= 2, color= " + grid.bulong.tag + "},\n";
                    textLevelstring += text;
                    Debug.Log(textLevelstring);
                    TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
                    // Debug.Log(TextLevel);
                    // Debug.Log(TextLevel.text);
                }
                if (grid.screw != null)
                {
                    Debug.Log(lstGrid[i].GetComponent<GridPrefab>().row);
                    Debug.Log(lstGrid[i].GetComponent<GridPrefab>().col);
                    Debug.Log(grid.screw.tag);

                    string text = "new SubLevel {row= " + lstGrid[i].GetComponent<GridPrefab>().row + ", col=" + lstGrid[i].GetComponent<GridPrefab>().col + ", type= 1, color= " + grid.screw.tag + "},\n";
                    textLevelstring += text;
                    Debug.Log(textLevelstring);
                    TextLevel.GetComponent<TextMeshProUGUI>().text = textLevelstring;
                }
            }
        }
    }

    public void onGenerateObject()
    {

        // Lấy level hiện tại từ danh sách levels dựa trên giá trị lv lưu trong PlayerPrefs
        // var currentLevel = LVConfig.Instance.levels[0];
        var currentLevel = LVConfig.Instance.Imageslow[DataConfig.ImageIndex];

        //dễ nhất lv2
        // var currentLevel = LVConfig.Instance.levels[0];
        // Chọn ngẫu nhiên một sub-level từ danh sách sub-levels của level hiện tại
        // int randomSubLevelIndex = Random.Range(0, currentLevel.subLevelsLists.Count);
        int randomSubLevelIndex = 0;
        var subLevels = currentLevel.subLevelsLists[randomSubLevelIndex];

        // Cập nhật text của Map với chỉ số sub-level được chọn
        // Map.text = "Map: " + randomSubLevelIndex.ToString();
        // Debug.Log("Map: " + randomSubLevelIndex);

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
            }
        }

        // ReadTags();
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
