using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioWorks))]
public class LogicManager : MonoBehaviour
{
    public int scoring = 0;
    public Text scoreText;
    public GameObject gameOverScreen;
    public BirdScript Bob;

    private AudioWorks audioWorks;
    private bool gameIsOver = false;

    private void Awake()
    {
        audioWorks = GetComponent<AudioWorks>();

        // Automatically finds the bird if Bob is empty.
        var birdScript = Object.FindAnyObjectByType<BirdScript>();

        if (birdScript is null)
        {
            Debug.LogError("Bird script not found!");
        }
        else
        {
            Bob = birdScript;
        }
    }

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Score Text has not been assigned!");
        }
        else
        {
            scoreText.text = scoring.ToString();
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Background music starts once when the game begins.
        audioWorks.PlayGameSound();
    }

    public void PlayJumpSound()
    {
        if (!gameIsOver)
        {
            audioWorks.PlayJumpSound();
        }
    }

    public void AddScore(int scoreToAdd)
    {
        if (gameIsOver || Bob == null || !Bob.Alive)
        {
            return;
        }

        scoring += scoreToAdd;

        // Point sound plays once when a score is added.
        audioWorks.PlayPointSound();

        if (scoreText != null)
        {
            scoreText.text = scoring.ToString();
        }

        Debug.Log("Score increased to: " + scoring);
    }

    public void gameOver()
    {
        // Prevent game over and losing sound from running multiple times.
        if (gameIsOver)
        {
            return;
        }

        gameIsOver = true;

        if (Bob != null)
        {
            Bob.Alive = false;
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Stops background music and plays losing sound once.
        audioWorks.PlayLosingSound();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}