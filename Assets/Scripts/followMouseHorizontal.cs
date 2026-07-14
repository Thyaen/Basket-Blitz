using UnityEngine;

public class followMouseHorizontal : MonoBehaviour
{
    private float minX;
    private float maxX;

    void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 0.6f;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 0.6f;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float x = Mathf.Clamp(mousePos.x, minX, maxX);

        transform.position = new Vector3(
            x,
            transform.position.y,
            transform.position.z
        );
    }
}
