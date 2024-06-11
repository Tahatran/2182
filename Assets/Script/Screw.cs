using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using System;

public class Screw : MonoBehaviour
{
    private Coroutine toggleCoroutine;
    private Coroutine toggleCoroutine2;
    public bool HasBulong;
    public GameObject Bulong;
    public int col;
    public int row;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    BulongAction GetClickedBulong()
    {
        GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
        foreach (GameObject bulongGameObject in gameCtrInstance.lstBulong)
        {
            BulongAction bulongAction = bulongGameObject.GetComponent<BulongAction>();
            if (bulongAction.IsClicked)
            {
                return bulongAction;
            }
        }
        // Trả về null nếu không tìm thấy Bulong nào có IsClicked là true
        return null;
    }
    void MoveBulong()
    {

        // killTween();
        // DOTween.KillAll();
        if (HasBulong)
        {
            BulongAction bulongAction = GetClickedBulong();
            if (bulongAction != null)
            {

                // Debug.Log("2");
                TweenDown(bulongAction.gameObject);
                Tweenup(Bulong);
                HasBulong = false;

            }
            else
            {
                // Debug.Log("3");
                Tweenup(Bulong);
                HasBulong = false;
            }
        }
        else
        {

            BulongAction bulongAction3 = GetClickedBulong();
            if (bulongAction3 != null)
            {
                try
                {
                    if (bulongAction3.tag == this.gameObject.tag)
                    {
                        // Debug.Log("try");
                        Vector3 upPosition = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
                        bulongAction3.IsClicked = false;
                        // Tween Bulong đến vị trí mới để di chuyển lên trên Screw
                        bulongAction3.transform.DOMove(upPosition, 0.2f).SetEase(Ease.OutQuad)
                        // bulongAction3.BulongBody.transform.DOScale(0f, 0.3f).SetEase(Ease.OutQuad)
                        .OnComplete(() => TweenDowndone(bulongAction3.gameObject));
                    }
                    else if (bulongAction3.row == Bulong.GetComponent<BulongAction>().row && bulongAction3.col == Bulong.GetComponent<BulongAction>().col)
                    {
                        // Debug.Log("5");
                        TweenDown(Bulong);
                    }
                    else if (bulongAction3.tag == this.gameObject.tag)
                    {
                        // Audio.instance.shak.Stop();
                        Audio.instance.shak.Play();
                        bulongAction3.transform.DOShakePosition(0.3f, new Vector3(0.05f, 0, 0));
                    }
                }
                catch
                {
                    // Audio.instance.shak.Stop();
                    Audio.instance.shak.Play();
                    bulongAction3.transform.DOShakePosition(0.3f, new Vector3(0.05f, 0, 0));
                }
            }
        }
        resetOnOffBulong();
    }

    void resetOnOffBulong()
    {
        GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
        foreach (var bulongObject in gameCtrInstance.lstBulong)
        {
            var bulongface = bulongObject.transform.Find("Bulongface").gameObject;
            var bulongface2 = bulongObject.transform.Find("Bulongface2").gameObject;
            bulongface.SetActive(true);
            bulongface2.SetActive(false);
        }
    }

    void killTween()
    {
        GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
        foreach (var bulongObject in gameCtrInstance.lstBulong)
        {
            DOTween.Kill(bulongObject);
        }
    }

