using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Screw : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    void MoveBulong()
    {
        GameCtr gameCtrInstance = GameObject.FindObjectOfType<GameCtr>();
        if (gameCtrInstance != null)
        {
            foreach (GameObject bulongGameObject in gameCtrInstance.lstBulong)
            {
                BulongAction bulongAction = bulongGameObject.GetComponent<BulongAction>();
                // Kiểm tra nếu isClicked của phần tử là true
                if (bulongAction.IsClicked)
                {
                    // Thực hiện lệnh của MoveBulong
                    bool bulongAtScrewPosition = CheckForBulongAtScrewPosition();
                    // Debug.Log(bulongAtScrewPosition);
                    if (!bulongAtScrewPosition && this.gameObject.tag == bulongAction.gameObject.tag)
                    {
                        // Debug.Log("MoveBulong");
                        Vector3 upPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                        bulongAction.IsClicked = false;
                        // Tween Bulong đến vị trí mới để di chuyển lên trên Screw
                        bulongGameObject.transform.DOMove(upPosition, 0.3f).SetEase(Ease.OutQuad)
                        // bulongAction.BulongBody.transform.DOScale(0f, 0.3f).SetEase(Ease.OutQuad)
                            .OnComplete(() => TweenDown(bulongGameObject)); // Gọi hàm TweenDown khi di chuyển lên hoàn thành
                    }
                    else
                    {
                        // Lắc Bulong tại chỗ nếu không cùng tag hoặc không ở đúng vị trí
                        bulongGameObject.transform.DOShakePosition(0.3f, new Vector3(0.05f, 0, 0));
                    }
                }
            }
        }

    }

    void TweenDown(GameObject bulongGameObject)
    {
        // Tạo vị trí mới cho Bulong để di chuyển xuống dưới
        Vector3 downPosition = new Vector3(bulongGameObject.transform.position.x, bulongGameObject.transform.position.y - 0.5f, bulongGameObject.transform.position.z);

        // Tween Bulong xuống vị trí mới
        bulongGameObject.transform.DOMove(downPosition, 0.3f).SetEase(Ease.OutQuad);
        bulongGameObject.GetComponent<BulongAction>().BulongBody.transform.DOScale(0f, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
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
                        gameCtrInstance.CheckLose();
                        Destroy(bulongGameObject);
                        Destroy(this.gameObject);
                    }
                });
            });
    }

    bool CheckForBulongAtScrewPosition()
    {
        // Lấy vị trí hiện tại của Screw
        Vector3 screwPosition = transform.position;

        // Tạo một hình cầu với bán kính nhỏ xung quanh vị trí của Screw
        float radius = 0.5f; // Điều chỉnh bán kính theo kích thước của Screw và Bulong
        Collider[] colliders = Physics.OverlapSphere(screwPosition, radius);

        // Duyệt qua tất cả các Collider trong vùng cầu

        if (colliders.Length > 0)
        {
            // Tìm thấy đối tượng Bulong tại vị trí của Screw
            return true;
        }

        // Nếu không tìm thấy Bulong tại vị trí của Screw
        return false;
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
