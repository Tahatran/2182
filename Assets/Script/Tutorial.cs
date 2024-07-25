using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> uiElements;
    public List<GameObject> lstTutorial;
    public static Tutorial instance;
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
        TutorialSkin();
    }
    public void TutorialSkin()
    {
        if (!PlayerPrefs.HasKey("CheckTutorialSkin"))
        {
            int CheckTutorialSkin = 0;
            PlayerPrefs.SetInt("CheckTutorialSkin", CheckTutorialSkin);
        }
        if (PlayerPrefs.GetInt("lv") == 2 && PlayerPrefs.GetInt("CheckTutorialSkin") == 0)
        {
            lstTutorial[0].SetActive(true);
            // DisableAllRaycasts();
            EnableRaycast(uiElements[1]);
        }
    }
    public void DisableAllRaycasts()
    {
        foreach (GameObject uiElement in uiElements)
        {
            SetRaycastTarget(uiElement, false);
        }
    }
    private void SetRaycastTarget(GameObject element, bool enabled)
    {
        // Get all Graphic components (Image, Text, etc.) in the element
        Graphic[] graphics = element.GetComponentsInChildren<Graphic>();
        foreach (Graphic graphic in graphics)
        {
            graphic.raycastTarget = enabled;
        }
    }
    public void EnableRaycast(GameObject targetElement)
    {
        // Disable all raycasts first
        DisableAllRaycasts();

        // Enable raycast for the target element
        SetRaycastTarget(targetElement, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
