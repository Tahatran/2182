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
    public float clampedMinX;
    public float clampedMaxX;
    public float clampedMinY;
    public float clampedMaxY;

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
        if (Camera.main.orthographicSize == zoomOutMax)
        {
            // Nếu zoom đến mức nhỏ nhất, đưa camera về vị trí (0, 0)
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }
        else
        {
            // Giữ camera trong giới hạn tọa độ x và y
            Camera.main.transform.position = new Vector3(
                Mathf.Clamp(Camera.main.transform.position.x, minX * Camera.main.orthographicSize / zoomOutMax, maxX * Camera.main.orthographicSize / zoomOutMax),
                Mathf.Clamp(Camera.main.transform.position.y, minY * Camera.main.orthographicSize / zoomOutMax, maxY * Camera.main.orthographicSize / zoomOutMax),
                -10 // Đảm bảo Z = -10 sau khi zoom
            );
        }
        // Giữ camera trong giới hạn tọa độ x và y
        ClampCameraPosition();
    }

    void ClampCameraPosition()
    {
        float currentOrthographicSize = Camera.main.orthographicSize;
        clampedMinX = minX * (currentOrthographicSize / zoomOutMax);
        clampedMaxX = maxX * (currentOrthographicSize / zoomOutMax);
        clampedMinY = minY * (currentOrthographicSize / zoomOutMax);
        clampedMaxY = maxY * (currentOrthographicSize / zoomOutMax);
        if (currentOrthographicSize > 2.8f)
        {
            float a = 1f;
            clampedMinX = -a;
            clampedMaxX = a;
            clampedMinY = -a;
            clampedMaxY = a;
        }
        else if (currentOrthographicSize > 1.5f && currentOrthographicSize < 2.8f)
        {
            float a = 1.5f;
            clampedMinX = -a;
            clampedMaxX = a;
            clampedMinY = -a;
            clampedMaxY = a;
        }
        // float b = 3f;
        // float a = 1.6f;
        // if (clampedMinX < -b) clampedMinX = -a;
        // if (clampedMaxX > b) clampedMaxX = a;
        // if (clampedMinY < -b) clampedMinY = -a;
        // if (clampedMaxY > b) clampedMaxY = a;
        Debug.LogError("cu---" + currentOrthographicSize + "-----zooom" + zoomOutMax + "----ket qua" + currentOrthographicSize / zoomOutMax);

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
        clampedMinX = minX * (currentOrthographicSize / zoomOutMax);
        clampedMaxX = maxX * (currentOrthographicSize / zoomOutMax);
        clampedMinY = minY * (currentOrthographicSize / zoomOutMax);
        clampedMaxY = maxY * (currentOrthographicSize / zoomOutMax);
        if (currentOrthographicSize > 2.8f)
        {
            float a = 1f;
            clampedMinX = -a;
            clampedMaxX = a;
            clampedMinY = -a;
            clampedMaxY = a;
        }
        else if (currentOrthographicSize > 1.5f && currentOrthographicSize < 2.8f)
        {
            float a = 1.5f;
            clampedMinX = -a;
            clampedMaxX = a;
            clampedMinY = -a;
            clampedMaxY = a;
        }
        // float b = 3f;
        // float a = 1.6f;
        // if (clampedMinX < -b) clampedMinX = -a;
        // if (clampedMaxX > b) clampedMaxX = a;
        // if (clampedMinY < -b) clampedMinY = -a;
        // if (clampedMaxY > b) clampedMaxY = a;
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
