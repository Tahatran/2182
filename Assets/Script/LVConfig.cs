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

                  new SubLevel {row= 6, col= 0, type= 2, color= 3}, // 7-1=6, 1-1=0, 4-1=3
new SubLevel {row= 6, col= 0, type= 1, color= 2}, // 7-1=6, 1-1=0, 3-1=2

new SubLevel {row= 7, col= 0, type= 2, color= 5}, // 8-1=7, 1-1=0, 6-1=5
new SubLevel {row= 7, col= 0, type= 1, color= 2}, // 8-1=7, 1-1=0, 3-1=2

new SubLevel {row= 7, col= 1, type= 2, color= 5}, // 8-1=7, 2-1=1, 6-1=5


new SubLevel {row= 8, col= 0, type= 2, color= 3}, // 9-1=8, 1-1=0, 4-1=3
new SubLevel {row= 8, col= 0, type= 1, color= 5}, // 9-1=8, 1-1=0, 6-1=5

new SubLevel {row= 9, col= 0, type= 2, color= 5}, // 10-1=9, 1-1=0, 6-1=5
new SubLevel {row= 9, col= 0, type= 1, color= 3}, // 10-1=9, 1-1=0, 4-1=3

new SubLevel {row= 9, col= 1, type= 2, color= 2}, // 10-1=9, 2-1=1, 3-1=2
new SubLevel {row= 9, col= 1, type= 1, color= 5}, // 10-1=9, 2-1=1, 6-1=5

new SubLevel {row= 10, col= 0, type= 2, color= 3}, // 11-1=10, 1-1=0, 4-1=3
new SubLevel {row= 10, col= 0, type= 1, color= 2}, // 11-1=10, 1-1=0, 3-1=2

new SubLevel {row= 11, col= 0, type= 2, color= 2}, // 12-1=11, 1-1=0, 3-1=2
new SubLevel {row= 11, col= 0, type= 1, color= 3}, // 12-1=11, 1-1=0, 4-1=3

