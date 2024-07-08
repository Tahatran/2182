using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipePagination : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    public int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshould;

    private void Awakke()
    {
        currentPage = 0;
        targetPos = levelPagesRect.localPosition;
        dragThreshould = Screen.width / 3;
    }
    void Start()
    {

    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 0)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        // Use LeanTween to move the RectTransform
        // LeanTween.moveLocal(levelPagesRect.gameObject, targetPos, tweenTime)
        //          .setEase(tweenType);
        Debug.Log(targetPos);
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    void Update()
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x)
            {
                Previous();
            }
            else
            {
                Next();
            }
        }
        else
        {
            MovePage();
        }
    }
}
