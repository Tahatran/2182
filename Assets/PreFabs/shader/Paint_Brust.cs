using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint_Brust : MonoBehaviour
{
    public float radius = 5.0f;
    public float speed = 1.0f;
    private float angle = 0.0f;
    private Vector3 center;
    public GameObject burst;
    public GameObject trail;


    void Start()
    {
        center = transform.position;
        // StartCoroutine(delay());
        // IEnumerator delay()
        // {

        //     // trail.SetActive(true);
        //     yield return new WaitForSeconds(2.8f);
        burst.SetActive(true);

        // }
    }

    // void Start()
    // {
    //     center = transform.position;
    //     StartCoroutine(delay());
    //     IEnumerator delay()
    //     {

    //         // trail.SetActive(true);
    //         yield return new WaitForSeconds(2.8f);
    //         burst.SetActive(true);

    //     }
    // }

    // void Update()
    // {
    //     angle += speed * Time.deltaTime;
    //     radius -= speed * Time.deltaTime * 0.1f;

    //     float x = center.x + radius * Mathf.Cos(angle);
    //     float y = center.y + radius * Mathf.Sin(angle);

    //     transform.position = new Vector3(x, y, transform.position.z);

    //     if (radius <= 0)
    //     {
    //         trail.SetActive(false);
    //         gameObject.GetComponent<SpriteRenderer>().enabled = false;
    //         // gameObject.SetActive(false);

    //         // if (burst != null)
    //         // {
    //         //     burst.SetActive(true);
    //         // }
    //     }
    // }
}