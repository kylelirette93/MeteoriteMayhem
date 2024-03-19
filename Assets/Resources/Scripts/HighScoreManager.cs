using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    private int highScore;

    void Start()
    {
        // Load the high score from PlayerPrefs when the game starts
        highScore = PlayerPrefs.GetInt("hiScore", 0); // Default to 0 if "hiScore" is not set
        UpdateHighScoreText(); // Initially update the high score text
    }

    // Method to update the high score text
    void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    // Method to check and update the high score
    public void CheckHighScore(int currentScore)
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("hiScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreText(); // Update the high score text
        }
    }
}