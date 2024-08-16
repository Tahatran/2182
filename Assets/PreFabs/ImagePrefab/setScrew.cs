using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
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
        if (DataConfig.ScoreImage > 0)
        {
            Image1308.instance.FillandSaveScore();
            gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Image1308.instance.lstSprites[Image1308.instance.idSelect];
            gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255); // 1f is the maximum value for alpha in Unity's Color, equivalent to 255
            idSprite = Image1308.instance.idSelect;
            Checkfill = true;
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            //neu ma fill kin roi thi tat bg nen
            if (Image1308.instance.ImageShowPanel(DataConfig.ImageIndex) == 1)
            {
                Image1308.instance.lstimgbg[DataConfig.ImageIndex].SetActive(false);
                ShowLogFireBase.Instance.LogBuildDone();

            }
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
