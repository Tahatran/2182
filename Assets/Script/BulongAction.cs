using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;


public class BulongAction : MonoBehaviour
{
    public GameObject Bulongface;

    public GameObject Bulongface2;
    public GameObject BulongBody;
    public TagData tagData;

    public bool IsClicked = false;



    void Start()
    {
        // GameCtr GameCtr = Gamectr.GetComponent<GameCtr>();
        // Register click event on GameObject
        GetComponent<Collider2D>().enabled = true; // Collider needed to receive click event
    }

    // Tween Bulongface and BulongBody
    void TweenBulongObjects()
    {
        if (IsClicked == false)
        {
            float targetY = transform.position.y + 0.5f;

            // Tạo một sequence để kết hợp tween position và tween scale
            Sequence sequence = DOTween.Sequence();

            // Tween position vừa bật tắt
            sequence.Append(transform.DOMoveY(targetY, 0.2f).SetEase(Ease.OutQuad).OnUpdate(() =>
            {
                ToggleBulongFaces();
            }));

            // Tween scale của BulongBody từ 0 đến 0.45 sau khi tween position kết thúc
            sequence.Join(BulongBody.transform.DOScale(0.45f, 0.1f).SetEase(Ease.OutQuad));

            //? nên tính lại vị trí đặt 2 cái này. 
            sequence.OnComplete(() =>
            {
                Tweendown();
                IsClicked = true;
                Bulongface.SetActive(true);
                Bulongface2.SetActive(false);
            });
        }
        else
        {
            Tweendown();
        }

    }

    void Tweendown()
    {
        GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();

        if (gameCtrInstance != null)
        {
            foreach (GameObject bulongGameObject in gameCtrInstance.lstBulong)
            {
                // Lấy thành phần BulongAction từ GameObject
                BulongAction bulongAction = bulongGameObject.GetComponent<BulongAction>();

                // Kiểm tra xem có thành phần BulongAction không
                if (bulongAction != null && bulongAction.IsClicked)
                {
                    // Calculate the target Y position relative to the current position of the BulongAction
                    float targetY = bulongAction.transform.position.y - 0.5f;

                    // Tween the BulongAction from its current position to the calculated target position over 1 second with Ease.OutQuad ease type
                    bulongAction.transform.DOMoveY(targetY, 0.3f).SetEase(Ease.OutQuad).OnUpdate(() =>
                    {
                        ToggleBulongFaces();
                    });
                    bulongAction.BulongBody.transform.DOScale(0f, 0.3f).SetEase(Ease.OutQuad);
                    gameCtrInstance.CheckLose();
                    bulongAction.IsClicked = false;
                    Bulongface.SetActive(true);
                    Bulongface2.SetActive(false);
                }
            }

        }

    }

    void ToggleBulongFaces()
    {
        // Bật tắt xen kẽ giữa Bulongface và Bulongface2
        if (Bulongface.activeSelf)
        {
            Bulongface.SetActive(false);
            Bulongface2.SetActive(true);
        }
        else
        {
            Bulongface.SetActive(true);
            Bulongface2.SetActive(false);
        }
    }

    // Handle click event on GameObject
    void OnMouseDown()
    {
        TweenBulongObjects();
    }
}
