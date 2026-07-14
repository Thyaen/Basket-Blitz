using UnityEngine;

public class BasketBehaviour : MonoBehaviour
{
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AppleBehaviour>() != null)
        {
            if (scoreManager != null)
            {
                scoreManager.AddScore();
            }

            Destroy(other.gameObject);
        }
    }
}