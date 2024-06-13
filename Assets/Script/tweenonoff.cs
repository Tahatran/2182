using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TweenOnOff : MonoBehaviour
{
    public GameObject onObject;
    public GameObject offObject;
    public float interval = 2f; // Time in seconds between toggles

    private bool isOn = true;

    void Start()
    {
        // Start the toggle loop
        StartCoroutine(DelayedLoop());

    }
    IEnumerator DelayedLoop()
    {
        yield return new WaitForSeconds(0.8f); // Change the delay time as needed
        loop();
    }

    void loop()
    {
        if (PlayerPrefs.GetInt("lv") == 1)
        {
            gameObject.SetActive(true);
            StartCoroutine(ToggleObjects());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void turnOff()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator ToggleObjects()
    {
        while (true)
        {
            Toggle();
            yield return new WaitForSeconds(interval);
        }
    }

    private void Toggle()
    {
        if (isOn)
        {
            // Turn off 'onObject' and turn on 'offObject'
            onObject.SetActive(false);
            offObject.SetActive(true);
        }
        else
        {
            // Turn on 'onObject' and turn off 'offObject'
            onObject.SetActive(true);
            offObject.SetActive(false);
        }

        // Toggle the state
        isOn = !isOn;
    }

    public void TweenToPosition(Vector3 targetPosition, float duration)
    {
        // Tween the GameObject this script is attached to
        transform.DOMove(targetPosition, duration).SetEase(Ease.InOutQuad);
    }

}
