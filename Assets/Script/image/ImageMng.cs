using System.Collections;
using System.Collections.Generic;
using Nami.Controller;
using UnityEngine;
using UnityEngine.UI;

public class ImageMng : MonoBehaviour
{
    [Space(10)]
    [Header("Data")]
    public ShopItemSO ImageItemData;
    public GameObject Home;
    public GameObject ShopContent;
    public GameObject ShopContent2;
    public GameObject ItemPrefab;
    public GameObject btnAds;
    public GameObject btnGet;
    public List<GameObject> lstImage;
    public int id;
    public int idSelecSkinLock;

    // Start is called before the first frame update
    void Start()
    {
        LoadShop();
    }

    public void Get()
    {
        DataConfig.ImageIndex = id;
        // Debug.Log("1");
        // ImageCtr.instance.LoadLevelData(DataConfig.ImageIndex);
        // ImageCtr.instance.onGenerateGrid();
        Image2.instance.ShowScore();
        Image2.instance.LoadImage(DataConfig.ImageIndex);
        Image2.instance.Img[id].SetActive(true);
        HomeMng.instance.btnBack.SetActive(true);
        Home.SetActive(false);
        GameCtr.instance.GameClear();
    }

    public void Ads()
    {
        //adsssssssssss
        GameCtr.instance.Loading.GetComponent<TweenLoading>().ShowLoading();
        GameAds.Get.LoadAndShowRewardAd((onComplete) =>
        {
            if (onComplete)
            {
                UnlockImage(idSelecSkinLock);
                ImageItemData.Items[idSelecSkinLock].IsBuy = true;
                PlayerPrefs.SetInt("ImageUnlocked_" + idSelecSkinLock, 1); // Save the unlock status
                LoadShop();
                DeactivateAllItems();
                id = idSelecSkinLock;
                lstImage[idSelecSkinLock].transform.GetChild(2).gameObject.SetActive(true);
                btnAds.SetActive(false);
                btnGet.SetActive(true);
            }
            else
            {
                Debug.Log("Reward image failed");
            }
            GameCtr.instance.Loading.GetComponent<TweenLoading>().HideLoading();
        });

    }

    private void UnlockImage(int imageId)
    {
        lstImage[imageId].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void LoadShop()
    {
        int itemsPerPage = 8; // Number of items per page

        // Clear existing items in both ShopContent and ShopContent2
        ClearShopContent(ShopContent);
        ClearShopContent(ShopContent2);
        lstImage.Clear();
        // Loop through ImageItemData.Items and distribute items between ShopContent and ShopContent2
        for (int i = 0; i < ImageItemData.Items.Count; i++)
        {
            ItemData item = ImageItemData.Items[i];
            GameObject Item = Instantiate(ItemPrefab);
            lstImage.Add(Item);
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
            bool isUnlocked = item.IsBuy || PlayerPrefs.GetInt("ImageUnlocked_" + i, 0) == 1;

            if (isUnlocked)
            {
                item.IsBuy = true; // Ensure item state is updated
                Item.GetComponent<Image>().sprite = item.ItemImg;
                Item.GetComponent<Button>().onClick.AddListener(() =>
                {
                    DeactivateAllItems();
                    id = item.Id; // Set the ID for purchasing
                    btnGet.SetActive(true);
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
                    if (idSelecSkinLock > 0 && !ImageItemData.Items[idSelecSkinLock - 1].IsBuy)
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
        // lstImage[DataConfig.EffectIndex].transform.GetChild(2).gameObject.SetActive(true);
        ShopContent.SetActive(true); // Ensure ShopContent is active after loading
    }

    private void DeactivateAllItems()
    {
        foreach (GameObject image in lstImage)
        {
            image.transform.GetChild(2).gameObject.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {

    }
}
