using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class SubLevel
{
    public int col { get; set; }
    public int row { get; set; }
    public int type { get; set; } // 1 cho Bulong, 2 cho Scew
    public int color { get; set; }

}

[System.Serializable]
public class Level
{
    public List<List<SubLevel>> subLevelsLists { get; set; }
    public int lv;
}
public class LVConfig : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    public List<Sprite> BulongBodyColor;
    public List<Sprite> BulongFaceColor;
    public List<Sprite> BulongFaceColor2;
    public List<Sprite> ScewColor;
    public static LVConfig instance;
    public static LVConfig Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LVConfig>();

                if (instance == null)
                {
                    var singletonObject = new GameObject($"Singleton - {nameof(LVConfig)}");
                    instance = singletonObject.AddComponent<LVConfig>();
                }
            }

            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        levels = new List<Level>
        {
            new Level
            {
                lv = 1,
                subLevelsLists = new List<List<SubLevel>>
                {
                    new List<SubLevel>
                    {
                        new SubLevel {row= 0, col= 1, type= 2, color= 5},
                        new SubLevel {row= 0, col= 1, type= 1, color= 4},
                        new SubLevel {row= 0, col= 3, type= 2, color= 4},
                        new SubLevel {row= 0, col= 3, type= 1, color= 5},
                        new SubLevel {row= 0, col= 4, type= 2, color= 6},
                        new SubLevel {row= 0, col= 4, type= 1, color= 3},
                        new SubLevel {row= 1, col= 3, type= 2, color= 5},
                        new SubLevel {row= 1, col= 3, type= 1, color= 3},
                        new SubLevel {row= 2, col= 3, type= 2, color= 7},
                        new SubLevel {row= 2, col= 3, type= 1, color= 2},
                        new SubLevel {row= 2, col= 4, type= 2, color= 4},
                        new SubLevel {row= 2, col= 4, type= 1, color= 7},
                        new SubLevel {row= 3, col= 2, type= 2, color= 3},
                        new SubLevel {row= 3, col= 2, type= 1, color= 6},
                        new SubLevel {row= 3, col= 3, type= 2, color= 7},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 3, col= 4, type= 2, color= 6},
                        new SubLevel {row= 3, col= 4, type= 1, color= 2},
                        new SubLevel {row= 4, col= 3, type= 2, color= 7},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 2},
                        new SubLevel {row= 4, col= 4, type= 1, color= 7},
                        new SubLevel {row= 4, col= 5, type= 2, color= 2},
                        new SubLevel {row= 4, col= 5, type= 1, color= 6},
                        new SubLevel {row= 5, col= 2, type= 2, color= 3},
                        new SubLevel {row= 5, col= 2, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 0},
                        new SubLevel {row= 5, col= 3, type= 1, color= 5},
                        new SubLevel {row= 5, col= 4, type= 2, color= 4},
                        new SubLevel {row= 5, col= 4, type= 1, color= 2},
                        new SubLevel {row= 6, col= 2, type= 2, color= 4},
                        new SubLevel {row= 6, col= 2, type= 1, color= 1},
                        new SubLevel {row= 6, col= 3, type= 2, color= 3},
                        new SubLevel {row= 6, col= 3, type= 1, color= 0},
                        new SubLevel {row= 6, col= 4, type= 2, color= 1},
                        new SubLevel {row= 6, col= 4, type= 1, color= 4},
                        new SubLevel {row= 6, col= 5, type= 2, color= 6},
                        new SubLevel {row= 7, col= 2, type= 2, color= 2},
                        new SubLevel {row= 7, col= 2, type= 1, color= 4},
                        new SubLevel {row= 7, col= 3, type= 2, color= 4},
                        new SubLevel {row= 7, col= 3, type= 1, color= 5},
                        new SubLevel {row= 7, col= 4, type= 2, color= 4},
                        new SubLevel {row= 7, col= 4, type= 1, color= 1},
                        new SubLevel {row= 8, col= 4, type= 2, color= 5},
                        new SubLevel {row= 8, col= 4, type= 1, color= 2},
                        new SubLevel {row= 9, col= 2, type= 2, color= 7},
                        new SubLevel {row= 9, col= 2, type= 1, color= 5},
                        new SubLevel {row= 9, col= 3, type= 2, color= 0},
                        new SubLevel {row= 9, col= 3, type= 1, color= 3},
                        new SubLevel {row= 9, col= 4, type= 2, color= 5},
                        new SubLevel {row= 9, col= 4, type= 1, color= 0},
                        new SubLevel {row= 0, col= 2, type= 2, color= 5},
                        new SubLevel {row= 0, col= 2, type= 1, color= 7},

                    },
                    new List<SubLevel>
                    {

                        new SubLevel {row= 0, col= 1, type= 2, color= 5},
                        new SubLevel {row= 0, col= 1, type= 1, color= 4},
                        new SubLevel {row= 0, col= 3, type= 2, color= 4},
                        new SubLevel {row= 0, col= 3, type= 1, color= 5},
                        new SubLevel {row= 0, col= 4, type= 2, color= 6},
                        new SubLevel {row= 0, col= 4, type= 1, color= 3},
                        new SubLevel {row= 1, col= 3, type= 2, color= 5},
                        new SubLevel {row= 1, col= 3, type= 1, color= 3},
                        new SubLevel {row= 2, col= 3, type= 2, color= 7},
                        new SubLevel {row= 2, col= 3, type= 1, color= 2},
                        new SubLevel {row= 2, col= 4, type= 2, color= 4},
                        new SubLevel {row= 2, col= 4, type= 1, color= 7},
                        new SubLevel {row= 3, col= 2, type= 2, color= 3},
                        new SubLevel {row= 3, col= 2, type= 1, color= 6},
                        new SubLevel {row= 3, col= 3, type= 2, color= 7},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 3, col= 4, type= 2, color= 6},
                        new SubLevel {row= 3, col= 4, type= 1, color= 2},
                        new SubLevel {row= 4, col= 3, type= 2, color= 7},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 2},
                        new SubLevel {row= 4, col= 4, type= 1, color= 7},
                        new SubLevel {row= 4, col= 5, type= 2, color= 2},
                        new SubLevel {row= 4, col= 5, type= 1, color= 6},
                        new SubLevel {row= 5, col= 2, type= 2, color= 3},
                        new SubLevel {row= 5, col= 2, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 0},
                        new SubLevel {row= 5, col= 3, type= 1, color= 5},
                        new SubLevel {row= 5, col= 4, type= 2, color= 4},
                        new SubLevel {row= 5, col= 4, type= 1, color= 2},
                        new SubLevel {row= 6, col= 2, type= 2, color= 4},
                        new SubLevel {row= 6, col= 2, type= 1, color= 1},
                        new SubLevel {row= 6, col= 3, type= 2, color= 3},
                        new SubLevel {row= 6, col= 3, type= 1, color= 0},
                        new SubLevel {row= 6, col= 4, type= 2, color= 1},
                        new SubLevel {row= 6, col= 4, type= 1, color= 4},
                        new SubLevel {row= 6, col= 5, type= 2, color= 6},
                        new SubLevel {row= 7, col= 2, type= 2, color= 2},
                        new SubLevel {row= 7, col= 2, type= 1, color= 4},
                        new SubLevel {row= 7, col= 3, type= 2, color= 4},
                        new SubLevel {row= 7, col= 3, type= 1, color= 5},
                        new SubLevel {row= 7, col= 4, type= 2, color= 4},
                        new SubLevel {row= 7, col= 4, type= 1, color= 1},
                        new SubLevel {row= 8, col= 4, type= 2, color= 5},
                        new SubLevel {row= 8, col= 4, type= 1, color= 2},
                        new SubLevel {row= 9, col= 2, type= 2, color= 7},
                        new SubLevel {row= 9, col= 2, type= 1, color= 5},
                        new SubLevel {row= 9, col= 3, type= 2, color= 0},
                        new SubLevel {row= 9, col= 3, type= 1, color= 3},
                        new SubLevel {row= 9, col= 4, type= 2, color= 5},
                        new SubLevel {row= 9, col= 4, type= 1, color= 0},
                        new SubLevel {row= 0, col= 2, type= 2, color= 5},
                        new SubLevel {row= 0, col= 2, type= 1, color= 7},
                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 0, col= 1, type= 2, color= 5},
                        new SubLevel {row= 0, col= 1, type= 1, color= 4},
                        new SubLevel {row= 0, col= 3, type= 2, color= 4},
                        new SubLevel {row= 0, col= 3, type= 1, color= 5},
                        new SubLevel {row= 0, col= 4, type= 2, color= 6},
                        new SubLevel {row= 0, col= 4, type= 1, color= 3},
                        new SubLevel {row= 1, col= 3, type= 2, color= 5},
                        new SubLevel {row= 1, col= 3, type= 1, color= 3},
                        new SubLevel {row= 2, col= 3, type= 2, color= 7},
                        new SubLevel {row= 2, col= 3, type= 1, color= 2},
                        new SubLevel {row= 2, col= 4, type= 2, color= 4},
                        new SubLevel {row= 2, col= 4, type= 1, color= 7},
                        new SubLevel {row= 3, col= 2, type= 2, color= 3},
                        new SubLevel {row= 3, col= 2, type= 1, color= 6},
                        new SubLevel {row= 3, col= 3, type= 2, color= 7},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 3, col= 4, type= 2, color= 6},
                        new SubLevel {row= 3, col= 4, type= 1, color= 2},
                        new SubLevel {row= 4, col= 3, type= 2, color= 7},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 2},
                        new SubLevel {row= 4, col= 4, type= 1, color= 7},
                        new SubLevel {row= 4, col= 5, type= 2, color= 2},
                        new SubLevel {row= 4, col= 5, type= 1, color= 6},
                        new SubLevel {row= 5, col= 2, type= 2, color= 3},
                        new SubLevel {row= 5, col= 2, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 0},
                        new SubLevel {row= 5, col= 3, type= 1, color= 5},
                        new SubLevel {row= 5, col= 4, type= 2, color= 4},
                        new SubLevel {row= 5, col= 4, type= 1, color= 2},
                        new SubLevel {row= 6, col= 2, type= 2, color= 4},
                        new SubLevel {row= 6, col= 2, type= 1, color= 1},
                        new SubLevel {row= 6, col= 3, type= 2, color= 3},
                        new SubLevel {row= 6, col= 3, type= 1, color= 0},
                        new SubLevel {row= 6, col= 4, type= 2, color= 1},
                        new SubLevel {row= 6, col= 4, type= 1, color= 4},
                        new SubLevel {row= 6, col= 5, type= 2, color= 6},
                        new SubLevel {row= 7, col= 2, type= 2, color= 2},
                        new SubLevel {row= 7, col= 2, type= 1, color= 4},
                        new SubLevel {row= 7, col= 3, type= 2, color= 4},
                        new SubLevel {row= 7, col= 3, type= 1, color= 5},
                        new SubLevel {row= 7, col= 4, type= 2, color= 4},
                        new SubLevel {row= 7, col= 4, type= 1, color= 1},
                        new SubLevel {row= 8, col= 4, type= 2, color= 5},
                        new SubLevel {row= 8, col= 4, type= 1, color= 2},
                        new SubLevel {row= 9, col= 2, type= 2, color= 7},
                        new SubLevel {row= 9, col= 2, type= 1, color= 5},
                        new SubLevel {row= 9, col= 3, type= 2, color= 0},
                        new SubLevel {row= 9, col= 3, type= 1, color= 3},
                        new SubLevel {row= 9, col= 4, type= 2, color= 5},
                        new SubLevel {row= 9, col= 4, type= 1, color= 0},
                        new SubLevel {row= 0, col= 2, type= 2, color= 5},
                        new SubLevel {row= 0, col= 2, type= 1, color= 7},

                    },
                },

            },
            new Level
            {
                lv = 2,
                subLevelsLists = new List<List<SubLevel>>
                {
                     new List<SubLevel>
                    {
                        new SubLevel {row= 6, col= 3, type= 2, color= 1},
                        new SubLevel {row= 6, col= 3, type= 1, color= 4},
                        new SubLevel {row= 6, col= 4, type= 2, color= 1},
                        new SubLevel {row= 6, col= 4, type= 1, color= 4},
                        new SubLevel {row= 7, col= 3, type= 2, color= 4},
                        new SubLevel {row= 7, col= 3, type= 1, color= 1},
                        new SubLevel {row= 6, col= 2, type= 2, color= 4},

                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 6, col= 3, type= 2, color= 1},
                        new SubLevel {row= 6, col= 3, type= 1, color= 4},
                        new SubLevel {row= 6, col= 4, type= 2, color= 1},
                        new SubLevel {row= 6, col= 4, type= 1, color= 4},
                        new SubLevel {row= 7, col= 3, type= 2, color= 4},
                        new SubLevel {row= 7, col= 3, type= 1, color= 1},
                        new SubLevel {row= 6, col= 2, type= 2, color= 4},

                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 6, col= 3, type= 2, color= 1},
                        new SubLevel {row= 6, col= 3, type= 1, color= 4},
                        new SubLevel {row= 6, col= 4, type= 2, color= 1},
                        new SubLevel {row= 6, col= 4, type= 1, color= 4},
                        new SubLevel {row= 7, col= 3, type= 2, color= 4},
                        new SubLevel {row= 7, col= 3, type= 1, color= 1},
                        new SubLevel {row= 6, col= 2, type= 2, color= 4},

                    },
                },

            },
            new Level
            {
                lv = 3,
                subLevelsLists = new List<List<SubLevel>>
                {
                     new List<SubLevel>
                    {
                        new SubLevel {row= 3, col= 3, type= 2, color= 6},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 5, col= 3, type= 2, color= 7},
                        new SubLevel {row= 5, col= 3, type= 1, color= 6},
                        new SubLevel {row= 6, col= 3, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 1, color= 6},
                        new SubLevel {row= 7, col= 3, type= 2, color= 6},
                        new SubLevel {row= 7, col= 3, type= 1, color= 7},

                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 3, col= 3, type= 2, color= 6},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 5, col= 3, type= 2, color= 7},
                        new SubLevel {row= 5, col= 3, type= 1, color= 6},
                        new SubLevel {row= 6, col= 3, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 1, color= 6},
                        new SubLevel {row= 7, col= 3, type= 2, color= 6},
                        new SubLevel {row= 7, col= 3, type= 1, color= 7},

                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 3, col= 3, type= 2, color= 6},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 5, col= 3, type= 2, color= 7},
                        new SubLevel {row= 5, col= 3, type= 1, color= 6},
                        new SubLevel {row= 6, col= 3, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 1, color= 6},
                        new SubLevel {row= 7, col= 3, type= 2, color= 6},
                        new SubLevel {row= 7, col= 3, type= 1, color= 7},
                    },
                },

            },
            new Level
            {
                lv = 4,
                subLevelsLists = new List<List<SubLevel>>
                {
                     new List<SubLevel>
                    {
                        new SubLevel {row= 3, col= 3, type= 2, color= 6},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 4, col= 3, type= 2, color= 4},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 6},
                        new SubLevel {row= 4, col= 4, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 6},
                        new SubLevel {row= 6, col= 3, type= 2, color= 4},
                        new SubLevel {row= 6, col= 3, type= 1, color= 6},
                        new SubLevel {row= 6, col= 4, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 1, color= 5},
                        new SubLevel {row= 7, col= 3, type= 2, color= 5},
                        new SubLevel {row= 7, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 3, type= 2, color= 5},
                        new SubLevel {row= 8, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 4, type= 2, color= 5},
                        new SubLevel {row= 8, col= 4, type= 1, color= 6},

                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 3, col= 3, type= 2, color= 6},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 4, col= 3, type= 2, color= 4},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 6},
                        new SubLevel {row= 4, col= 4, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 6},
                        new SubLevel {row= 6, col= 3, type= 2, color= 4},
                        new SubLevel {row= 6, col= 3, type= 1, color= 6},
                        new SubLevel {row= 6, col= 4, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 1, color= 5},
                        new SubLevel {row= 7, col= 3, type= 2, color= 5},
                        new SubLevel {row= 7, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 3, type= 2, color= 5},
                        new SubLevel {row= 8, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 4, type= 2, color= 5},
                        new SubLevel {row= 8, col= 4, type= 1, color= 6},

                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 3, col= 3, type= 2, color= 6},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 4, col= 3, type= 2, color= 4},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 6},
                        new SubLevel {row= 4, col= 4, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 6},
                        new SubLevel {row= 6, col= 3, type= 2, color= 4},
                        new SubLevel {row= 6, col= 3, type= 1, color= 6},
                        new SubLevel {row= 6, col= 4, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 1, color= 5},
                        new SubLevel {row= 7, col= 3, type= 2, color= 5},
                        new SubLevel {row= 7, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 3, type= 2, color= 5},
                        new SubLevel {row= 8, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 4, type= 2, color= 5},
                        new SubLevel {row= 8, col= 4, type= 1, color= 6},
                    },
                },

            },
            new Level
            {
                lv = 5,
                subLevelsLists = new List<List<SubLevel>>
                {
                     new List<SubLevel>
                    {
                        new SubLevel {row= 2, col= 3, type= 2, color= 1},
                        new SubLevel {row= 2, col= 3, type= 1, color= 3},
                        new SubLevel {row= 3, col= 2, type= 2, color= 5},
                        new SubLevel {row= 3, col= 2, type= 1, color= 3},
                        new SubLevel {row= 3, col= 3, type= 2, color= 3},
                        new SubLevel {row= 3, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 3, type= 2, color= 1},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 5},
                        new SubLevel {row= 4, col= 4, type= 1, color= 1},
                        new SubLevel {row= 5, col= 2, type= 2, color= 1},
                        new SubLevel {row= 5, col= 2, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 3},
                        new SubLevel {row= 5, col= 4, type= 2, color= 3},
                        new SubLevel {row= 5, col= 4, type= 1, color= 1},
                        new SubLevel {row= 6, col= 3, type= 2, color= 3},
                        new SubLevel {row= 6, col= 3, type= 1, color= 1},
                        new SubLevel {row= 6, col= 4, type= 2, color= 5},
                        new SubLevel {row= 6, col= 4, type= 1, color= 3},
                        new SubLevel {row= 7, col= 3, type= 2, color= 1},
                        new SubLevel {row= 7, col= 3, type= 1, color= 3},
                        new SubLevel {row= 7, col= 4, type= 2, color= 5},
                        new SubLevel {row= 7, col= 4, type= 1, color= 1},
                        new SubLevel {row= 8, col= 4, type= 2, color= 3},
                        new SubLevel {row= 8, col= 4, type= 1, color= 5},

                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 2, col= 3, type= 2, color= 1},
                        new SubLevel {row= 2, col= 3, type= 1, color= 3},
                        new SubLevel {row= 3, col= 2, type= 2, color= 5},
                        new SubLevel {row= 3, col= 2, type= 1, color= 3},
                        new SubLevel {row= 3, col= 3, type= 2, color= 3},
                        new SubLevel {row= 3, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 3, type= 2, color= 1},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 5},
                        new SubLevel {row= 4, col= 4, type= 1, color= 1},
                        new SubLevel {row= 5, col= 2, type= 2, color= 1},
                        new SubLevel {row= 5, col= 2, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 3},
                        new SubLevel {row= 5, col= 4, type= 2, color= 3},
                        new SubLevel {row= 5, col= 4, type= 1, color= 1},
                        new SubLevel {row= 6, col= 3, type= 2, color= 3},
                        new SubLevel {row= 6, col= 3, type= 1, color= 1},
                        new SubLevel {row= 6, col= 4, type= 2, color= 5},
                        new SubLevel {row= 6, col= 4, type= 1, color= 3},
                        new SubLevel {row= 7, col= 3, type= 2, color= 1},
                        new SubLevel {row= 7, col= 3, type= 1, color= 3},
                        new SubLevel {row= 7, col= 4, type= 2, color= 5},
                        new SubLevel {row= 7, col= 4, type= 1, color= 1},
                        new SubLevel {row= 8, col= 4, type= 2, color= 3},
                        new SubLevel {row= 8, col= 4, type= 1, color= 5},
                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 2, col= 3, type= 2, color= 1},
                        new SubLevel {row= 2, col= 3, type= 1, color= 3},
                        new SubLevel {row= 3, col= 2, type= 2, color= 5},
                        new SubLevel {row= 3, col= 2, type= 1, color= 3},
                        new SubLevel {row= 3, col= 3, type= 2, color= 3},
                        new SubLevel {row= 3, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 3, type= 2, color= 1},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 5},
                        new SubLevel {row= 4, col= 4, type= 1, color= 1},
                        new SubLevel {row= 5, col= 2, type= 2, color= 1},
                        new SubLevel {row= 5, col= 2, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 3},
                        new SubLevel {row= 5, col= 4, type= 2, color= 3},
                        new SubLevel {row= 5, col= 4, type= 1, color= 1},
                        new SubLevel {row= 6, col= 3, type= 2, color= 3},
                        new SubLevel {row= 6, col= 3, type= 1, color= 1},
                        new SubLevel {row= 6, col= 4, type= 2, color= 5},
                        new SubLevel {row= 6, col= 4, type= 1, color= 3},
                        new SubLevel {row= 7, col= 3, type= 2, color= 1},
                        new SubLevel {row= 7, col= 3, type= 1, color= 3},
                        new SubLevel {row= 7, col= 4, type= 2, color= 5},
                        new SubLevel {row= 7, col= 4, type= 1, color= 1},
                        new SubLevel {row= 8, col= 4, type= 2, color= 3},
                        new SubLevel {row= 8, col= 4, type= 1, color= 5},
                    },
                },

            },
            new Level
            {
                lv = 6,
                subLevelsLists = new List<List<SubLevel>>
                {
                     new List<SubLevel>
                    {
                        new SubLevel {row= 1, col= 3, type= 2, color= 5},
                        new SubLevel {row= 1, col= 3, type= 1, color= 4},
                        new SubLevel {row= 2, col= 3, type= 2, color= 4},
                        new SubLevel {row= 2, col= 3, type= 1, color= 5},
                        new SubLevel {row= 2, col= 4, type= 2, color= 6},
                        new SubLevel {row= 2, col= 4, type= 1, color= 3},
                        new SubLevel {row= 3, col= 3, type= 2, color= 5},
                        new SubLevel {row= 3, col= 3, type= 1, color= 3},
                        new SubLevel {row= 4, col= 3, type= 2, color= 7},
                        new SubLevel {row= 4, col= 3, type= 1, color= 2},
                        new SubLevel {row= 4, col= 4, type= 2, color= 4},
                        new SubLevel {row= 4, col= 4, type= 1, color= 7},
                        new SubLevel {row= 5, col= 2, type= 2, color= 3},
                        new SubLevel {row= 5, col= 2, type= 1, color= 6},
                        new SubLevel {row= 5, col= 3, type= 2, color= 7},
                        new SubLevel {row= 5, col= 3, type= 1, color= 4},
                        new SubLevel {row= 5, col= 4, type= 2, color= 6},
                        new SubLevel {row= 5, col= 4, type= 1, color= 2},
                        new SubLevel {row= 6, col= 3, type= 2, color= 7},
                        new SubLevel {row= 6, col= 3, type= 1, color= 5},
                        new SubLevel {row= 6, col= 4, type= 2, color= 2},
                        new SubLevel {row= 6, col= 4, type= 1, color= 7},
                        new SubLevel {row= 6, col= 5, type= 2, color= 2},
                        new SubLevel {row= 6, col= 5, type= 1, color= 6},
                        new SubLevel {row= 7, col= 2, type= 2, color= 3},
                        new SubLevel {row= 7, col= 2, type= 1, color= 5},
                        new SubLevel {row= 7, col= 3, type= 2, color= 0},
                        new SubLevel {row= 7, col= 3, type= 1, color= 5},
                        new SubLevel {row= 7, col= 4, type= 2, color= 4},
                        new SubLevel {row= 7, col= 4, type= 1, color= 2},
                        new SubLevel {row= 8, col= 2, type= 2, color= 4},
                        new SubLevel {row= 8, col= 2, type= 1, color= 1},
                        new SubLevel {row= 8, col= 3, type= 2, color= 3},
                        new SubLevel {row= 8, col= 3, type= 1, color= 0},
                        new SubLevel {row= 8, col= 4, type= 2, color= 1},
                        new SubLevel {row= 8, col= 4, type= 1, color= 4},
                        new SubLevel {row= 8, col= 5, type= 2, color= 6},
                        new SubLevel {row= 9, col= 2, type= 2, color= 2},
                        new SubLevel {row= 9, col= 2, type= 1, color= 4},
                        new SubLevel {row= 9, col= 3, type= 2, color= 4},
                        new SubLevel {row= 9, col= 3, type= 1, color= 5},
                        new SubLevel {row= 9, col= 4, type= 2, color= 4},
                        new SubLevel {row= 9, col= 4, type= 1, color= 1},
                        new SubLevel {row= 10, col= 4, type= 2, color= 5},
                        new SubLevel {row= 10, col= 4, type= 1, color= 4},
                        new SubLevel {row= 11, col= 2, type= 2, color= 7},
                        new SubLevel {row= 11, col= 2, type= 1, color= 5},
                        new SubLevel {row= 11, col= 3, type= 2, color= 0},
                        new SubLevel {row= 11, col= 3, type= 1, color= 3},
                        new SubLevel {row= 11, col= 4, type= 2, color= 5},
                        new SubLevel {row= 11, col= 4, type= 1, color= 0},
                        new SubLevel {row= 13, col= 3, type= 2, color= 5},
                        new SubLevel {row= 13, col= 3, type= 1, color= 7},


                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 1, col= 3, type= 2, color= 5},
                        new SubLevel {row= 1, col= 3, type= 1, color= 4},
                        new SubLevel {row= 2, col= 3, type= 2, color= 4},
                        new SubLevel {row= 2, col= 3, type= 1, color= 5},
                        new SubLevel {row= 2, col= 4, type= 2, color= 6},
                        new SubLevel {row= 2, col= 4, type= 1, color= 3},
                        new SubLevel {row= 3, col= 3, type= 2, color= 5},
                        new SubLevel {row= 3, col= 3, type= 1, color= 3},
                        new SubLevel {row= 4, col= 3, type= 2, color= 7},
                        new SubLevel {row= 4, col= 3, type= 1, color= 2},
                        new SubLevel {row= 4, col= 4, type= 2, color= 4},
                        new SubLevel {row= 4, col= 4, type= 1, color= 7},
                        new SubLevel {row= 5, col= 2, type= 2, color= 3},
                        new SubLevel {row= 5, col= 2, type= 1, color= 6},
                        new SubLevel {row= 5, col= 3, type= 2, color= 7},
                        new SubLevel {row= 5, col= 3, type= 1, color= 4},
                        new SubLevel {row= 5, col= 4, type= 2, color= 6},
                        new SubLevel {row= 5, col= 4, type= 1, color= 2},
                        new SubLevel {row= 6, col= 3, type= 2, color= 7},
                        new SubLevel {row= 6, col= 3, type= 1, color= 5},
                        new SubLevel {row= 6, col= 4, type= 2, color= 2},
                        new SubLevel {row= 6, col= 4, type= 1, color= 7},
                        new SubLevel {row= 6, col= 5, type= 2, color= 2},
                        new SubLevel {row= 6, col= 5, type= 1, color= 6},
                        new SubLevel {row= 7, col= 2, type= 2, color= 3},
                        new SubLevel {row= 7, col= 2, type= 1, color= 5},
                        new SubLevel {row= 7, col= 3, type= 2, color= 0},
                        new SubLevel {row= 7, col= 3, type= 1, color= 5},
                        new SubLevel {row= 7, col= 4, type= 2, color= 4},
                        new SubLevel {row= 7, col= 4, type= 1, color= 2},
                        new SubLevel {row= 8, col= 2, type= 2, color= 4},
                        new SubLevel {row= 8, col= 2, type= 1, color= 1},
                        new SubLevel {row= 8, col= 3, type= 2, color= 3},
                        new SubLevel {row= 8, col= 3, type= 1, color= 0},
                        new SubLevel {row= 8, col= 4, type= 2, color= 1},
                        new SubLevel {row= 8, col= 4, type= 1, color= 4},
                        new SubLevel {row= 8, col= 5, type= 2, color= 6},
                        new SubLevel {row= 9, col= 2, type= 2, color= 2},
                        new SubLevel {row= 9, col= 2, type= 1, color= 4},
                        new SubLevel {row= 9, col= 3, type= 2, color= 4},
                        new SubLevel {row= 9, col= 3, type= 1, color= 5},
                        new SubLevel {row= 9, col= 4, type= 2, color= 4},
                        new SubLevel {row= 9, col= 4, type= 1, color= 1},
                        new SubLevel {row= 10, col= 4, type= 2, color= 5},
                        new SubLevel {row= 10, col= 4, type= 1, color= 4},
                        new SubLevel {row= 11, col= 2, type= 2, color= 7},
                        new SubLevel {row= 11, col= 2, type= 1, color= 5},
                        new SubLevel {row= 11, col= 3, type= 2, color= 0},
                        new SubLevel {row= 11, col= 3, type= 1, color= 3},
                        new SubLevel {row= 11, col= 4, type= 2, color= 5},
                        new SubLevel {row= 11, col= 4, type= 1, color= 0},
                        new SubLevel {row= 13, col= 3, type= 2, color= 5},
                        new SubLevel {row= 13, col= 3, type= 1, color= 7},

                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 1, col= 3, type= 2, color= 5},
                        new SubLevel {row= 1, col= 3, type= 1, color= 4},
                        new SubLevel {row= 2, col= 3, type= 2, color= 4},
                        new SubLevel {row= 2, col= 3, type= 1, color= 5},
                        new SubLevel {row= 2, col= 4, type= 2, color= 6},
                        new SubLevel {row= 2, col= 4, type= 1, color= 3},
                        new SubLevel {row= 3, col= 3, type= 2, color= 5},
                        new SubLevel {row= 3, col= 3, type= 1, color= 3},
                        new SubLevel {row= 4, col= 3, type= 2, color= 7},
                        new SubLevel {row= 4, col= 3, type= 1, color= 2},
                        new SubLevel {row= 4, col= 4, type= 2, color= 4},
                        new SubLevel {row= 4, col= 4, type= 1, color= 7},
                        new SubLevel {row= 5, col= 2, type= 2, color= 3},
                        new SubLevel {row= 5, col= 2, type= 1, color= 6},
                        new SubLevel {row= 5, col= 3, type= 2, color= 7},
                        new SubLevel {row= 5, col= 3, type= 1, color= 4},
                        new SubLevel {row= 5, col= 4, type= 2, color= 6},
                        new SubLevel {row= 5, col= 4, type= 1, color= 2},
                        new SubLevel {row= 6, col= 3, type= 2, color= 7},
                        new SubLevel {row= 6, col= 3, type= 1, color= 5},
                        new SubLevel {row= 6, col= 4, type= 2, color= 2},
                        new SubLevel {row= 6, col= 4, type= 1, color= 7},
                        new SubLevel {row= 6, col= 5, type= 2, color= 2},
                        new SubLevel {row= 6, col= 5, type= 1, color= 6},
                        new SubLevel {row= 7, col= 2, type= 2, color= 3},
                        new SubLevel {row= 7, col= 2, type= 1, color= 5},
                        new SubLevel {row= 7, col= 3, type= 2, color= 0},
                        new SubLevel {row= 7, col= 3, type= 1, color= 5},
                        new SubLevel {row= 7, col= 4, type= 2, color= 4},
                        new SubLevel {row= 7, col= 4, type= 1, color= 2},
                        new SubLevel {row= 8, col= 2, type= 2, color= 4},
                        new SubLevel {row= 8, col= 2, type= 1, color= 1},
                        new SubLevel {row= 8, col= 3, type= 2, color= 3},
                        new SubLevel {row= 8, col= 3, type= 1, color= 0},
                        new SubLevel {row= 8, col= 4, type= 2, color= 1},
                        new SubLevel {row= 8, col= 4, type= 1, color= 4},
                        new SubLevel {row= 8, col= 5, type= 2, color= 6},
                        new SubLevel {row= 9, col= 2, type= 2, color= 2},
                        new SubLevel {row= 9, col= 2, type= 1, color= 4},
                        new SubLevel {row= 9, col= 3, type= 2, color= 4},
                        new SubLevel {row= 9, col= 3, type= 1, color= 5},
                        new SubLevel {row= 9, col= 4, type= 2, color= 4},
                        new SubLevel {row= 9, col= 4, type= 1, color= 1},
                        new SubLevel {row= 10, col= 4, type= 2, color= 5},
                        new SubLevel {row= 10, col= 4, type= 1, color= 4},
                        new SubLevel {row= 11, col= 2, type= 2, color= 7},
                        new SubLevel {row= 11, col= 2, type= 1, color= 5},
                        new SubLevel {row= 11, col= 3, type= 2, color= 0},
                        new SubLevel {row= 11, col= 3, type= 1, color= 3},
                        new SubLevel {row= 11, col= 4, type= 2, color= 5},
                        new SubLevel {row= 11, col= 4, type= 1, color= 0},
                        new SubLevel {row= 13, col= 3, type= 2, color= 5},
                        new SubLevel {row= 13, col= 3, type= 1, color= 7},
                    },
                },

            },
            new Level
            {
                lv = 7,
                subLevelsLists = new List<List<SubLevel>>
                {
                     new List<SubLevel>
                    {
                        new SubLevel {row= 3, col= 3, type= 2, color= 6},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 4, col= 3, type= 2, color= 4},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 6},
                        new SubLevel {row= 4, col= 4, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 6},
                        new SubLevel {row= 6, col= 3, type= 2, color= 4},
                        new SubLevel {row= 6, col= 3, type= 1, color= 6},
                        new SubLevel {row= 6, col= 4, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 1, color= 5},
                        new SubLevel {row= 7, col= 3, type= 2, color= 5},
                        new SubLevel {row= 7, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 3, type= 2, color= 5},
                        new SubLevel {row= 8, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 4, type= 2, color= 5},
                        new SubLevel {row= 8, col= 4, type= 1, color= 6},

                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 3, col= 3, type= 2, color= 6},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 4, col= 3, type= 2, color= 4},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 6},
                        new SubLevel {row= 4, col= 4, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 6},
                        new SubLevel {row= 6, col= 3, type= 2, color= 4},
                        new SubLevel {row= 6, col= 3, type= 1, color= 6},
                        new SubLevel {row= 6, col= 4, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 1, color= 5},
                        new SubLevel {row= 7, col= 3, type= 2, color= 5},
                        new SubLevel {row= 7, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 3, type= 2, color= 5},
                        new SubLevel {row= 8, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 4, type= 2, color= 5},
                        new SubLevel {row= 8, col= 4, type= 1, color= 6},

                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 3, col= 3, type= 2, color= 6},
                        new SubLevel {row= 3, col= 3, type= 1, color= 4},
                        new SubLevel {row= 4, col= 3, type= 2, color= 4},
                        new SubLevel {row= 4, col= 3, type= 1, color= 5},
                        new SubLevel {row= 4, col= 4, type= 2, color= 6},
                        new SubLevel {row= 4, col= 4, type= 1, color= 5},
                        new SubLevel {row= 5, col= 3, type= 2, color= 6},
                        new SubLevel {row= 6, col= 3, type= 2, color= 4},
                        new SubLevel {row= 6, col= 3, type= 1, color= 6},
                        new SubLevel {row= 6, col= 4, type= 2, color= 4},
                        new SubLevel {row= 6, col= 4, type= 1, color= 5},
                        new SubLevel {row= 7, col= 3, type= 2, color= 5},
                        new SubLevel {row= 7, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 3, type= 2, color= 5},
                        new SubLevel {row= 8, col= 3, type= 1, color= 4},
                        new SubLevel {row= 8, col= 4, type= 2, color= 5},
                        new SubLevel {row= 8, col= 4, type= 1, color= 6},
                    },
                },

            },
        };
    }

}
