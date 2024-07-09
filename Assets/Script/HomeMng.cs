using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMng : MonoBehaviour
{

    public GameObject ImageGameObject;
    public GameObject Skin;
    public GameObject Image;
    public GameObject btnBack;

    public static HomeMng instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void btnBacktoImage()
    {
        btnBack.SetActive(false);
        gameObject.SetActive(true);
        btnImage();
    }

    public void btncometoHome()
    {
        ImageGameObject.SetActive(true);
        gameObject.SetActive(true);

    }


    public void btnLoadGame()
    {
        ImageGameObject.SetActive(false);
        gameObject.SetActive(false);
        GameCtr.instance.autonextlvwhenwin();
    }

    public void btnHome()
    {
        Skin.SetActive(false);
        Image.SetActive(false);
    }

    public void btnSkin()
    {
        Skin.SetActive(true);
        Image.SetActive(false);
    }

    public void btnImage()
    {
        Skin.SetActive(false);
        Image.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
