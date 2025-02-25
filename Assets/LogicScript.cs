using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText; 
    public Text HighScoreText;
    public GameObject gameOverScreen;
    private bool isGameOver = false;
    private bool isNewHighScore = false; // Track if it's a new high score

    SoundEffectsPlayer audioManager;

    [ContextMenu("Increase Score")]
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundEffectsPlayer>();
    }

    public void AddScore(int scoreToAdd)
    {
        if (!gameOverScreen.activeSelf)
        {
            playerScore += scoreToAdd;
            scoreText.text = playerScore.ToString();
            audioManager.PlaySFX(audioManager.newpoint);
            CheckHighScore();
        }
    }

    void CheckHighScore() 
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (playerScore > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            isNewHighScore = true; // Mark as a new high score
        }
    }

    public void GameOver() 
    {
        if (!isGameOver) 
        {
            isGameOver = true;
            gameOverScreen.SetActive(true);
            audioManager.PlaySFX(audioManager.gameover);
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText() 
    {
        RectTransform highScoreRect = HighScoreText.GetComponent<RectTransform>();

        if (isNewHighScore)
        {
            HighScoreText.text = $"NEW High Score: {PlayerPrefs.GetInt("HighScore", 0)}";
            highScoreRect.anchoredPosition += new Vector2(-170, 0); // Move left by 170 units (adjust as needed)
            StartCoroutine(AnimateHighScore());
            audioManager.PlaySFX(audioManager.highScoreSound);
        }
        else
        {
            HighScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore", 0)}";
            highScoreRect.anchoredPosition = new Vector2(750, highScoreRect.anchoredPosition.y); // Reset position
        }
    }

    IEnumerator AnimateHighScore()
    {
        float duration = 2f;
        float elapsed = 0f;
        Vector3 originalScale = HighScoreText.transform.localScale;

        while (elapsed < duration)
        {
            float scale = 1.2f + Mathf.Sin(elapsed * 8f) * 0.2f; // Pulse effect
            HighScoreText.transform.localScale = originalScale * scale;
            HighScoreText.color = Color.Lerp(Color.yellow, Color.red, Mathf.PingPong(elapsed * 3, 1)); // Flashing color
            elapsed += Time.deltaTime;
            yield return null;
        }

        HighScoreText.transform.localScale = originalScale;
        HighScoreText.color = Color.white; // Reset color
    }

    public void RestartGame() 
    {
        isGameOver = false;
        isNewHighScore = false; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
