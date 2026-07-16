using UnityEngine;
using System.Collections;

public class BasketBehaviour : MonoBehaviour
{
    [Header("Basket Sprites")]
    public Sprite basketSprite;
    public Sprite trashCanSprite;
    public bool IsTrashMode { get; private set; } = false;

    private SpriteRenderer spriteRenderer;
    private Coroutine trashCoroutine;

    private ScoreManager scoreManager;
    private SoundManager soundManager;
    private HealthManager healthManager;


    private FollowMouseHorizontal movement;

    private void Start()
    {
        soundManager = FindFirstObjectByType<SoundManager>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
        healthManager = FindFirstObjectByType<HealthManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        movement = GetComponent<FollowMouseHorizontal>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsTrashMode)
            {
                SwitchToBasket();
            }
            else
            {
                SwitchToTrash();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AppleBehaviour apple = other.GetComponent<AppleBehaviour>();

        if (apple != null)
        {
            Debug.Log("" + apple.isRotten + IsTrashMode);
            bool correctCatch =
                (!apple.isRotten && !IsTrashMode) ||
                (apple.isRotten && IsTrashMode);

            if (correctCatch)
            {
                // Richtiger Behälter
                scoreManager.AddScore();

                if (soundManager != null)
                    soundManager.PlayCatchSound();
                movement.AppleCaught();

                // War es ein fauler Apfel?
                if (apple.isRotten)
                {
                    DeactivateTrashMode();
                }
            }
            else
            {
                // Falscher Behälter
                healthManager.LoseLife();

                if (soundManager != null)
                    soundManager.PlayMissSound();
            }

            Destroy(other.gameObject);
        }
    }

    public void ActivateTrashMode()
    {
        IsTrashMode = true;
        if (trashCoroutine != null)
        {
            StopCoroutine(trashCoroutine);
        }

        trashCoroutine = StartCoroutine(TrashModeRoutine());
    }

    public void DeactivateTrashMode()
    {
        SwitchToBasket();
    }

    private IEnumerator TrashModeRoutine()
    {
        SwitchToTrash();

        yield return new WaitForSeconds(1.5f);

        SwitchToBasket();

        trashCoroutine = null;
    }

    public void SwitchToBasket()
    {
        if (trashCoroutine != null)
        {
            StopCoroutine(trashCoroutine);
            trashCoroutine = null;
        }

        IsTrashMode = false;
        spriteRenderer.sprite = basketSprite;
    }

    public void SwitchToTrash()
    {
        if (trashCoroutine != null)
        {
            StopCoroutine(trashCoroutine);
            trashCoroutine = null;
        }

        IsTrashMode = true;
        spriteRenderer.sprite = trashCanSprite;
    }



}