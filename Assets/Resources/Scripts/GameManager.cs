using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // TMP variables
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelText;
    
    // UI variables
    private int currentScore = 0;
    public int startingLives = 3;
    private int currentLives;
    public int currentLevel = 0;
    public Image[] shipImage;

    // Player references
    public GameObject playerPrefab;
    private GameObject player;
    public Transform respawnPoint;


    // Variables for managing asteroids for level
    public GameObject smallMeteoritePrefab;
    public GameObject bigMeteoritePrefab;
   

    private void Awake()
    {
        // Display the current level and score.
        levelText.text = "Level " + (currentLevel + 1).ToString();
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        
        scoreText.text = "Score " + currentScore.ToString();
    }

    private void Start()
    {
        
        player = GameObject.FindWithTag("Player");
        StartLevel(currentLevel);
        currentLives = startingLives;
        UpdateShipImages();

        // Calculate and display the required score for the current level.
        int requiredScore = CalculateRequiredScore(currentLevel + 1); // Add 1 to currentLevel to calculate next level.
        Debug.Log("Required Score for Level " + (currentLevel + 1) + ": " + requiredScore);
    }

    void StartLevel(int level)
    {
        // Update the level text
        levelText.text = "Level " + (level + 1).ToString();

        // Calculate the number of regular and big meteorites
        int numSmallMeteorites = level + 1;
        int numBigMeteorites = level + 3;

        // Calculate the speed based on the current level, increasing by 0.05 every level up to a maximum of 2.5
        float speedIncrement = Mathf.Min(2.5f, 0.5f + level * 0.05f);

        // Define spawn area parameters
        float spawnRadius = 10f; // Radius of the spawn area
        Vector2 playerPosition = player.transform.position; // Get the player position

        // Spawn regular meteorites
        SpawnMeteorites(numSmallMeteorites, spawnRadius, playerPosition, smallMeteoritePrefab, speedIncrement);

        // Spawn big meteorites
        SpawnMeteorites(numBigMeteorites, spawnRadius, playerPosition, bigMeteoritePrefab, speedIncrement);
    }

    void SpawnMeteorites(int count, float spawnRadius, Vector2 playerPosition, GameObject prefab, float speed)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randomPosition;
            do
            {
                randomPosition = playerPosition + Random.insideUnitCircle.normalized * Random.Range(spawnRadius / 2f, spawnRadius);
            } while (Vector2.Distance(randomPosition, playerPosition) < spawnRadius / 2f);

            GameObject meteorite = Instantiate(prefab, randomPosition, Quaternion.identity);

            if (meteorite.TryGetComponent(out MeteoriteController controller))
            {
                controller.floatSpeed = speed;
            }
        }
    }
    // Below are the functions to be called from menu buttons.
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
        // Set the animator as indicator the player isn't susceptible to damage when respawning.
        player = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        player.GetComponent<Animator>().SetBool("isRespawning", true);
        Invoke("EnablePlayerCollider", 2f);
    }
    private void EnablePlayerCollider()
    {
        // Enable collider again when player respawns.
        player.GetComponent<Collider2D>().enabled = true;
        player.GetComponent<Animator>().SetBool("isRespawning", false);
    }
    void UpdateShipImages()
    {
        // Update life system UI images
        for (int i = 0; i < shipImage.Length; i++)
        {
            if (i < currentLives)
                shipImage[i].enabled = true;
            else
                shipImage[i].enabled = false;     
        }
    }


    int CalculateRequiredScore(int level)
    {
        if (level == 1)
        {
            return 2600;
        }
        else if (level == 2)
        {
            return 6200;
        }
        else
        {
            int baseScore = 6200; // Base score for level 3
            int increment = 4600; // Incrementing score for each subsequent level

            for (int i = 3; i <= level; i++)
            {
                baseScore += increment; // Update base score for the current level
                increment += 1000; // Increase the increment by 1000 for each subsequent level
            }

            return baseScore;
        }
    }

    // Method to increment score and check if level needs to be incremented
    public void IncrementScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score " + currentScore.ToString();

        int requiredScore = CalculateRequiredScore(currentLevel + 1); // Calculate required score for the next level

        if (currentScore >= requiredScore)
        {
            currentLevel++;
            StartLevel(currentLevel);
        }
    }

}