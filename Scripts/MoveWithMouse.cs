using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    float distance = 25;

    public void OnMouseDrag()
    {
        Vector3 screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

        Vector3 offset = Camera.main.ScreenToWorldPoint(screenPoint);

        transform.position = offset;
    }

}
