using UnityEngine;

public class BasketBehaviour : MonoBehaviour
{
    private ScoreManager scoreManager;
    private SoundManager soundManager;

    private FollowMouseHorizontal movement;

    private void Start()
    {
        soundManager = FindFirstObjectByType<SoundManager>();
        scoreManager = FindFirstObjectByType<ScoreManager>();

        movement = GetComponent<FollowMouseHorizontal>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AppleBehaviour>() != null)
        {
            if (scoreManager != null)
            {
                scoreManager.AddScore();
            }

            if (soundManager != null)
            {
                soundManager.PlayCatchSound();
            }
            movement.AppleCaught();

            Destroy(other.gameObject);
        }
    }



}