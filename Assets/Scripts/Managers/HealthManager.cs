using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private SpriteRenderer[] hearts;
    private int currentLives;

    void Start()
    {
        hearts = new SpriteRenderer[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            hearts[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        currentLives = hearts.Length;
    }

    public void LoseLife()
    {
        if (currentLives <= 0)
            return;

        currentLives--;

        hearts[currentLives].sprite = emptyHeart;

        if (currentLives <= 0)
        {
            Debug.Log("Game Over");

            // Optional: Szene neu laden
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}