    void Tweenup(GameObject Bulong)
    {
        // Audio.instance.sfxScrew.Stop();
        Audio.instance.sfxScrew.Play();
        Audio.instance.sfxScrew2.Play();

        // DOTween.KillAll();
        // Debug.Log("3");
        var bulongfaceUp = Bulong.transform.Find("Bulongface").gameObject;
        var bulongbodyUp = Bulong.transform.Find("Bulongface").gameObject;
        var bulongface2 = Bulong.transform.Find("Bulongface2").gameObject;
        float targetY = transform.position.y + 0.4f;
        Sequence sequence = DOTween.Sequence();
        // Bắt đầu coroutine để toggle faces
        if (toggleCoroutine2 != null)
        {
            StopCoroutine(toggleCoroutine2);
            bulongfaceUp.SetActive(true);
            bulongface2.SetActive(false);
        }
        toggleCoroutine2 = StartCoroutine(ToggleFacesWhileTweening(bulongfaceUp, bulongface2));
        // Tween di chuyển lên
        sequence.Append(Bulong.transform.DOMoveY(targetY, 0.2f).SetEase(Ease.OutQuad)).OnUpdate(() =>
                    {
                        // DOVirtual.DelayedCall(0.05f, () =>
                        // {
                        Bulong.GetComponent<BulongAction>().BulongBody.transform.DOScale(0.45f, 0.2f).SetEase(Ease.OutQuad);
                        // });
                    })
        .OnComplete(() =>
        {
            if (toggleCoroutine2 != null)
            {
                StopCoroutine(toggleCoroutine2);
                bulongfaceUp.SetActive(true);
                bulongface2.SetActive(false);
                Bulong.GetComponent<BulongAction>().IsClicked = true;
                HasBulong = false;
            }
        });
        // Tween scale của BulongBody từ 0 đến 0.45 sau khi tween position kết thúc
        // sequence.Join(Bulong.GetComponent<BulongAction>().BulongBody.transform.DOScale(0.45f, 0.2f).SetEase(Ease.OutQuad));

    }


    void TweenDown(GameObject bulongGameObject)
    {
        // Audio.instance.sfxScrew.Stop();
        Audio.instance.sfxScrew.Play();
        Audio.instance.sfxScrew2.Play();
        // DOTween.KillAll();
        var bulongFaceDown = bulongGameObject.transform.Find("Bulongface").gameObject;
        var bulongFace2Down = bulongGameObject.transform.Find("Bulongface2").gameObject;

        // Tạo vị trí mới cho Bulong để di chuyển xuống dưới
        Vector3 downPosition = new Vector3(bulongGameObject.transform.position.x, bulongGameObject.transform.position.y - 0.4f, bulongGameObject.transform.position.z);
        if (toggleCoroutine != null)
        {
            StopCoroutine(toggleCoroutine);
            bulongFaceDown.SetActive(true);
            bulongFace2Down.SetActive(false);
        }
        toggleCoroutine = StartCoroutine(ToggleFacesWhileTweening(bulongFaceDown, bulongFace2Down));

        // Tween Bulong xuống vị trí mới
        bulongGameObject.transform.DOMove(downPosition, 0.2f).SetEase(Ease.OutQuad).OnUpdate(() =>
                    {
                        DOVirtual.DelayedCall(0.05f, () =>
                        {
                            bulongGameObject.GetComponent<BulongAction>().BulongBody.transform.DOScale(0f, 0.2f).SetEase(Ease.OutQuad);
                        });
                    })
                    .OnComplete(() =>
                                    {
                                        if (toggleCoroutine != null)
                                        {
                                            StopCoroutine(toggleCoroutine);
                                        }
                                        bulongFaceDown.SetActive(true);
                                        bulongFace2Down.SetActive(false);
                                        bulongGameObject.GetComponent<BulongAction>().IsClicked = false;
                                    });

        HasBulong = true;

        GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
        foreach (var screwObject in gameCtrInstance.lstCrew)
        {
            var screw = screwObject.GetComponent<Screw>();
            var rowS = screw.row;
            var colS = screw.col;

            foreach (var bulongObject in gameCtrInstance.lstBulong)
            {
                var bulongAction = bulongObject.GetComponent<BulongAction>();
                var rowB = bulongAction.row;
                var colB = bulongAction.col;

                if (rowS == rowB && colS == colB)
                {
                    screw.HasBulong = true;
                    screw.Bulong = bulongObject;
                    break; // Dừng vòng lặp khi đã tìm thấy Bulong tương ứng
                }
            }
        }
    }