new SubLevel {row= 11, col= 1, type= 2, color= 2}, // 12-1=11, 2-1=1, 3-1=2
new SubLevel {row= 11, col= 1, type= 1, color= 3}, // 12-1=11, 2-1=1, 4-1=3


                    },
                    new List<SubLevel>
                    {

                                   new SubLevel {row= 6, col= 0, type= 2, color= 3}, // 7-1=6, 1-1=0, 4-1=3
new SubLevel {row= 6, col= 0, type= 1, color= 2}, // 7-1=6, 1-1=0, 3-1=2

new SubLevel {row= 7, col= 0, type= 2, color= 5}, // 8-1=7, 1-1=0, 6-1=5
new SubLevel {row= 7, col= 0, type= 1, color= 2}, // 8-1=7, 1-1=0, 3-1=2

new SubLevel {row= 7, col= 1, type= 2, color= 5}, // 8-1=7, 2-1=1, 6-1=5


new SubLevel {row= 8, col= 0, type= 2, color= 3}, // 9-1=8, 1-1=0, 4-1=3
new SubLevel {row= 8, col= 0, type= 1, color= 5}, // 9-1=8, 1-1=0, 6-1=5

new SubLevel {row= 9, col= 0, type= 2, color= 5}, // 10-1=9, 1-1=0, 6-1=5
new SubLevel {row= 9, col= 0, type= 1, color= 3}, // 10-1=9, 1-1=0, 4-1=3

new SubLevel {row= 9, col= 1, type= 2, color= 2}, // 10-1=9, 2-1=1, 3-1=2
new SubLevel {row= 9, col= 1, type= 1, color= 5}, // 10-1=9, 2-1=1, 6-1=5

new SubLevel {row= 10, col= 0, type= 2, color= 3}, // 11-1=10, 1-1=0, 4-1=3
new SubLevel {row= 10, col= 0, type= 1, color= 2}, // 11-1=10, 1-1=0, 3-1=2

new SubLevel {row= 11, col= 0, type= 2, color= 2}, // 12-1=11, 1-1=0, 3-1=2
new SubLevel {row= 11, col= 0, type= 1, color= 3}, // 12-1=11, 1-1=0, 4-1=3

new SubLevel {row= 11, col= 1, type= 2, color= 2}, // 12-1=11, 2-1=1, 3-1=2
new SubLevel {row= 11, col= 1, type= 1, color= 3}, // 12-1=11, 2-1=1, 4-1=3
                    },
                    new List<SubLevel>
                    {

                                        new SubLevel {row= 6, col= 0, type= 2, color= 3}, // 7-1=6, 1-1=0, 4-1=3
new SubLevel {row= 6, col= 0, type= 1, color= 2}, // 7-1=6, 1-1=0, 3-1=2

new SubLevel {row= 7, col= 0, type= 2, color= 5}, // 8-1=7, 1-1=0, 6-1=5
new SubLevel {row= 7, col= 0, type= 1, color= 2}, // 8-1=7, 1-1=0, 3-1=2

new SubLevel {row= 7, col= 1, type= 2, color= 5}, // 8-1=7, 2-1=1, 6-1=5


new SubLevel {row= 8, col= 0, type= 2, color= 3}, // 9-1=8, 1-1=0, 4-1=3
new SubLevel {row= 8, col= 0, type= 1, color= 5}, // 9-1=8, 1-1=0, 6-1=5

new SubLevel {row= 9, col= 0, type= 2, color= 5}, // 10-1=9, 1-1=0, 6-1=5
new SubLevel {row= 9, col= 0, type= 1, color= 3}, // 10-1=9, 1-1=0, 4-1=3

new SubLevel {row= 9, col= 1, type= 2, color= 2}, // 10-1=9, 2-1=1, 3-1=2
new SubLevel {row= 9, col= 1, type= 1, color= 5}, // 10-1=9, 2-1=1, 6-1=5

new SubLevel {row= 10, col= 0, type= 2, color= 3}, // 11-1=10, 1-1=0, 4-1=3
new SubLevel {row= 10, col= 0, type= 1, color= 2}, // 11-1=10, 1-1=0, 3-1=2

new SubLevel {row= 11, col= 0, type= 2, color= 2}, // 12-1=11, 1-1=0, 3-1=2
new SubLevel {row= 11, col= 0, type= 1, color= 3}, // 12-1=11, 1-1=0, 4-1=3

new SubLevel {row= 11, col= 1, type= 2, color= 2}, // 12-1=11, 2-1=1, 3-1=2
new SubLevel {row= 11, col= 1, type= 1, color= 3}, // 12-1=11, 2-1=1, 4-1=3
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
                        new SubLevel {row= 9,col= 4,type= 1,  color= 6},
                        new SubLevel {row= 9,col= 5,type= 1,  color= 6},
                        new SubLevel {row= 8,col= 2,type= 1,  color= 5},
                        new SubLevel {row= 7,col= 3,type= 1, color= 5},
                        new SubLevel {row= 7,col= 4,type= 1,  color= 5},
                        new SubLevel {row= 6,col= 2,type= 1,  color= 6},
                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 10,col= 2,type= 1, color= 5},
                        new SubLevel {row= 10,col= 4,type= 1, color= 5},
                        new SubLevel {row= 9,col= 3,type= 1, color= 6},
                        new SubLevel {row= 9,col= 5,type= 1,  color= 5},
                        new SubLevel {row= 8,col= 4,type= 1,  color= 6},
                        new SubLevel {row= 7,col= 5,type= 1, color= 6},
                    },
                    new List<SubLevel>
                    {
                        new SubLevel {row= 10,col= 2,type= 1,  color= 5},
                        new SubLevel {row= 10,col= 4,type= 1,  color= 5},
                        new SubLevel {row= 9,col= 3,type= 1,  color= 6},
                        new SubLevel {row= 9,col= 4,type= 1,  color= 6},
                        new SubLevel {row= 7,col= 3,type= 1,  color= 6},
                        new SubLevel {row= 7,col= 5,type= 1, color= 5},
                    },
                },

            },
        };
    }

}
