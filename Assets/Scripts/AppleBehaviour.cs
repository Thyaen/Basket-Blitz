using System.Collections;
using UnityEngine;

public class AppleBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera mainCamera;
    private HealthManager healthManager;
    private SoundManager soundManager;

    public static bool appleIsFalling = false;

    void Start()
    {
        healthManager = FindFirstObjectByType<HealthManager>();
        soundManager = FindFirstObjectByType<SoundManager>();
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

        // Warten, bis kein anderer Apfel mehr fällt
        while (appleIsFalling)
        {
            yield return null;
        }

        appleIsFalling = true;
        rb.gravityScale = 1f;
    }

    void Update()
    {
        // Unteren Bildschirmrand in Weltkoordinaten bestimmen
        float bottomEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        // Objekt löschen, wenn es unter den Bildschirm fällt
        if (transform.position.y < bottomEdge - 1f)
        {
            if (soundManager != null)
            {
                soundManager.PlayMissSound();
            }

            if (healthManager != null)
            {
                healthManager.LoseLife();
            }


            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (rb != null && rb.gravityScale > 0f)
        {
            appleIsFalling = false;
        }
    }
}