using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    const int maxHealth = 20;
    private int currentHealth;
    private GameManager gameManagerScript;
    

    void Start()
    {
        currentHealth = maxHealth;   
        gameManagerScript = GameObject.Find("_GM").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SmallMeteorite") || collision.gameObject.CompareTag("BigMeteorite"))
        {
            TakeDamage(20);
        }
    }
    void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameManagerScript.LoseLife();
    }

   
   
}
