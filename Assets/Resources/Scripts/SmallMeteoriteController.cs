using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMeteoriteController : MonoBehaviour
{

    public float floatSpeed = 2f;
    Vector2 randomDirection;
    private CircleCollider2D circleCollider;
    private BigMeteoriteController BMCScript;

    private GameManager gameManagerScript;
    private bool alreadyHit = false;

    public AudioClip explosionSound;

    private GameObject bullet;
    bool isDestroyed = false;
    
    // Start is called before the first frame update
    void Awake()
    {
       
        gameManagerScript = GameObject.Find("_GM").GetComponent<GameManager>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        randomDirection = Random.insideUnitCircle.normalized;
        bullet = GameObject.FindWithTag("Bullet");
        Invoke("EnableCollider", 0.2f);

        

    }

    void EnableCollider()
    {
        circleCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDestroyed) // Check if the meteorite hasn't been destroyed yet
        {
            // Ensure the collision occurs only once
            if (!alreadyHit)
            {
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    GameObject collidedObject = contact.collider.gameObject;
                    if (collidedObject.CompareTag("Bullet"))
                    {
                        // Destroy the meteorite
                        Destroy(gameObject);

                        // Increment the score
                        gameManagerScript.IncrementScore(200);

                        // Log a message to verify score increment
                        Debug.Log("Score incremented!");

                        if (explosionSound != null)
                        {
                            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
                        }

                        // Set the alreadyHit flag to true
                        alreadyHit = true;

                        // Set the flag to true
                        isDestroyed = true;
                        Destroy(collision.gameObject);
                        break; // Exit the loop once a bullet collision is detected
                    }
                }
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
            newPosition.x = -cam.orthographicSize * cam.aspect;
        }
        else if (transform.position.x < cam.transform.position.x - cam.orthographicSize * cam.aspect)
        {
            newPosition.x = cam.orthographicSize * cam.aspect;
        }

        // Wrap around the screen in the y-axis
        if (transform.position.y > cam.transform.position.y + cam.orthographicSize)
        {
            newPosition.y = -cam.orthographicSize;
        }
        else if (transform.position.y < cam.transform.position.y - cam.orthographicSize)
        {
            newPosition.y = cam.orthographicSize;
        }

        transform.position = newPosition;
    }


}
