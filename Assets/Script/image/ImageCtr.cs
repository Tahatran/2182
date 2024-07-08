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
    private float hexWidth = 100f; // chiều rộng của một hexagon
    private float hexHeight = Mathf.Sqrt(0.8f) / 2 * 10f;
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

                float xPos = j * hexWidth * 0.7f;
                float yPos = i * hexHeight * 15f;

                // Nếu hàng là hàng lẻ thì dịch chuyển vị trí của hexagon
                if (j % 2 == 1)
                {
                    yPos += hexHeight * 10f;
                }

                var tempGrid = Instantiate(HexagridPrefab, gridContainer.GetComponent<RectTransform>());
                tempGrid.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 10);
                var gridRender = tempGrid.GetComponent<GridPrefab>();
                gridRender.col = j;
                gridRender.row = i;
                lstGrid.Add(gridRender);
            }
        }
        // onGenerateObject();
        lstGrid.Sort((a, b) => b.GetComponent<RectTransform>().localPosition.y.CompareTo(a.GetComponent<RectTransform>().localPosition.y));
        for (int i = 0; i < lstGrid.Count; i++)
        {
            lstGrid[i].transform.SetSiblingIndex(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
