using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject gameOverScreen;
    public TMP_Text finalScoreText;

    private ScoreManager scoreManager;
    private SpriteRenderer[] hearts;
    private int currentLives;

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        gameOverScreen.SetActive(false);
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
            gameOverScreen.SetActive(true);

            scoreManager.HideScore();

            finalScoreText.text = "" + scoreManager.GetScore();

            Time.timeScale = 0f;
        }
    }
}