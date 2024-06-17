using System.Collections;
using System.Collections.Generic;
using Nami.Controller;
using UnityEngine;

public class Dontdestroyonload : MonoBehaviour
{

    public float checktime = 0;
    public bool isAdsPlay = false;
    public bool checkads = false;
    public static Dontdestroyonload instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        checktime = 180f;
        isAdsPlay = false;
    }

    public void ads()
    {

        if (isAdsPlay && checkads == true)
        // if (isAdsPlay)
        {
            GameAds.Get.ShowInterstitialAd();
            checktime = GameAds.Get.Time_inter_ad;
            isAdsPlay = false;
            checktime = 180f;
            Debug.Log("Ads Play");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        checktime -= Time.deltaTime;
        if (checktime <= 0)
        {
            checktime = 0;
            isAdsPlay = true;
        }
        ads();

    }
}
