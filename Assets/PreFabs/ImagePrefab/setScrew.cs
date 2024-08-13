using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setScrew : MonoBehaviour
{
    public int idSprite;
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
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Image1308.instance.lstSprites[Image1308.instance.idSelect];
        idSprite = Image1308.instance.idSelect;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
