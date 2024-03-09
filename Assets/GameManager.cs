using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int requiredScore = 4000; // Set the required score for victory
    private int currentScore = 0;

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
    }
}