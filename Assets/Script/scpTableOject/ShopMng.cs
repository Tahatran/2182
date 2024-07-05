using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMng : MonoBehaviour
{
    [Space(10)]
    [Header("Data")]
    public ShopItemSO SkinItemData;

    public GameObject ShopContent;
    public GameObject ItemPrefab;
    public GameObject btnGet;
    public GameObject btnAds;
    private int id;


    // Start is called before the first frame update
    void Start()
    {
        LoadShop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Get()
    {
        DataConfig.EffectIndex = id;
        Debug.Log(DataConfig.EffectIndex);
    }

    public void Ads()
    {
        //show ads o day
        btnAds.SetActive(false);
    }

    public void LoadShop()
    {
        foreach (ItemData item in SkinItemData.Items)
        {
            GameObject Item = Instantiate(ItemPrefab, ShopContent.transform);
            if (item.IsBuy)
            {
                Item.GetComponent<Image>().sprite = item.ItemImg;
                Item.GetComponent<Button>().onClick.AddListener(() =>
                {
                    //thực hiện truyền tham số/ gán vật liệu ở đây
                    // DataConfig.EffectIndex = item.Id;
                    id = item.Id;
                    Item.transform.GetChild(0).gameObject.SetActive(true);
                });
            }
            else
            {
                //set img = ? img
                // HideSelectEffect(shopEffectContent.transform);
                // getBtn.interactable = true;
                btnAds.SetActive(true);
                Item.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        ShopContent.SetActive(true);
    }

    private void HideSelectEffect(Transform content)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            content.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
    }
}
