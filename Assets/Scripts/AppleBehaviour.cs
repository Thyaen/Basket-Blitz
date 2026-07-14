using System.Collections;
using UnityEngine;

public class AppleBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera mainCamera;
    private HealthManager healthManager;

    void Start()
    {
        healthManager = FindFirstObjectByType<HealthManager>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        // Schwerkraft zunächst deaktivieren
        rb.gravityScale = 0f;

        // Zufällige Wartezeit starten
        StartCoroutine(StartFalling());
    }

    IEnumerator StartFalling()
    {
        float waitTime = Random.Range(1f, 10f);
        yield return new WaitForSeconds(waitTime);

        // Schwerkraft aktivieren
        rb.gravityScale = 1f;
    }

    void Update()
    {
        // Unteren Bildschirmrand in Weltkoordinaten bestimmen
        float bottomEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        // Objekt löschen, wenn es unter den Bildschirm fällt
        if (transform.position.y < bottomEdge - 1f)
        {
            if (healthManager != null)
            {
                healthManager.LoseLife();
            }

            Destroy(gameObject);
        }
    }
}