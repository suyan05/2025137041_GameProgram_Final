using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int highScore = 0;

    public int totalCoin = 0;

    void Awake()
    {
        score = 0;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData(); // 저장된 최고점 불러오기
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        score += value;
        PlayerPrefs.SetInt("Score", score);

        // 최고 점수 갱신
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void AddCoin(int value)
    {
        totalCoin += value;
        PlayerPrefs.SetInt("TotalCoin", totalCoin);
    }

    public void ResetData()
    {
        highScore = 0;
        totalCoin = 0;
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.DeleteKey("TotalCoin");
    }

    void LoadData()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        totalCoin = PlayerPrefs.GetInt("TotalCoin", 0);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space)&&Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Scene");
        }
    }
}
