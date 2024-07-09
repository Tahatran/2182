using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridComponent : MonoBehaviour
{
    public GameObject bulong;
    public GameObject screw;

    // Start is called before the first frame update
    void Start()
    {

    }

    void MoveBulong()
    {
        if (ImageCtr.instance.Delete1)
        {
            if (screw != null)
            {
                Destroy(screw.gameObject);
                screw = null;
            }


        }
        else if (ImageCtr.instance.Delete2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = ImageCtr.instance.HexagridPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
            gameObject.GetComponent<SpriteRenderer>().color = ImageCtr.instance.HexagridPrefab.GetComponentInChildren<SpriteRenderer>().color;
            gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 90f);
            gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            bulong = null;
        }
        else
        {
            if (ImageCtr.instance.checkbulongorscrew)
            {
                // if (bulong == null)
                // {
                // Instantiate đối tượng prefab tại vị trí của targetGrid
                // var instantiatedObject = Instantiate(ImageCtr.instance.objinstance, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                // // Cập nhật tỷ lệ scale của đối tượng tạo ra
                // instantiatedObject.transform.localScale = new Vector3(2f, 2f, 2f);
                // instantiatedObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                // // Đặt parent của đối tượng được tạo là objectContainer
                // instantiatedObject.transform.SetParent(gameObject.transform);
                // bulong = instantiatedObject;
                // instantiatedObject.GetComponent<CircleCollider2D>().enabled = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = ImageCtr.instance.objinstance.GetComponentInChildren<SpriteRenderer>().sprite;
                gameObject.GetComponent<SpriteRenderer>().color = ImageCtr.instance.objinstance.GetComponentInChildren<SpriteRenderer>().color;
                gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
                gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                gameObject.tag = ImageCtr.instance.objinstance.tag;
                bulong = gameObject;
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
                // }
            }
            else if (ImageCtr.instance.checkbulongorscrew == false)
            {
                if (screw == null)
                {
                    // Instantiate đối tượng prefab tại vị trí của targetGrid
                    var instantiatedObject = Instantiate(ImageCtr.instance.objinstance, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                    // Cập nhật tỷ lệ scale của đối tượng tạo ra
                    instantiatedObject.transform.localScale = new Vector3(2f, 2f, 2f);
                    instantiatedObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                    // Đặt parent của đối tượng được tạo là objectContainer
                    instantiatedObject.transform.SetParent(gameObject.transform);
                    screw = instantiatedObject;
                    instantiatedObject.GetComponent<CircleCollider2D>().enabled = false;
                }
                else if (screw != null)
                {
                    foreach (Transform child in gameObject.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    //
                    // Instantiate đối tượng prefab tại vị trí của targetGrid
                    var instantiatedObject = Instantiate(ImageCtr.instance.objinstance, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                    // Cập nhật tỷ lệ scale của đối tượng tạo ra
                    instantiatedObject.transform.localScale = new Vector3(2f, 2f, 2f);
                    instantiatedObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                    // Đặt parent của đối tượng được tạo là objectContainer
                    instantiatedObject.transform.SetParent(gameObject.transform);
                    screw = instantiatedObject;
                    instantiatedObject.GetComponent<CircleCollider2D>().enabled = false;
                }
            }

        }
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
