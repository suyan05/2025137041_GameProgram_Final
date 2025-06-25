using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI healthText;

    private PlayerHealth player;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (scoreText != null)
                scoreText.text = $"Score: {GameManager.Instance.score}";

            if (highScoreText != null)
                highScoreText.text = $"HighScore: {GameManager.Instance.highScore}";

            if (coinText != null)
                coinText.text = $"Coin: {GameManager.Instance.totalCoin}";
        }

        if (player != null && healthText != null)
        {
            healthText.text = $"Health: {player.CurrentHealth:F0} / {player.maxHealth}";
        }
    }
}