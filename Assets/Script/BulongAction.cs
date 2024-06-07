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
    // public TagData tagData;

    public int col;
    public int row;
    public bool IsClicked = false;
    private Coroutine toggleCoroutine;

    void Start()
    {
        // GetComponent<Collider2D>().enabled = true;
    }

    // Tween Bulongface and BulongBody
    // void TweenBulongObjects()
    // {
    //     // Debug.Log(gameObject.name);
    //     if (!IsClicked)
    //     {
    //         float targetY = transform.position.y + 0.3f;

    //         // Tạo một sequence để kết hợp tween position và tween scale
    //         Sequence sequence = DOTween.Sequence();

    //         // Bắt đầu coroutine để toggle faces
    //         if (toggleCoroutine != null)
    //         {
    //             StopCoroutine(toggleCoroutine);
    //         }
    //         toggleCoroutine = StartCoroutine(ToggleFacesWhileTweening());

    //         // Tween di chuyển lên
    //         sequence.Append(transform.DOMoveY(targetY, 0.2f).SetEase(Ease.OutQuad));

    //         // Tween scale của BulongBody từ 0 đến 0.45 sau khi tween position kết thúc
    //         sequence.Join(BulongBody.transform.DOScale(0.45f, 0.1f).SetEase(Ease.OutQuad));

    //         sequence.OnComplete(() =>
    //         {
    //             Tweendown();
    //             IsClicked = true;
    //             if (toggleCoroutine != null)
    //             {
    //                 StopCoroutine(toggleCoroutine);
    //             }
    //             Bulongface.SetActive(true);
    //             Bulongface2.SetActive(false);
    //         });
    //     }
    //     else
    //     {
    //         Tweendown();
    //     }
    // }


    // void Tweendown()
    // {
    //     GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();

    //     if (gameCtrInstance != null)
    //     {
    //         foreach (GameObject bulongGameObject in gameCtrInstance.lstBulong)
    //         {
    //             BulongAction bulongAction = bulongGameObject.GetComponent<BulongAction>();

    //             if (bulongAction != null && bulongAction.IsClicked)
    //             {
    //                 float targetY = bulongAction.transform.position.y - 0.3f;

    //                 Sequence sequence = DOTween.Sequence();

    //                 // Bắt đầu coroutine để toggle faces
    //                 if (toggleCoroutine != null)
    //                 {
    //                     StopCoroutine(toggleCoroutine);
    //                 }
    //                 toggleCoroutine = StartCoroutine(ToggleFacesWhileTweening());

    //                 // Tween di chuyển xuống
    //                 sequence.Append(bulongAction.transform.DOMoveY(targetY, 0.19f).SetEase(Ease.OutQuad));

    //                 // Tween scale của BulongBody về 0
    //                 sequence.Join(bulongAction.BulongBody.transform.DOScale(0f, 0.19f).SetEase(Ease.OutQuad));

    //                 sequence.OnComplete(() =>
    //                 {
    //                     gameCtrInstance.CheckLose();
    //                     bulongAction.IsClicked = false;
    //                     if (toggleCoroutine != null)
    //                     {
    //                         StopCoroutine(toggleCoroutine);
    //                     }
    //                     Bulongface.SetActive(true);
    //                     Bulongface2.SetActive(false);
    //                 });
    //             }
    //         }
    //     }
    // }

    // IEnumerator ToggleFacesWhileTweening()
    // {
    //     while (true)
    //     {
    //         ToggleBulongFaces();
    //         yield return new WaitForSeconds(0.01f); // Điều chỉnh thời gian chờ theo ý muốn
    //     }
    // }

    // void ToggleBulongFaces()
    // {
    //     // Bật tắt xen kẽ giữa Bulongface và Bulongface2
    //     if (Bulongface.activeSelf)
    //     {
    //         Bulongface.SetActive(false);
    //         Bulongface2.SetActive(true);
    //     }
    //     else
    //     {
    //         Bulongface.SetActive(true);
    //         Bulongface2.SetActive(false);
    //     }
    // }

    // Handle click event on GameObject
    void OnMouseDown()
    {
        // TweenBulongObjects();
    }
}
