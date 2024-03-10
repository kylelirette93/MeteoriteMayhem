using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteoriteController : MonoBehaviour
{
    private float floatSpeed = 1f;
    Vector2 randomDirection;

    private GameManager gameManagerScript;
    private bool alreadyHit = false;

    private GameObject bullet;
    bool isDestroyed = false;
    public GameObject smallMeteoritePrefab;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("_GM").GetComponent<GameManager>();
        randomDirection = Random.insideUnitCircle.normalized;
        bullet = GameObject.FindWithTag("Bullet");
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDestroyed && collision.gameObject.CompareTag("Bullet"))
        {
            // Check if the gameObject reference is not null
            if (collision.gameObject != null)
            {
                // Ensure the collision occurs only once
                if (!alreadyHit)
                {
                    // Log a message to verify collision detection
                    Debug.Log("Collision with bullet detected!");

                    // Increment the score
                    gameManagerScript.IncrementScore(400);

                    // Log a message to verify score increment
                    Debug.Log("Score incremented!");

                    // Set the alreadyHit flag to true
                    alreadyHit = true;
                }

                // Destroy the big meteorite
                Destroy(gameObject);

                // Log a message to verify destruction
                Debug.Log("Big meteorite destroyed!");

                // Instantiate two smaller meteorites
                if (smallMeteoritePrefab != null)
                {
                    Instantiate(smallMeteoritePrefab, transform.position, Quaternion.identity);
                    Instantiate(smallMeteoritePrefab, transform.position, Quaternion.identity);

                    // Log a message to verify instantiation
                    Debug.Log("Smaller meteorites instantiated!");
                }
                else
                {
                    // Log a warning if the prefab is null
                    Debug.LogWarning("Small meteorite prefab is null!");
                }

                // Set the flag to true to prevent multiple splits
                isDestroyed = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(randomDirection * floatSpeed * Time.deltaTime);
        ScreenWrap();
    }

    void ScreenWrap()
    {
        var cam = Camera.main;
        var newPosition = transform.position;

        // Wrap around the screen in the x-axis
        if (transform.position.x > cam.transform.position.x + cam.orthographicSize * cam.aspect)
        {
            newPosition.x = -cam.transform.position.x - cam.orthographicSize * cam.aspect;
        }
        else if (transform.position.x < cam.transform.position.x - cam.orthographicSize * cam.aspect)
        {
            newPosition.x = -cam.transform.position.x + cam.orthographicSize * cam.aspect;
        }

        // Wrap around the screen in the y-axis
        if (transform.position.y > cam.transform.position.y + cam.orthographicSize)
        {
            newPosition.y = -cam.transform.position.y - cam.orthographicSize;
        }
        else if (transform.position.y < cam.transform.position.y - cam.orthographicSize)
        {
            newPosition.y = -cam.transform.position.y + cam.orthographicSize;
        }

        transform.position = newPosition;
    }


}
