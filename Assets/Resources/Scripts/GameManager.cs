using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelCompleteText;
    int currentSceneIndex;
    public int requiredScore; // Set the required score for victory
    private int currentScore = 0;
    public int startingLives = 3;
    private int currentLives;
    public Image[] shipImage;

    public GameObject playerPrefab;
    private GameObject player;
    public Transform respawnPoint;

    private void Awake()
    {
        levelCompleteText.gameObject.SetActive(false);
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelCompleteText.text = "Level " + (currentSceneIndex + 1) + " Complete\n Nice Work!";
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
        if (player != null)
        {
            Destroy(player.gameObject);
        }

        currentLives--;
 
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
        player = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
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
        if (SceneManager.GetActiveScene().name == "1")
        {
            requiredScore = 2400;
        }
        if (SceneManager.GetActiveScene().name == "2")
        {
            requiredScore = 4000;
        }
        if (SceneManager.GetActiveScene().name == "3")
        {
            requiredScore = 5600;
        }
        if (SceneManager.GetActiveScene().name == "4")
        {
            requiredScore = 8000;
        }
        // Check if the current score meets the required score for victory
        if (currentScore >= requiredScore)
        {
            Debug.Log("Victory!");
            levelCompleteText.gameObject.SetActive(true);
            Invoke("LoadNextScene", 3f);
            
            // Add any additional victory-related logic here
        }
    }

    void LoadNextScene()
    {
        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    // Method to increment the score
    public void IncrementScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "Score " + currentScore.ToString();
    }
}