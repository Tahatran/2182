using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    // Define the zoom area
    public Rect zoomArea = new Rect(0, 0, Screen.width, Screen.height); // Adjust as needed

    // Define camera movement boundaries
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;

    void Update()
    {
        if (Image1308.instance.lstUpgameobject.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchStart.z = -10; // Giữ cố định trục Z
            }

            if (Input.touchCount == 2)
            {
                Vector2 touchZero = Input.GetTouch(0).position;
                Vector2 touchOne = Input.GetTouch(1).position;

                if (zoomArea.Contains(touchZero) && zoomArea.Contains(touchOne))
                {
                    Vector2 touchZeroPrevPos = touchZero - Input.GetTouch(0).deltaPosition;
                    Vector2 touchOnePrevPos = touchOne - Input.GetTouch(1).deltaPosition;

                    float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float currentMagnitude = (touchZero - touchOne).magnitude;

                    float difference = currentMagnitude - prevMagnitude;

                    zoom(difference * 0.01f);
                }
            }
            else if (Input.GetMouseButton(0) && Camera.main.orthographicSize < zoomOutMax)
            {
                if (zoomArea.Contains(Input.mousePosition))
                {
                    Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    direction.z = 0; // Không thay đổi vị trí Z
                    Camera.main.transform.position += direction;

                    // Clamp the camera position to keep it within boundaries
                    ClampCameraPosition();
                }
            }

            if (zoomArea.Contains(Input.mousePosition))
            {
                zoom(Input.GetAxis("Mouse ScrollWheel"));
            }
        }
    }

    void zoom(float increment)
    {
        Vector3 beforeZoom = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);

        Vector3 afterZoom = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = beforeZoom - afterZoom;

        Camera.main.transform.position += difference;

        // Giữ camera trong giới hạn tọa độ x và y
        ClampCameraPosition();
    }

    void ClampCameraPosition()
    {
        float currentOrthographicSize = Camera.main.orthographicSize;
        float clampedMinX = minX * (currentOrthographicSize / zoomOutMax);
        float clampedMaxX = maxX * (currentOrthographicSize / zoomOutMax);
        float clampedMinY = minY * (currentOrthographicSize / zoomOutMax);
        float clampedMaxY = maxY * (currentOrthographicSize / zoomOutMax);

        Camera.main.transform.position = new Vector3(
            Mathf.Clamp(Camera.main.transform.position.x, clampedMinX, clampedMaxX),
            Mathf.Clamp(Camera.main.transform.position.y, clampedMinY, clampedMaxY),
            -10 // Đảm bảo Z = -10
        );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Calculate the camera's zoom level and its effect on the movement boundaries
        float currentOrthographicSize = Camera.main != null ? Camera.main.orthographicSize : zoomOutMax;
        float clampedMinX = minX * (currentOrthographicSize / zoomOutMax);
        float clampedMaxX = maxX * (currentOrthographicSize / zoomOutMax);
        float clampedMinY = minY * (currentOrthographicSize / zoomOutMax);
        float clampedMaxY = maxY * (currentOrthographicSize / zoomOutMax);

        // Draw the boundary box
        Vector3 bottomLeft = new Vector3(clampedMinX, clampedMinY, -10);
        Vector3 bottomRight = new Vector3(clampedMaxX, clampedMinY, -10);
        Vector3 topLeft = new Vector3(clampedMinX, clampedMaxY, -10);
        Vector3 topRight = new Vector3(clampedMaxX, clampedMaxY, -10);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
