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

    // public GameObject pnlTutorial;
    public GameObject ShopContent;
    public GameObject ShopContent2;
    public GameObject ItemPrefab;
    public GameObject btnAds;
    public GameObject btnGet;
    public List<Sprite> lstDemo;
    public GameObject demoImage;
    public List<GameObject> lstSkin;
    public List<int> lstSave;
    public int id;
    public int idSelecSkinLock;

    // Start is called before the first frame update
    void Start()
    {
        // int Skin = PlayerPrefs.GetInt("Skin", 0);
        // PlayerPrefs.SetInt("Skin", Skin);
        // DataConfig.EffectIndex = PlayerPrefs.GetInt("Skin", 0);
        // PlayerPrefs.Save();
        // LoadShop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Get()
    {
        DataConfig.EffectIndex = id;
        PlayerPrefs.SetInt("Skin", DataConfig.EffectIndex);
        btnGet.SetActive(false);
        DeactivateAllItems2();
        lstSkin[DataConfig.EffectIndex].transform.GetChild(3).gameObject.SetActive(true);
        demoImage.GetComponent<Image>().sprite = SkinItemData.Items[id].PreviewImg;
        if (id == 0)
        {
            //1308 tat di vi co asset moi
            // demoImage.GetComponent<Image>().SetNativeSize();
            // demoImage.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1.4f);
            // demoImage.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90f);
        }
        else
        {
            demoImage.GetComponent<Image>().SetNativeSize();
            demoImage.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
            demoImage.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0f);
        }
        if (Tutorial.instance.lstTutorialSkins[3].activeSelf)
        {
            Tutorial.instance.SKinBlur[3].SetActive(false);
            Tutorial.instance.SKinBlur[4].SetActive(true);
            Tutorial.instance.lstTutorialSkins[3].SetActive(false);
            Tutorial.instance.lstTutorialSkins[4].SetActive(true);
            Tutorial.instance.EnableRaycast(Tutorial.instance.uiElements[6]);
        }
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
                // DataConfig.EffectIndex = idSelecSkinLock;
                id = idSelecSkinLock;
                btnAds.SetActive(false);
                ShowLogFireBase.Instance.LogChangeSkin(id);
            }
            else
            {
                Debug.Log("Reward skin failed");
            }
            GameCtr.instance.Loading.GetComponent<TweenLoading>().HideLoading();
        });

    }

    private void UnlockSkin(int skinId)
    {
        lstSkin[skinId].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void LoadShop()
    {

        if (PlayerPrefs.GetInt("lv") >= 2)
        {
            SkinItemData.Items[1].IsBuy = true;
            PlayerPrefs.SetInt("SkinUnlocked_" + 1, 1); // Save the unlock status
        }
        if (PlayerPrefs.GetInt("lv") >= 16)
        {
            SkinItemData.Items[2].IsBuy = true;
            PlayerPrefs.SetInt("SkinUnlocked_" + 2, 1); // Save the unlock status
        }
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
                Item.transform.GetChild(4).transform.gameObject.SetActive(false);
                if (PlayerPrefs.GetInt("lv") == 2 && PlayerPrefs.GetInt("CheckTutorialSkin") == 0)
                {
                    lstSkin[0].GetComponent<Button>().enabled = false;
                }


                item.IsBuy = true; // Ensure item state is updated
                Item.GetComponent<Image>().sprite = item.ItemImg;

                Item.GetComponent<Button>().onClick.AddListener(() =>
                {
                    //tat khi ma select lai 
                    if (lstSkin[DataConfig.EffectIndex].transform.GetChild(3).gameObject.activeSelf)
                    {
                        lstSkin[DataConfig.EffectIndex].transform.GetChild(3).gameObject.SetActive(false);
                    }
                    //neu select cai khac thi bat cai duoc select len
                    if (Item != lstSkin[DataConfig.EffectIndex])
                    {
                        lstSkin[DataConfig.EffectIndex].transform.GetChild(3).gameObject.SetActive(true);
                    }

                    DeactivateAllItems();
                    id = item.Id; // Set the ID for purchasing
                    btnGet.SetActive(true);
                    btnAds.SetActive(false);
                    Item.transform.GetChild(2).gameObject.SetActive(true); // Activate some UI element
                    if (Tutorial.instance.lstTutorialSkins[2].activeSelf)
                    {
                        id = 0;
                        Item.GetComponent<Button>().enabled = false;
                        id = 1;
                        Item.transform.GetChild(2).gameObject.SetActive(true);
                        Tutorial.instance.SKinBlur[2].SetActive(false);
                        Tutorial.instance.SKinBlur[3].SetActive(true);
                        Tutorial.instance.lstTutorialSkins[2].SetActive(false);
                        Tutorial.instance.lstTutorialSkins[3].SetActive(true);
                        Tutorial.instance.EnableRaycast(Tutorial.instance.uiElements[4]);
                    }
                });
            }
            else
            {
                Item.GetComponent<Image>().sprite = item.ItemImg;
                // Item.transform.GetChild(0).gameObject.SetActive(true);
                Item.transform.GetChild(4).transform.gameObject.GetComponent<Image>().sprite = item.activeLevel;

                //     Item.GetComponent<Button>().onClick.AddListener(() =>
                //   {
                //       DeactivateAllItems();
                //       idSelecSkinLock = item.Id;
                //       Item.transform.GetChild(2).gameObject.SetActive(true);
                //       // Handle non-buyable items, bat len de select lan luot skin
                //       //   if (idSelecSkinLock > 0 && !SkinItemData.Items[idSelecSkinLock - 1].IsBuy)
                //       //   {
                //       //       btnAds.SetActive(false);
                //       //       btnGet.SetActive(false);
                //       //   }
                //       //   else
                //       //   {
                //       //       btnGet.SetActive(true);
                //       //       btnAds.SetActive(true);
                //       //   }
                //       btnGet.SetActive(true);
                //       btnAds.SetActive(true);
                //   });

                if (i == 3 || i == 4)
                {
                    Item.GetComponent<Button>().onClick.AddListener(() =>
                  {
                      DeactivateAllItems();
                      idSelecSkinLock = item.Id;
                      Item.transform.GetChild(2).gameObject.SetActive(true);
                      // Handle non-buyable items, bat len de select lan luot skin
                      //   if (idSelecSkinLock > 0 && !SkinItemData.Items[idSelecSkinLock - 1].IsBuy)
                      //   {
                      //       btnAds.SetActive(false);
                      //       btnGet.SetActive(false);
                      //   }
                      //   else
                      //   {
                      //       btnGet.SetActive(true);
                      //       btnAds.SetActive(true);
                      //   }
                      btnGet.SetActive(true);
                      btnAds.SetActive(true);
                  });
                }
            }
        }
        if (PlayerPrefs.GetInt("lv") == 2 && Tutorial.instance.lstTutorialSkins[2].activeSelf)
        {
            lstSkin[0].GetComponent<Button>().enabled = false;
            lstSkin[2].GetComponent<Button>().enabled = false;
            lstSkin[3].GetComponent<Button>().enabled = false;
            lstSkin[4].GetComponent<Button>().enabled = false;
            // lstSkin[5].GetComponent<Button>().enabled = false;
        }
        lstSkin[DataConfig.EffectIndex].transform.GetChild(2).gameObject.SetActive(true);
        lstSkin[DataConfig.EffectIndex].transform.GetChild(3).gameObject.SetActive(true);
        id = DataConfig.EffectIndex;
        ShopContent.SetActive(true); // Ensure ShopContent is active after loading
    }

    private void DeactivateAllItems()
    {
        foreach (GameObject skin in lstSkin)
        {
            skin.transform.GetChild(2).gameObject.SetActive(false);
            // skin.transform.GetChild(3).gameObject.SetActive(false);
        }
    }
    private void DeactivateAllItems2()
    {
        foreach (GameObject skin in lstSkin)
        {
            // skin.transform.GetChild(2).gameObject.SetActive(false);
            skin.transform.GetChild(3).gameObject.SetActive(false);
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
