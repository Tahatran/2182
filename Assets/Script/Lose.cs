using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ReplayLoseBtn()
    {
        Debug.Log("aaaa");
        // SceneManager.LoadScene(0);
        // var rect = losePopup.GetComponent<RectTransform>();
        // rect.DOAnchorPos(new Vector2(rect.anchoredPosition.x, 2000), 0.5f)
        //     .SetEase(Ease.InFlash)
        //     .OnComplete(() => { SceneManager.LoadScene(0); });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
