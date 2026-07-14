using UnityEngine;

public class followMouseHorizontal : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 12f;
    public float smoothTime = 0.08f;

    [Header("Direction Change")]
    [Tooltip("Je größer der Wert, desto träger ist der Richtungswechsel.")]
    public float directionChangeMultiplier = 2.0f;

    private float minX;
    private float maxX;

    private float currentVelocity = 0f;
    private float lastVelocity = 0f;

    void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 0.6f;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 0.6f;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float targetX = Mathf.Clamp(mousePos.x, minX, maxX);

        // Prüfen, ob sich die Bewegungsrichtung ändern würde
        float desiredDirection = Mathf.Sign(targetX - transform.position.x);
        float currentDirection = Mathf.Sign(lastVelocity);

        float currentSmoothTime = smoothTime;

        // Falls die Richtung wechselt, kurz träger werden
        if (desiredDirection != 0 &&
            currentDirection != 0 &&
            desiredDirection != currentDirection)
        {
            currentSmoothTime *= directionChangeMultiplier;
        }

        float newX = Mathf.SmoothDamp(
            transform.position.x,
            targetX,
            ref currentVelocity,
            currentSmoothTime,
            maxSpeed
        );

        lastVelocity = currentVelocity;

        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}