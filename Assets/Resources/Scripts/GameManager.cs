using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public int requiredScore = 4000; // Set the required score for victory
    private int currentScore = 0;
    public int startingLives = 3;
    private int currentLives;
    public Image[] shipImage;

    public GameObject playerPrefab;
    private GameObject player;
    public Transform respawnPoint;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        currentLives = startingLives;
        UpdateShipImages();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoseLife()
    {
        currentLives--;
        if (currentLives == 3)
        {
            Destroy(player.gameObject);
        }
        else
        {
            Destroy(playerPrefab.gameObject);
        }
        Invoke("Respawn", 0.4f);
        livesText.text = currentLives.ToString() + " Lives Left";
        
        UpdateShipImages();

        if (currentLives <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }
    }

    void Respawn()
    {
        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }
    void UpdateShipImages()
    {
        for (int i = 0; i < shipImage.Length; i++)
        {
            if (i < currentLives)
                shipImage[i].enabled = true;
            else
                shipImage[i].enabled = false;
                
        }
    }
    void Update()
    {
        // Check if the current score meets the required score for victory
        if (currentScore >= requiredScore)
        {
            Debug.Log("Victory!");
            // Add any additional victory-related logic here
        }
    }

    // Method to increment the score
    public void IncrementScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "Score " + currentScore.ToString();
    }
}