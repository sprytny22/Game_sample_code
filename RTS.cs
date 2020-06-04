using UnityEngine;

public class RTS : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

    private void LateUpdate()
    {
        Vector3 position = transform.position;

        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            position.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness)
        {
            position.z -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            position.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness)
        {
            position.x -= panSpeed * Time.deltaTime;
        }

        position.x = Mathf.Clamp(position.x, panLimit.x, panLimit.y);
        position.z = Mathf.Clamp(position.z, panLimit.x, panLimit.y);

        transform.position = position;
    }
}
