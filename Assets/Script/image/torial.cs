using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class torial : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject pnlTutorial;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ShowTutorialPanel();
    }

    // This function is called when the click is released
    public void OnPointerUp(PointerEventData eventData)
    {
        HideTutorialPanel();
    }

    private void ShowTutorialPanel()
    {
        if (pnlTutorial != null)
        {
            pnlTutorial.SetActive(true);
        }
    }

    private void HideTutorialPanel()
    {
        if (pnlTutorial != null)
        {
            pnlTutorial.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
