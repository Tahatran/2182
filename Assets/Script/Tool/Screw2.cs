using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Screw2 : MonoBehaviour
{
    public bool isBulong;
    // Start is called before the first frame update
    void Start()
    {

    }
    void MoveBulong()
    {
        ImageCtr.instance.indexColor = int.Parse(gameObject.tag);
        if (isBulong)
        {
            ImageCtr.instance.checkbulongorscrew = true;
        }
        else
        {
            ImageCtr.instance.checkbulongorscrew = false;
        }
        ImageCtr.instance.objinstance = gameObject;
        Debug.Log(ImageCtr.instance.checkbulongorscrew);
        Debug.Log(ImageCtr.instance.indexColor);
    }

    void OnMouseDown()
    {
        MoveBulong();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
