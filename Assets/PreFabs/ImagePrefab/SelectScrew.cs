using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScrew : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnMouseDown()
    {
        SelectBulong();
    }

    void SelectBulong()
    {
        // for (int i = 0; i < Image1308.instance.lstUp.Count; i++)
        // {
        //     if (gameObject.name == Image1308.instance.lstUp[i].name)
        //     {
        //         Image1308.instance.id = i;
        //         Debug.Log("id" + Image1308.instance.id);
        //     }
        // }
        if (DataConfig.ScoreImage > 0)
        {
            Image1308.instance.ResetSelect();
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            Image1308.instance.idSelect = int.Parse(gameObject.tag);
            Debug.Log("id" + Image1308.instance.idSelect);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
