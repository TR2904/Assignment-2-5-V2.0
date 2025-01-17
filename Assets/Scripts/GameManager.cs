using UnityEngine;

using UnityEngine.SceneManagement;

using TMPro;


public class GameManager : MonoBehaviour

{

    public static GameManager instance;


    public GameObject gameOverCanvas; // Reference to the Game Over UI Canvas

    public TextMeshProUGUI highScoreText; // UI element to display the high score

    public TextMeshProUGUI scoreText; // UI element to display the current score


    private int currentScore = 0;

    private int highScore = 0;


    private void Awake()

    {

        if (instance == null)

        {

            instance = this;

            DontDestroyOnLoad(gameObject);

        }

        else

        {

            Destroy(gameObject);

        }

    }


    private void Start()

    {

        gameOverCanvas.SetActive(false);

        LoadHighScore();

        UpdateScoreUI();

    }


    public void AddScore(int amount)

    {

        currentScore += amount;

        UpdateScoreUI();

        Debug.Log($"Score Updated: {currentScore}");

    }


    public void TriggerGameOver()

    {

        gameOverCanvas.SetActive(true);

        UpdateHighScore();

        Debug.Log($"Game Over triggered! Final Score: {currentScore}, High Score: {highScore}");

        Time.timeScale = 0f; // Pause the game

    }


    public void RetryGame()

    {

        Time.timeScale = 1f; // Resume the game

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene

    }


    public void MainMenu()

    {

        Time.timeScale = 1f; // Resume the game

        SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene name

    }


    private void UpdateScoreUI()

    {

        if (scoreText != null)

        {

            scoreText.text = $"Score: {currentScore}";

        }

    }


    private void UpdateHighScore()

    {

        if (currentScore > highScore)

        {

            highScore = currentScore;

            PlayerPrefs.SetInt("HighScore", highScore);

            PlayerPrefs.Save();

            Debug.Log($"New High Score: {highScore}");

        }


        if (highScoreText != null)

        {

            highScoreText.text = $"High Score: {highScore}";

        }

    }


    private void LoadHighScore()

    {

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScoreText != null)

        {

            highScoreText.text = $"High Score: {highScore}";

        }

    }

}
