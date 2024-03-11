using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelText;
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
        levelText.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
        levelCompleteText.gameObject.SetActive(false);
        if (instance == null)
        {
            instance = this;
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        scoreText.text = "Score " + currentScore.ToString();
    }

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelCompleteText.text = "Level " + currentSceneIndex + " Complete\n Nice Work!";
        player = GameObject.Find("Player");
        currentLives = startingLives;
        UpdateShipImages();
    }

    public void StartGame()
    {
       
        SceneManager.LoadScene("1");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controls");
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
        Invoke("Respawn", 0.2f);
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
        player.GetComponent<Animator>().SetBool("isRespawning", true);
        Invoke("EnablePlayerCollider", 2f);
    }
    private void EnablePlayerCollider()
    {
        player.GetComponent<Collider2D>().enabled = true;
        player.GetComponent<Animator>().SetBool("isRespawning", false);
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
            
            requiredScore = 6400;
        }
        if (SceneManager.GetActiveScene().name == "3")
        {
            requiredScore = 12000;
        }
        if (SceneManager.GetActiveScene().name == "4")
        {
            requiredScore = 20000;
        }
        if (SceneManager.GetActiveScene().name == "5")
        {
            requiredScore = 29600;
        }
        if (SceneManager.GetActiveScene().name == "6")
        {
            requiredScore = 39200;
        }
        if (SceneManager.GetActiveScene().name == "7")
        {
            requiredScore = 48800;
        }
        if (SceneManager.GetActiveScene().name == "8")
        {
            requiredScore = 58400;
        }
        if (SceneManager.GetActiveScene().name == "9")
        {
            requiredScore = 68000;
        }
        if (SceneManager.GetActiveScene().name == "10")
        {
            requiredScore = 77600;
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

    private void OnDestroy()
    {
        // Save the current score to PlayerPrefs when the GameManager is destroyed
        PlayerPrefs.SetInt("CurrentScore", currentScore);
        PlayerPrefs.Save();
    }
    // Method to increment the score
    public void IncrementScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "Score " + currentScore.ToString();
    }
}