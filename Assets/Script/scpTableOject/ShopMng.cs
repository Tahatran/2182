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
    public GameObject ShopContent2;
    public GameObject ItemPrefab;
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
        // Show ads logic here
        btnAds.SetActive(false);
    }

    public void LoadShop()
    {
        int itemsPerPage = 8; // Number of items per page

        // Clear existing items in both ShopContent and ShopContent2
        ClearShopContent(ShopContent);
        ClearShopContent(ShopContent2);

        // Loop through SkinItemData.Items and distribute items between ShopContent and ShopContent2
        for (int i = 0; i < SkinItemData.Items.Count; i++)
        {
            ItemData item = SkinItemData.Items[i];
            GameObject Item = Instantiate(ItemPrefab);

            if (i < itemsPerPage)
            {
                // Add item to ShopContent
                Item.transform.SetParent(ShopContent.transform, false);
            }
            else
            {
                // Add item to ShopContent2
                Item.transform.SetParent(ShopContent2.transform, false);
            }

            if (item.IsBuy)
            {
                Item.GetComponent<Image>().sprite = item.ItemImg;
                Item.GetComponent<Button>().onClick.AddListener(() =>
                {

                    id = item.Id; // Set the ID for purchasing
                    Debug.Log("aa" + id);
                    Item.transform.GetChild(0).gameObject.SetActive(true); // Activate some UI element
                });
            }
            else
            {
                Debug.Log("bb");
                // Handle non-buyable items
                btnAds.SetActive(true); // Example: activate an ad button
                Item.transform.GetChild(0).gameObject.SetActive(true); // Activate some UI element
            }
        }

        ShopContent.SetActive(true); // Ensure ShopContent is active after loading
    }

    private void ClearShopContent(GameObject content)
    {
        // Clear existing items in a given ShopContent GameObject
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }
}