using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class setScrew : MonoBehaviour
{
    public int idSprite;
    public bool Checkfill;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnMouseDown()
    {
        SetBulong();
    }
    void SetBulong()
    {
        //dieu kien de fill roi thi khong tru diem
        // if (Checkfill == false)
        // {
        //     Image1308.instance.FillandSaveScore();
        // }
        Image1308.instance.FillandSaveScore();
        Color spriteColor = gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
        spriteColor.a = 1f; // 1f is the maximum value for alpha in Unity's Color, equivalent to 255
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Image1308.instance.lstSprites[Image1308.instance.idSelect];
        idSprite = Image1308.instance.idSelect;
        Checkfill = true;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
