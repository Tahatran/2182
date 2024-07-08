using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class ImageCtr : MonoBehaviour
{
    //list cac oc de chon
    public List<GameObject> lstScewImage;

    //not cha
    public GameObject objectContainer;
    public GameObject gridContainer;
    //danh sach cac hexa 
    public List<GameObject> lstBulong;
    public List<GridPrefab> lstGrid;
    //prefab
    public GameObject HexagridPrefab;

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
        onGenerateGrid();
    }

    void onGenerateGrid()
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
                lstGrid.Add(gridRender);
            }
        }
        // onGenerateObject();
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
