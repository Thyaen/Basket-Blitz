using System.Collections;
using UnityEngine;

public class AppleBehaviour : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite normalAppleSprite;
    public Sprite rottenAppleSprite;

    [Range(0f, 1f)]
    public float rottenChance = 0.2f;

    public bool isRotten = false;

    [Header("Fall Warning")]
    public float warningScale = 1.15f;
    public float warningDuration = 1f;
    public float shakeSpeed = 25f;

    [Header("Fall Delay")]
    public float delayBetweenApples = 0.75f;

    public AudioClip warningSound;

    private Vector3 originalScale;
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private HealthManager healthManager;
    private SoundManager soundManager;
    private SpriteRenderer spriteRenderer;

    public static bool appleIsFalling = false;
    public static float nextAllowedFallTime = 0f;

    private SpawnPointData spawnPointData;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Random.value < rottenChance)
        {
            isRotten = true;
            spriteRenderer.sprite = rottenAppleSprite;
        }
        else
        {
            isRotten = false;
            spriteRenderer.sprite = normalAppleSprite;
        }
        audioSource = GetComponent<AudioSource>();

        originalScale = transform.localScale;

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

        // Warten, bis kein anderer Apfel fällt
        while (appleIsFalling || Time.time < nextAllowedFallTime)
        {
            yield return null;
        }
        appleIsFalling = true;

        // ===== Warnphase =====


        float timer = 0f;

        while (timer < warningDuration)
        {
            float progress = timer / warningDuration;
            progress = Mathf.SmoothStep(0f, 1f, progress);
            transform.localScale = Vector3.Lerp(
                originalScale,
                originalScale * warningScale,
                progress
            );
            float angle = Mathf.Sin(Time.time * shakeSpeed) * 10f;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);


            timer += Time.deltaTime;
            yield return null;
        }



        // Sound direkt vor dem Fallen
        if (warningSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(warningSound);
        }

        // Kurze Verzögerung, damit der Sound hörbar beginnt
        yield return new WaitForSeconds(0.15f);

        // Position zurücksetzen
        transform.rotation = Quaternion.identity;
        transform.localScale = originalScale;

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
        if (spawnPointData != null)
        {
            spawnPointData.currentApple = null;
        }
        if (rb != null && rb.gravityScale > 0f)
        {
            appleIsFalling = false;
            nextAllowedFallTime = Time.time + delayBetweenApples;
        }
    }

    public void SetSpawnPoint(SpawnPointData data)
    {
        spawnPointData = data;
    }
}