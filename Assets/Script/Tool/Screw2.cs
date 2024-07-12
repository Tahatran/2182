using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

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
        //tools bat
        //  MoveBulong();
        ImageCtr.instance.Delete1 = false;
        // ImageCtr.instance.Delete2 = false;
        // ImageCtr.instance.btnDelete.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        // ImageCtr.instance.btnDelete2.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
