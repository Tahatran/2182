using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenLoading : MonoBehaviour
{
    public GameObject loading;
    private bool isLoading = false;
    private Coroutine rotateCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure loading is initially inactive
        // if (loading != null)
        // {
        //     loading.SetActive(false);
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowLoading()
    {
        gameObject.SetActive(true);
        if (loading != null && !isLoading)
        {
            loading.SetActive(true);
            isLoading = true;
            rotateCoroutine = StartCoroutine(RotateLoading());
        }
    }

    public void HideLoading()
    {
        if (loading != null && isLoading)
        {
            if (rotateCoroutine != null)
            {
                StopCoroutine(rotateCoroutine);
            }
            loading.SetActive(false);
            isLoading = false;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator RotateLoading()
    {
        while (true)
        {
            loading.transform.Rotate(new Vector3(0, 0, -90) * Time.deltaTime);
            yield return null;
        }
    }
}
