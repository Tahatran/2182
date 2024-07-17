using System.Collections;
using System.Collections.Generic;
using Nami.Controller;
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
    public GameObject btnGet;
    public List<GameObject> lstSkin;
    public List<int> lstSave;
    public int id;
    public int idSelecSkinLock;

    // Start is called before the first frame update
    void Start()
    {
        int Skin = PlayerPrefs.GetInt("Skin", 0);
        PlayerPrefs.SetInt("Skin", Skin);
        DataConfig.EffectIndex = PlayerPrefs.GetInt("Skin", 0);
        PlayerPrefs.Save();
        LoadShop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Get()
    {
        DataConfig.EffectIndex = id;
        PlayerPrefs.SetInt("Skin", DataConfig.EffectIndex);
    }

    public void Ads()
    {
        //adsssssssssssssss
        GameCtr.instance.Loading.GetComponent<TweenLoading>().ShowLoading();
        GameAds.Get.LoadAndShowRewardAd((onComplete) =>
        {
            if (onComplete)
            {
                UnlockSkin(idSelecSkinLock);
                SkinItemData.Items[idSelecSkinLock].IsBuy = true;
                PlayerPrefs.SetInt("SkinUnlocked_" + idSelecSkinLock, 1); // Save the unlock status
                LoadShop();
                DeactivateAllItems();
                lstSkin[idSelecSkinLock].transform.GetChild(2).gameObject.SetActive(true);
                DataConfig.EffectIndex = idSelecSkinLock;
                btnAds.SetActive(false);
                GameCtr.instance.Loading.GetComponent<TweenLoading>().HideLoading();
            }
            else
            {
                Debug.Log("Reward skin failed");
            }
        });

    }

    private void UnlockSkin(int skinId)
    {
        lstSkin[skinId].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void LoadShop()
    {
        int itemsPerPage = 8; // Number of items per page

        // Clear existing items in both ShopContent and ShopContent2
        ClearShopContent(ShopContent);
        ClearShopContent(ShopContent2);
        lstSkin.Clear();
        // Loop through SkinItemData.Items and distribute items between ShopContent and ShopContent2
        for (int i = 0; i < SkinItemData.Items.Count; i++)
        {
            ItemData item = SkinItemData.Items[i];
            GameObject Item = Instantiate(ItemPrefab);
            lstSkin.Add(Item);
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

            // Check if the item has been purchased or unlocked
            bool isUnlocked = item.IsBuy || PlayerPrefs.GetInt("SkinUnlocked_" + i, 0) == 1;

            if (isUnlocked)
            {
                item.IsBuy = true; // Ensure item state is updated
                Item.GetComponent<Image>().sprite = item.ItemImg;
                Item.GetComponent<Button>().onClick.AddListener(() =>
                {
                    DeactivateAllItems();
                    id = item.Id; // Set the ID for purchasing
                    btnAds.SetActive(false);
                    Item.transform.GetChild(2).gameObject.SetActive(true); // Activate some UI element
                });
            }
            else
            {
                Item.GetComponent<Image>().sprite = item.ItemImg;
                Item.transform.GetChild(0).gameObject.SetActive(true);
                Item.GetComponent<Button>().onClick.AddListener(() =>
                {
                    DeactivateAllItems();
                    idSelecSkinLock = item.Id;

                    // Handle non-buyable items
                    if (idSelecSkinLock > 0 && !SkinItemData.Items[idSelecSkinLock - 1].IsBuy)
                    {
                        btnAds.SetActive(false);
                        btnGet.SetActive(false);
                    }
                    else
                    {
                        btnAds.SetActive(true);
                    }
                });
            }
        }
        lstSkin[DataConfig.EffectIndex].transform.GetChild(2).gameObject.SetActive(true);
        ShopContent.SetActive(true); // Ensure ShopContent is active after loading
    }

    private void DeactivateAllItems()
    {
        foreach (GameObject skin in lstSkin)
        {
            skin.transform.GetChild(2).gameObject.SetActive(false);
        }
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