    void TweenDowndone(GameObject bulongGameObject)
    {
        Audio.instance.sfxgone.Play();
        var bulongFaceDone = bulongGameObject.transform.Find("Bulongface").gameObject;
        var bulongFace2Done = bulongGameObject.transform.Find("Bulongface2").gameObject;
        // Tạo vị trí mới cho Bulong để di chuyển xuống dưới
        Vector3 downPosition = new Vector3(bulongGameObject.transform.position.x, bulongGameObject.transform.position.y - 0.3f, bulongGameObject.transform.position.z);
        if (toggleCoroutine != null)
        {
            StopCoroutine(toggleCoroutine);
            bulongFaceDone.SetActive(true);
            bulongFace2Done.SetActive(false);
        }
        toggleCoroutine = StartCoroutine(ToggleFacesWhileTweening(bulongFaceDone, bulongFace2Done));

        // Tween Bulong xuống vị trí mới
        bulongGameObject.transform.DOMove(downPosition, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
                    {
                        if (toggleCoroutine != null)
                        {
                            StopCoroutine(toggleCoroutine);
                        }
                        bulongFaceDone.SetActive(true);
                        bulongFace2Done.SetActive(false);
                    });
        bulongGameObject.GetComponent<BulongAction>().BulongBody.transform.DOScale(0f, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                this.gameObject.transform.DOScale(0, 0.3f).SetEase(Ease.OutQuad);
                bulongGameObject.transform.DOScale(0, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    // Xóa Bulong và Screw khỏi danh sách và Destroy các GameObject
                    GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
                    if (gameCtrInstance != null)
                    {
                        gameCtrInstance.lstBulong.Remove(bulongGameObject);
                        gameCtrInstance.lstCrew.Remove(this.gameObject);
                        UpdateRemainingTags();
                        gameCtrInstance.CheckLose();
                        Destroy(bulongGameObject);
                        Destroy(this.gameObject);
                    }
                });
            });
    }

    void UpdateRemainingTags()
    {
        GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
        gameCtrInstance.bulongTags.Clear();
        gameCtrInstance.crewTags.Clear();

        // Cập nhật tag từ danh sách lstCrew
        foreach (var screwObject in gameCtrInstance.lstCrew)
        {
            var screw = screwObject.GetComponent<Screw>();
            var tag = screwObject.tag;
            if (!gameCtrInstance.crewTags.Contains(tag))
            {
                gameCtrInstance.crewTags.Add(tag);
            }
        }

        //  foreach (var screwObject in gameCtrInstance.lstCrew)
        // {
        //     var screw = screwObject.GetComponent<Screw>();
        //     var tag = screwObject.tag;
        //     if (!gameCtrInstance.crewTags.Contains(tag))
        //     {
        //         if (screw.HasBulong == false)
        //         {
        //             gameCtrInstance.crewTags.Add(screw.tag);
        //         }
        //         gameCtrInstance.crewTags.Add(tag);
        //     }

        // Cập nhật tag từ danh sách lstBulong
        foreach (var bulongObject in gameCtrInstance.lstBulong)
        {
            var bulongAction = bulongObject.GetComponent<BulongAction>();
            var tag = bulongObject.tag;
            if (!gameCtrInstance.bulongTags.Contains(tag))
            {
                gameCtrInstance.bulongTags.Add(tag);
            }
        }
    }


    void OnMouseDown()
    {
        MoveBulong();
    }

    IEnumerator ToggleFacesWhileTweening(GameObject BuLongface, GameObject Bulongface2)
    {
        while (true)
        {
            ToggleBulongFaces(BuLongface, Bulongface2);
            // BuLongface.SetActive(true);
            // Bulongface2.SetActive(false);
            yield return new WaitForSeconds(0.02f); // Điều chỉnh thời gian chờ theo ý muốn
        }
    }
    void ToggleBulongFaces(GameObject BuLongface, GameObject Bulongface2)
    {
        // Bật tắt xen kẽ giữa Bulongface và Bulongface2
        if (BuLongface.activeSelf)
        {
            BuLongface.SetActive(false);
            Bulongface2.SetActive(true);
        }
        else
        {
            BuLongface.SetActive(true);
            Bulongface2.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
