using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 offset;

    void OnMouseDown()
    {
        offset = transform.position -
            GetMousePosition();
    }

    void OnMouseDrag()
    {
        transform.position =
            GetMousePosition() + offset;
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePos =
            Input.mousePosition;

        mousePos.z = 10f;

        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}