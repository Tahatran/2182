using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipePagination : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public ScrollRect scrollRect;
    public float sensitivity = 10f; // Độ nhạy của việc phát hiện swipe

    private float[] pages;
    private int currentPage = 0;
    private bool isDragging = false;

    void Start()
    {
        int pageCount = CalculatePageCount();
        pages = new float[pageCount];
        for (int i = 0; i < pageCount; i++)
        {
            pages[i] = 1f / pageCount * i;
        }
    }

    void Update()
    {
        if (!isDragging)
        {
            LerpToPage(currentPage);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float velocity = eventData.velocity.x;
        if (Mathf.Abs(velocity) > sensitivity)
        {
            if (velocity > 0 && currentPage > 0)
            {
                currentPage--;
            }
            else if (velocity < 0 && currentPage < pages.Length - 1)
            {
                currentPage++;
            }
        }
        else
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if (scrollRect.horizontalNormalizedPosition < pages[i] + (1f / pages.Length / 2) &&
                    scrollRect.horizontalNormalizedPosition > pages[i] - (1f / pages.Length / 2))
                {
                    currentPage = i;
                    break;
                }
            }
        }
        isDragging = false;
    }

    void LerpToPage(int page)
    {
        float target = pages[page];
        scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, target, Time.deltaTime * 10f);
    }

    int CalculatePageCount()
    {
        // Tính toán số lượng trang dựa trên số lượng phần tử bạn có và số lượng phần tử trên mỗi trang
        // Ví dụ: Nếu bạn có 32 phần tử và muốn mỗi trang có 8 phần tử, sẽ có 4 trang
        // Trong ví dụ này, pageCount sẽ là 4
        int itemCount = YourItemCount(); // Thay YourItemCount() bằng hàm lấy số lượng phần tử của bạn
        int itemsPerPage = 8; // Số phần tử trên mỗi trang
        return Mathf.CeilToInt((float)itemCount / itemsPerPage);
    }

    int YourItemCount()
    {
        // Hàm này sẽ trả về số lượng phần tử bạn có trong danh sách của bạn
        // Thay thế bằng cách lấy danh sách phần tử của bạn
        // Ví dụ: return YourList.Count;
        return 32; // Ví dụ: bạn có 32 phần tử
    }
}
