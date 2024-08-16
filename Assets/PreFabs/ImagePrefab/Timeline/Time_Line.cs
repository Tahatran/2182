using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_Line : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Tutorial.instance.TurnOffAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
